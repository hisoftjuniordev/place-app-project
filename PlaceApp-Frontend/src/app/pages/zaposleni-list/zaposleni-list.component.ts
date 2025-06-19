// Datoteka: src/app/pages/zaposleni-list/zaposleni-list.component.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';

// Uvozimo vse potrebne modele in servis
import { Zaposlen, Podjetje, ZaposleniService, NovoPodjetjeDTO, NovZaposlenDTO } from '../../services/zaposleni.service';

@Component({
  selector: 'app-zaposleni-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './zaposleni-list.component.html',
  styleUrls: ['./zaposleni-list.component.css']
})
export class ZaposleniListComponent implements OnInit {
  
  // Dva seznama: en za zaposlene, en za podjetja
  zaposleni: Zaposlen[] = [];
  podjetja: Podjetje[] = [];

  // Dva ločena obrazca
  podjetjeForm: FormGroup;
  zaposlenForm: FormGroup;

  constructor(
    private zaposleniService: ZaposleniService,
    private fb: FormBuilder
  ) {
    // Obrazec za ustvarjanje podjetja
    this.podjetjeForm = this.fb.group({
      naziv: ['', Validators.required],
      davcnaStevilka: ['', [Validators.required, Validators.pattern('^[0-9]{8}$')]],
      maticnaStevilka: ['', Validators.required],
      naslov: ['', Validators.required],
    });

    // Obrazec za ustvarjanje zaposlenega
    this.zaposlenForm = this.fb.group({
      ime: ['', Validators.required],
      priimek: ['', Validators.required],
      emso: ['', [Validators.required, Validators.pattern('^[0-9]{13}$')]],
      davcnaStevilka: ['', [Validators.required, Validators.pattern('^[0-9]{8}$')]],
      naslov: [''],
      trr: ['', Validators.required],
      datumZaposlitve: [new Date().toISOString().split('T')[0], Validators.required],
      // Ključno: polje za ID podjetja, ki ga bomo izbrali iz spustnega seznama
      podjetjeID: [null, Validators.required] 
    });
  }

  ngOnInit(): void {
    // Ob zagonu naložimo tako zaposlene kot podjetja
    this.naloziZaposlene();
    this.naloziPodjetja();
  }

  // --- Metode za nalaganje podatkov ---

  naloziZaposlene(): void {
    this.zaposleniService.getZaposleni().subscribe(data => {
      this.zaposleni = data;
    });
  }

  naloziPodjetja(): void {
    this.zaposleniService.getPodjetja().subscribe(data => {
      this.podjetja = data;
    });
  }

  // --- Metode za oddajo obrazcev ---

  onPodjetjeSubmit(): void {
    if (this.podjetjeForm.invalid) return;

    const novoPodjetje: NovoPodjetjeDTO = this.podjetjeForm.value;
    this.zaposleniService.addPodjetje(novoPodjetje).subscribe(() => {
      this.naloziPodjetja(); // Osvežimo seznam podjetij
      this.podjetjeForm.reset();
    });
  }

  onZaposlenSubmit(): void {
    if (this.zaposlenForm.invalid) return;
    
    const novZaposlen: NovZaposlenDTO = this.zaposlenForm.value;
    this.zaposleniService.addZaposlen(novZaposlen).subscribe(() => {
      this.naloziZaposlene(); // Osvežimo seznam zaposlenih
      this.zaposlenForm.reset();
      // Ponastavimo datum
      this.zaposlenForm.patchValue({
        datumZaposlitve: new Date().toISOString().split('T')[0]
      });
    });
  }
}

export interface Zaposleni {
  // ...other properties...
  podjetje?: { naziv: string } | null;
}