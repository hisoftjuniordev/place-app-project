// Datoteka: src/app/services/zaposleni.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

// --- PODATKOVNI MODELI ---

export interface Podjetje {
  id: number;
  naziv: string;
  davcnaStevilka: string;
  maticnaStevilka: string;
  naslov: string;
}

export interface Zaposlen {
  id: number;
  podjetjeID: number;
  podjetje: Podjetje; // To ostane za lažji prikaz v tabeli
  ime: string;
  priimek: string;
  emso: string;
  davcnaStevilka: string;
  naslov: string | null;
  trr: string;
  datumZaposlitve: string;
  datumPrenehanja: string | null;
  jeAktiven: boolean;
}

// --- DTO (Data Transfer Objects) - Objekti za pošiljanje na API ---

// Uporabimo Partial<T> da so vsa polja opcijska, ker ID-ja ne pošiljamo
export type NovoPodjetjeDTO = Omit<Podjetje, 'id'>;

export type NovZaposlenDTO = Omit<Zaposlen, 'id' | 'podjetje' | 'jeAktiven' | 'datumPrenehanja'>;


@Injectable({
  providedIn: 'root'
})
export class ZaposleniService {
  
  private apiUrlZaposleni = 'https://localhost:7162/api/Zaposleni';
  private apiUrlPodjetje = 'https://localhost:7162/api/Podjetje'; // Nov API naslov za podjetja

  constructor(private http: HttpClient) { }

  // --- Metode za Zaposlene ---

  getZaposleni(): Observable<Zaposlen[]> {
    return this.http.get<Zaposlen[]>(this.apiUrlZaposleni);
  }
  
  addZaposlen(zaposlen: NovZaposlenDTO): Observable<Zaposlen> {
    // Sedaj pošiljamo poenostavljen objekt, ki vsebuje podjetjeID
    return this.http.post<Zaposlen>(this.apiUrlZaposleni, zaposlen);
  }

  // --- NOVE Metode za Podjetja ---

  getPodjetja(): Observable<Podjetje[]> {
    return this.http.get<Podjetje[]>(this.apiUrlPodjetje);
  }

  addPodjetje(podjetje: NovoPodjetjeDTO): Observable<Podjetje> {
    return this.http.post<Podjetje>(this.apiUrlPodjetje, podjetje);
  }
}