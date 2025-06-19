// Datoteka: src/app/app-routing.module.ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ZaposleniListComponent } from './pages/zaposleni-list/zaposleni-list.component';

// Tukaj definiramo vse poti (URL-je) v aplikaciji
const routes: Routes = [
  // Ko uporabnik obišče pot '/zaposleni', se mu prikaže ZaposleniListComponent
  { path: 'zaposleni', component: ZaposleniListComponent },

  // Če uporabnik pride na osnovno stran ('/'), ga preusmerimo na seznam zaposlenih
  { path: '', redirectTo: '/zaposleni', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }