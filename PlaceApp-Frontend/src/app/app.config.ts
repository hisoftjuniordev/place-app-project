import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http'; // <-- DODAJTE TA IMPORT

import { routes } from './app-routing.module'; // <-- UVOZITE RUTING MODUL

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient() // <-- IN DODAJTE TO VRSTICO
  ]
};