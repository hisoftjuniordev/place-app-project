// Datoteka: src/app/app-routing.module.ts

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Uvoz komponent, ki jih uporabljate v poteh
import { ZaposleniListComponent } from './pages/zaposleni-list/zaposleni-list.component';
import { IzracunPlaceComponent } from './pages/izracun-place/izracun-place.component'; // <<< DODAJTE TA IMPORT
import { NastavitveComponent } from './pages/nastavitve/nastavitve.component'; // <<< DODAJTE TA IMPORT
import { EvidencaDelaComponent } from './pages/evidenca-dela/evidenca-dela.component'; // <<< DODAJTE TA IMPORT
import { PlacilnaListaComponent } from './pages/placilna-lista/placilna-lista.component'; // <<< DODAJTE


const routes: Routes = [
  // Obstoječe poti
  { path: '', redirectTo: '/zaposleni', pathMatch: 'full' },
  { path: 'zaposleni', component: ZaposleniListComponent },

  // === DODAJTE TO NOVO VRSTICO ZA POT DO IZRAČUNA PLAČE ===
  { path: 'izracun-place/:id', component: IzracunPlaceComponent },
  { path: 'nastavitve', component: NastavitveComponent },
{ path: 'evidenca-dela/:id', component: EvidencaDelaComponent },
{ path: 'placilna-lista/:id', component: PlacilnaListaComponent }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }