// Datoteka: src/app/app-routing.module.ts

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Uvoz komponent, ki jih uporabljate v poteh
import { ZaposleniListComponent } from './pages/zaposleni-list/zaposleni-list.component';
import { IzracunPlaceComponent } from './pages/izracun-place/izracun-place.component'; // <<< DODAJTE TA IMPORT

const routes: Routes = [
  // Obstoječe poti
  { path: '', redirectTo: '/zaposleni', pathMatch: 'full' },
  { path: 'zaposleni', component: ZaposleniListComponent },

  // === DODAJTE TO NOVO VRSTICO ZA POT DO IZRAČUNA PLAČE ===
  { path: 'izracun-place/:id', component: IzracunPlaceComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }