// Datoteka: src/app/pages/evidenca-dela/evidenca-dela.component.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ZaposleniService, EvidencaUra } from '../../services/zaposleni.service';

@Component({
  selector: 'app-evidenca-dela',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, DatePipe], // Dodamo DatePipe za formatiranje datumov
  templateUrl: './evidenca-dela.component.html',
  styleUrls: ['./evidenca-dela.component.css']
})
export class EvidencaDelaComponent implements OnInit {

  evidencaForm: FormGroup;
  zaposlenId: number = 0;
  trenutniMesec: Date = new Date(); // Privzeto na trenutni mesec

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private zaposleniService: ZaposleniService
  ) {
    this.evidencaForm = this.fb.group({
      dnevi: this.fb.array([]) // FormArray bo vseboval skupino polj za vsak dan
    });
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.zaposlenId = +id;
      this.pripraviKoledarZaMesec();
    }
  }

  // Getter za lažji dostop do FormArray-a v HTML predlogi
  get dnevi(): FormArray {
    return this.evidencaForm.get('dnevi') as FormArray;
  }

  // Pripravi obrazec za cel mesec
  pripraviKoledarZaMesec(): void {
    const leto = this.trenutniMesec.getFullYear();
    const mesec = this.trenutniMesec.getMonth();
    const stDniVMesecu = new Date(leto, mesec + 1, 0).getDate();

    this.dnevi.clear(); // Počistimo morebitne stare vnose

    for (let i = 1; i <= stDniVMesecu; i++) {
      const datum = new Date(leto, mesec, i);
      this.dnevi.push(this.ustvariVrsticoZaDan(datum));
    }

    this.naloziObstojecePodatke();
  }

  // Ustvari FormGroup za en dan
  ustvariVrsticoZaDan(datum: Date): FormGroup {
    return this.fb.group({
      datum: [datum],
      redneUre: [0, [Validators.min(0), Validators.max(24)]],
      nadure: [0, [Validators.min(0), Validators.max(24)]],
      praznikUre: [0, [Validators.min(0), Validators.max(24)]],
      dopustUre: [0, [Validators.min(0), Validators.max(24)]],
      bolniskaUre: [0, [Validators.min(0), Validators.max(24)]],
      opomba: ['']
    });
  }

  // Naloži že shranjene podatke iz API-ja in jih vpiše v obrazec
  naloziObstojecePodatke(): void {
    const leto = this.trenutniMesec.getFullYear();
    const mesec = this.trenutniMesec.getMonth() + 1; // Meseci so 1-12 v API klicu

    this.zaposleniService.getEvidencaZaMesec(this.zaposlenId, leto, mesec).subscribe(data => {
      data.forEach(vnos => {
        const datumVnosa = new Date(vnos.datum);
        const danVMesecu = datumVnosa.getDate();
        // Poiščemo pravo vrstico v obrazcu in jo posodobimo
        const vrstica = this.dnevi.at(danVMesecu - 1);
        if (vrstica) {
          vrstica.patchValue(vnos);
        }
      });
    });
  }
  
  // Shrani spremembe
  onSubmit(): void {
    if (this.evidencaForm.invalid) {
      alert("Nekatera polja vsebujejo neveljavne vrednosti (ure morajo biti med 0 in 24).");
      return;
    }

    // Pripravimo samo spremenjene/vnesene podatke za pošiljanje
    const podatkiZaPosiljanje = this.evidencaForm.value.dnevi
      .map((dan: any) => ({ ...dan, zaposlenId: this.zaposlenId }))
      .filter((dan: any) => dan.redneUre > 0 || dan.nadure > 0 || dan.praznikUre > 0 || dan.dopustUre > 0 || dan.bolniskaUre > 0);

    if (podatkiZaPosiljanje.length === 0) {
      alert("Niste vnesli nobenih ur za shranjevanje.");
      return;
    }

    this.zaposleniService.saveEvidencaUr(podatkiZaPosiljanje).subscribe({
      next: () => {
        alert('Evidenca ur uspešno shranjena!');
        this.router.navigate(['/zaposleni']);
      },
      error: (err) => {
        console.error('Napaka pri shranjevanju evidence:', err);
        alert('Prišlo je do napake pri shranjevanju.');
      }
    });
  }
}