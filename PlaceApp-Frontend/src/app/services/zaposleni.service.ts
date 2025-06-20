// Datoteka: src/app/services/zaposleni.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

// --- Podatkovni Modeli ---

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
  podjetje: Podjetje;
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

// === POPRAVLJEN IN POPOLN VMESNIK ZA PLACA ===
export interface Placa {
    id: number;
    zaposlenId: number;
    obdobje: string;
    // Vnosi iz obrazca
    urnaPostavka: number;
    rednoDeloUre: number;
    placaniPraznikiUre: number; // Manjkajoča lastnost
    poracun: number;
    stimulacija: number;
    delovnaDobaProcent: number;
    posebneOlajsave: number;
    odtegljaji: number;
    prehranaDni: number;
    prehranaZnesek: number;
    prevozKm: number;
    kmCena: number;
    kilometrineFiksno: number;
    // Glavni izračunani zneski
    brutoPlaca: number;
    netoPlaca: number;
    dohodnina: number;
    zaIzplacilo: number;
    celotenStrosekDelodajalca: number;
}
// === DODAJTE NOVA VMESNIKA ===
export interface NastavitveObracuna {
  id: number;
  veljavnoOd: string;
  minimalnaPlaca: number;
  cenaPrehrane: number;
  cenaPrevozaKm: number;
}

export interface DohodninskaLestvica {
  id: number;
  zaporednaSt: number;
  mejaDo: number;
  stopnja: number;
  absolutniZnesek: number;
}
// =============================
// === DODAJTE NOV VMESNIK ===
export interface EvidencaUra {
  id: number;
  zaposlenId: number;
  datum: string;
  redneUre: number;
  nadure: number;
  praznikUre: number;
  dopustUre: number;
  bolniskaUre: number;
  status: string;
  opomba?: string;
}
// =============================

// DTO (Data Transfer Objects)
export type NovoPodjetjeDTO = Omit<Podjetje, 'id'>;
export type NovZaposlenDTO = Omit<Zaposlen, 'id' | 'podjetje' | 'jeAktiven' | 'datumPrenehanja'>;
export type NovaPlacaDTO = Omit<Placa, 'id'>; // Uporabimo lahko to za večjo varnost tipov


@Injectable({
  providedIn: 'root'
})
export class ZaposleniService {
  
  private apiUrlZaposleni = 'https://localhost:7162/api/Zaposleni';
  private apiUrlPodjetje = 'https://localhost:7162/api/Podjetje';
  private apiUrlPlaca = 'https://localhost:7162/api/Placa';
  private apiUrlNastavitve = 'https://localhost:7162/api/Nastavitve';
private apiUrlEvidencaUr = 'https://localhost:7162/api/EvidencaUr'; // <<< DODAJTE

  constructor(private http: HttpClient) { }

  // --- Metode za Zaposlene ---
  getZaposleni(): Observable<Zaposlen[]> {
    return this.http.get<Zaposlen[]>(this.apiUrlZaposleni);
  }
  
  addZaposlen(zaposlen: NovZaposlenDTO): Observable<Zaposlen> {
    return this.http.post<Zaposlen>(this.apiUrlZaposleni, zaposlen);
  }

  getNastavitve(): Observable<any> {
    return this.http.get<any>(this.apiUrlNastavitve);
  }

 
  updateNastavitve(nastavitve: Omit<NastavitveObracuna, 'id'>): Observable<any> {
    return this.http.post(this.apiUrlNastavitve, nastavitve);
  }

  
  updateLestvica(lestvica: Omit<DohodninskaLestvica, 'id'>[]): Observable<any> {
    return this.http.post(`${this.apiUrlNastavitve}/lestvica`, lestvica);
  }

  // --- Metode za Podjetja ---
  getPodjetja(): Observable<Podjetje[]> {
    return this.http.get<Podjetje[]>(this.apiUrlPodjetje);
  }

  addPodjetje(podjetje: NovoPodjetjeDTO): Observable<Podjetje> {
    return this.http.post<Podjetje>(this.apiUrlPodjetje, podjetje);
  }

  // --- Metode za Plače ---
  getPlaceForZaposlen(zaposlenId: number): Observable<Placa[]> {
    return this.http.get<Placa[]>(`${this.apiUrlZaposleni}/${zaposlenId}/Place`);
  }

  addPlaca(placa: NovaPlacaDTO): Observable<Placa> {
    return this.http.post<Placa>(this.apiUrlPlaca, placa);
  }
    // === NOVE METODE ZA EVIDENCO UR ===
  
  // Pridobi vse vnose za zaposlenega za določen mesec in leto
  getEvidencaZaMesec(zaposlenId: number, leto: number, mesec: number): Observable<EvidencaUra[]> {
    return this.http.get<EvidencaUra[]>(`${this.apiUrlEvidencaUr}/${zaposlenId}/${leto}/${mesec}`);
  }

  // Shrani seznam vnosov za evidenco ur
  saveEvidencaUr(vnosi: Omit<EvidencaUra, 'id' | 'zaposlen'>[]): Observable<any> {
    return this.http.post(this.apiUrlEvidencaUr, vnosi);
  }
  // =================================
  getPlacaById(id: number): Observable<Placa> {
    return this.http.get<Placa>(`${this.apiUrlPlaca}/${id}`);
  }
}