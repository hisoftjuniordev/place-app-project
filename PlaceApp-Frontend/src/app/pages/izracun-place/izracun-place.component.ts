// Datoteka: src/app/pages/izracun-place/izracun-place.component.ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ZaposleniService, Placa } from '../../services/zaposleni.service'; // Prilagojen import
import { PlacaKonfiguracijaService } from '../../services/placa-konfiguracija.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-izracun-place',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './izracun-place.component.html',
  styleUrls: ['./izracun-place.component.css']
})
export class IzracunPlaceComponent implements OnInit {
  zaposlenId: number = 0;
  placaForm: FormGroup;
  rezultat: any = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private zaposleniService: ZaposleniService,
    private konfig: PlacaKonfiguracijaService,
    private fb: FormBuilder
  ) {
    this.placaForm = this.fb.group({
      obdobje: [new Date().toISOString().slice(0, 7), Validators.required],
      brutoPlacaVnos: [0],
      urnaPostavka: [10.50],
      rednoDeloUre: [168],
      placaniPraznikiUre: [0],
      poracun: [0],
      stimulacija: [0],
      delovnaDobaProcent: [8.5],
      posebneOlajsave: [0],
      odtegljaji: [0],
      prehranaDni: [21],
      prehranaZnesek: [7.96],
      prevozKm: [15],
      kmCena: [0.21],
      kilometrineFiksno: [0],
    });
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.zaposlenId = +id;
    }
  }

  izracunajInShrani(): void {
    if (this.placaForm.invalid) return;
    const v = this.placaForm.value;
    
    // --- Logika izračuna ---
    let brutoPlaca, znesekRednoDelo, znesekPrazniki, znesekDelovnaDoba;
    if (v.brutoPlacaVnos > 0) {
      brutoPlaca = v.brutoPlacaVnos;
    } else {
      znesekRednoDelo = v.urnaPostavka * v.rednoDeloUre;
      znesekPrazniki = v.urnaPostavka * v.placaniPraznikiUre;
      const osnovaZaMinuloDelo = znesekRednoDelo + znesekPrazniki + v.stimulacija;
      znesekDelovnaDoba = osnovaZaMinuloDelo * (v.delovnaDobaProcent / 100);
      brutoPlaca = znesekRednoDelo + znesekPrazniki + v.poracun + v.stimulacija + znesekDelovnaDoba;
    }
    
    // <<< POPRAVEK TUKAJ (1. DEL) >>>
    const prispevekPIZ = brutoPlaca * this.konfig.STOPNJA_PIZ;
    const prispevekZZ = brutoPlaca * this.konfig.STOPNJA_ZZ;
    const prispevekZaposlovanje = brutoPlaca * this.konfig.STOPNJA_ZAPOSLOVANJE;
    const prispevekPorodniska = brutoPlaca * this.konfig.STOPNJA_PORODNISKA;
    const skupajPrispevkiIzBruto = this.konfig.OZZ_FIKSNI + prispevekPIZ + prispevekZZ + prispevekZaposlovanje + prispevekPorodniska;

    const splosnaOlajsava = this.konfig.getSplošnaOlajsava(brutoPlaca);
    const osnovaZaDohodnino = Math.max(0, brutoPlaca - skupajPrispevkiIzBruto - splosnaOlajsava - v.posebneOlajsave);
    const dohodnina = this.konfig.getDohodnina(osnovaZaDohodnino);
    
    const netoPlaca = brutoPlaca - skupajPrispevkiIzBruto - dohodnina;
    
    const stroskiPrehrana = v.prehranaDni * v.prehranaZnesek;
    const stroskiPrevoz = v.prevozKm * v.prehranaDni * 2 * v.kmCena;
    const skupajMaterialniStroski = stroskiPrehrana + stroskiPrevoz + v.kilometrineFiksno;
    const zaIzplacilo = netoPlaca - v.odtegljaji + skupajMaterialniStroski;

    // <<< POPRAVEK TUKAJ (2. DEL) >>>
    const prispevkiNaBruto = brutoPlaca * (this.konfig.STOPNJA_PIZ_NA_BRUTO + this.konfig.STOPNJA_ZZ_NA_BRUTO + this.konfig.STOPNJA_ZAPOSLOVANJE_NA_BRUTO + this.konfig.STOPNJA_PORODNISKA_NA_BRUTO + this.konfig.STOPNJA_POSKODBE_DELO);
    const celotenStrosekDelodajalca = brutoPlaca + prispevkiNaBruto + skupajMaterialniStroski;

    this.rezultat = { 
        brutoPlaca, dohodnina, netoPlaca, zaIzplacilo, celotenStrosekDelodajalca,
        skupajPrispevkiIzBruto, skupajMaterialniStroski, odtegljaji: v.odtegljaji
    };
    
    const novaPlaca: Omit<Placa, 'id'> = {
      zaposlenId: this.zaposlenId,
      obdobje: new Date(v.obdobje).toISOString(),
      urnaPostavka: v.urnaPostavka,
      rednoDeloUre: v.rednoDeloUre,
      placaniPraznikiUre: v.placaniPraznikiUre,
      poracun: v.poracun,
      stimulacija: v.stimulacija,
      delovnaDobaProcent: v.delovnaDobaProcent,
      posebneOlajsave: v.posebneOlajsave,
      odtegljaji: v.odtegljaji,
      prehranaDni: v.prehranaDni,
      prehranaZnesek: v.prehranaZnesek,
      prevozKm: v.prevozKm,
      kmCena: v.kmCena,
      kilometrineFiksno: v.kilometrineFiksno,
      brutoPlaca, netoPlaca, dohodnina, zaIzplacilo, celotenStrosekDelodajalca
    };

    this.zaposleniService.addPlaca(novaPlaca).subscribe({
      next: () => {
        alert("Plača uspešno shranjena!");
        this.router.navigate(['/zaposleni']);
      },
      error: (err) => {
        console.error("Napaka pri shranjevanju plače", err);
        alert("Napaka pri shranjevanju!");
      }
    });
  }
}