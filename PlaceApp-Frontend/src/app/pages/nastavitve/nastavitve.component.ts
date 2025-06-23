// Datoteka: src/app/pages/nastavitve/nastavitve.component.ts
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { DohodninskaLestvica, ZaposleniService } from '../../services/zaposleni.service';
import { NastavitveObracuna } from '../../services/zaposleni.service';

@Component({
  selector: 'app-nastavitve',
  standalone: true,
  // Poskrbimo, da komponenta uvozi vse potrebno za delovanje obrazcev
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './nastavitve.component.html',
  styleUrls: ['./nastavitve.component.css']
})
export class NastavitveComponent implements OnInit {

  nastavitveForm: FormGroup;
  // V prihodnosti lahko dodate tudi polje za dohodninsko lestvico
   dohodninskaLestvica: DohodninskaLestvica[] = [];

  constructor(
    private fb: FormBuilder,
    private zaposleniService: ZaposleniService
  ) {
    // Definiramo obrazec z vsemi polji, ki jih želimo urejati
    this.nastavitveForm = this.fb.group({
      id: [0], // Skrito polje za ID, če ga bomo potrebovali
      veljavnoOd: [new Date().toISOString().slice(0, 10), Validators.required],
      minimalnaPlaca: [0, Validators.required],
      cenaPrehrane: [0, Validators.required],
      cenaPrevozaKm: [0, Validators.required]
    });
  }

  ngOnInit(): void {
    // Ko se stran naloži, pridobimo zadnje shranjene nastavitve
    this.naloziNastavitve();
  }

  naloziNastavitve(): void {
    this.zaposleniService.getNastavitve().subscribe(data => {
      if (data && data.nastavitve) {
        // Prejete podatke vpišemo v naš obrazec
        this.nastavitveForm.patchValue(data.nastavitve);
      }
      // V prihodnosti lahko tukaj obdelate tudi 'data.lestvica'
       if (data && data.lestvica) {
         this.dohodninskaLestvica = data.lestvica;
       }
    });
  }

  onSubmit(): void {
    if (this.nastavitveForm.invalid) {
      alert('Prosimo, izpolnite vsa polja.');
      return;
    }

    // Pripravimo podatke za pošiljanje. 'Omit' ni potreben, ker pošiljamo celoten objekt.
    const noveNastavitve: NastavitveObracuna = this.nastavitveForm.value;

    this.zaposleniService.updateNastavitve(noveNastavitve).subscribe({
      next: () => {
        alert('Nastavitve uspešno shranjene!');
        this.naloziNastavitve(); // Ponovno naložimo, da vidimo morebitne spremembe
      },
      error: (err) => {
        console.error('Napaka pri shranjevanju nastavitev:', err);
        alert('Prišlo je do napake pri shranjevanju.');
      }
    });
  }
}