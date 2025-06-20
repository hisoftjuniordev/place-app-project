// Datoteka: src/app/app.module.ts

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'; // <<< PREVERITE TA IMPORT

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

// Uvoz vaših standalone komponent
import { ZaposleniListComponent } from './pages/zaposleni-list/zaposleni-list.component';
import { IzracunPlaceComponent } from './pages/izracun-place/izracun-place.component';
import { NastavitveComponent } from './pages/nastavitve/nastavitve.component';
import { EvidencaDelaComponent } from './pages/evidenca-dela/evidenca-dela.component';
import { PlacilnaListaComponent } from './pages/placilna-lista/placilna-lista.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,       // <<< PREVERITE, DA JE TA VRSTICA TUKAJ
    ZaposleniListComponent,   // Standalone komponente se uvažajo
    IzracunPlaceComponent,
    EvidencaDelaComponent,
    NastavitveComponent,
    PlacilnaListaComponent     // Standalone komponente se uvažajo
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }