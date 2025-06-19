// Datoteka: src/app/app.module.ts
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module'; // <-- UVOZIMO ROUTING MODUL
import { AppComponent } from './app.component';
import { ZaposleniListComponent } from './pages/zaposleni-list/zaposleni-list.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule, // <-- UPORABIMO ROUTING MODUL TUKAJ
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }