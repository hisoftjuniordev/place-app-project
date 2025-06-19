// Datoteka: src/app/services/placa-konfiguracija.service.ts
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PlacaKonfiguracijaService {

  // Fiksni zneski
  readonly OZZ_FIKSNI = 37.17; // Obvezni zdravstveni prispevek

  // Stopnje prispevkov delojemalca (iz bruto)
  readonly STOPNJA_PIZ = 0.1550;
  readonly STOPNJA_ZZ = 0.0636;
  readonly STOPNJA_ZAPOSLOVANJE = 0.0014;
  readonly STOPNJA_PORODNISKA = 0.0010;

  // Stopnje prispevkov delodajalca (na bruto)
  readonly STOPNJA_PIZ_NA_BRUTO = 0.0885;
  readonly STOPNJA_ZZ_NA_BRUTO = 0.0656;
  readonly STOPNJA_ZAPOSLOVANJE_NA_BRUTO = 0.0006;
  readonly STOPNJA_PORODNISKA_NA_BRUTO = 0.0010;
  readonly STOPNJA_POSKODBE_DELO = 0.0053;

  constructor() { }

  /**
   * Izračuna splošno olajšavo za leto 2025.
   * @param mesecniBruto Mesečni bruto znesek.
   * @returns Znesek splošne olajšave.
   */
  getSplošnaOlajsava(mesecniBruto: number): number {
    if (mesecniBruto <= 0) return 0;
    if (mesecniBruto <= 1402.67) {
      return 438.33 + (1644.75 - 1.17259 * mesecniBruto);
    }
    return 438.33;
  }

  /**
   * Izračuna dohodnino po lestvici za leto 2025.
   * @param osnova Osnova za dohodnino.
   * @returns Znesek akontacije dohodnine.
   */
  getDohodnina(osnova: number): number {
    if (osnova <= 0) return 0;
    if (osnova <= 767.52) return osnova * 0.16;
    if (osnova <= 2257.42) return 122.80 + (osnova - 767.52) * 0.26;
    if (osnova <= 3776.06) return 468.18 + (osnova - 2257.42) * 0.33;
    if (osnova <= 5394.69) return 878.55 + (osnova - 3776.06) * 0.39;
    return 1221.69 + (osnova - 5394.69) * 0.50;
  }
}