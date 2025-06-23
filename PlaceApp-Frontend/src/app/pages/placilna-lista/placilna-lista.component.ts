// Datoteka: src/app/pages/placilna-lista/placilna-lista.component.ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule, DatePipe } from '@angular/common';
import { Placa, ZaposleniService } from '../../services/zaposleni.service';
import { PlacaKonfiguracijaService } from '../../services/placa-konfiguracija.service';
import * as html2pdf from 'html2pdf.js';

@Component({
  selector: 'app-placilna-lista',
  standalone: true,
  imports: [CommonModule, DatePipe],
  templateUrl: './placilna-lista.component.html',
  styleUrls: ['./placilna-lista.component.css']
})
export class PlacilnaListaComponent implements OnInit {
  placa: Placa | undefined;
  izracunPodrobnosti: any = null; // Objekt za shranjevanje vmesnih izračunov

  // V prihodnosti bi lahko tukaj shranili tudi podatke o zaposlenem in podjetju
  // zaposlen: Zaposlen | undefined;
  // podjetje: Podjetje | undefined;

  constructor(
    private route: ActivatedRoute,
    private zaposleniService: ZaposleniService,
    private konfig: PlacaKonfiguracijaService // Vbrizgamo konfiguracijski servis
  ) { }

  ngOnInit(): void {
    const placaId = this.route.snapshot.paramMap.get('id');
    if (placaId) {
      this.zaposleniService.getPlacaById(+placaId).subscribe(data => {
        this.placa = data;
        // Ko dobimo podatke, zaženemo izračun za prikaz podrobnosti
        this.izracunajPodrobnosti(data);
      });
    }
  }

  izracunajPodrobnosti(p: Placa): void {
    // Ponovimo logiko izračuna, da dobimo vse vmesne vrednosti
    const prispevekPIZ = p.brutoPlaca * this.konfig.STOPNJA_PIZ;
    const prispevekZZ = p.brutoPlaca * this.konfig.STOPNJA_ZZ;
    const prispevekZaposlovanje = p.brutoPlaca * this.konfig.STOPNJA_ZAPOSLOVANJE;
    const prispevekPorodniska = p.brutoPlaca * this.konfig.STOPNJA_PORODNISKA;
    const skupajPrispevkiIzBruto = this.konfig.OZZ_FIKSNI + prispevekPIZ + prispevekZZ + prispevekZaposlovanje + prispevekPorodniska;

    const splosnaOlajsava = this.konfig.getSplošnaOlajsava(p.brutoPlaca);
    const osnovaZaDohodnino = Math.max(0, p.brutoPlaca - skupajPrispevkiIzBruto - splosnaOlajsava - p.posebneOlajsave);

    const stroskiPrehrana = p.prehranaDni * p.prehranaZnesek;
    const stroskiPrevoz = p.prevozKm * p.prehranaDni * 2 * p.kmCena;
    const skupajMaterialniStroski = stroskiPrehrana + stroskiPrevoz + p.kilometrineFiksno;
    
    // Prispevki delodajalca
    const prispevekPIZ_naBruto = p.brutoPlaca * this.konfig.STOPNJA_PIZ_NA_BRUTO;
    const prispevekZZ_naBruto = p.brutoPlaca * this.konfig.STOPNJA_ZZ_NA_BRUTO;
    const prispevekZaposlovanje_naBruto = p.brutoPlaca * this.konfig.STOPNJA_ZAPOSLOVANJE_NA_BRUTO;
    const prispevekPorodniska_naBruto = p.brutoPlaca * this.konfig.STOPNJA_PORODNISKA_NA_BRUTO;
    const prispevekPoskodbeDelo = p.brutoPlaca * this.konfig.STOPNJA_POSKODBE_DELO;
    const skupajPrispevkiNaBruto = prispevekPIZ_naBruto + prispevekZZ_naBruto + prispevekZaposlovanje_naBruto + prispevekPorodniska_naBruto + prispevekPoskodbeDelo;

    this.izracunPodrobnosti = {
      prispevekPIZ, prispevekZZ, prispevekZaposlovanje, prispevekPorodniska,
      OZZ_FIKSNI: this.konfig.OZZ_FIKSNI,
      skupajPrispevkiIzBruto,
      splosnaOlajsava,
      osnovaZaDohodnino,
      stroskiPrehrana,
      stroskiPrevoz,
      skupajMaterialniStroski,
      skupajPrispevkiNaBruto
    };
  }

  public generirajPDF(): void {
    const element = document.getElementById('placilnaListaContent');
    const options = {
      filename: `placilna_lista_${this.placa?.id}.pdf`,
      image: { type: 'jpeg', quality: 0.98 },
      html2canvas: { scale: 2 },
      jsPDF: { unit: 'in', format: 'a4', orientation: 'portrait' }
    };
    html2pdf().from(element).set(options).save();
  }
}