// Datoteka: src/app/pages/placilna-lista/placilna-lista.component.ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ZaposleniService, Placa, Zaposlen } from '../../services/zaposleni.service';
import html2pdf from 'html2pdf.js';
import { CommonModule } from '@angular/common'; // Potrebno za *ngIf in pipe

@Component({
  selector: 'app-placilna-lista',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './placilna-lista.component.html',
  styleUrls: ['./placilna-lista.component.css']
})
export class PlacilnaListaComponent implements OnInit {
  placa: Placa | undefined;
  // V prihodnosti bi lahko pridobili tudi podatke o zaposlenem
  // zaposlen: Zaposlen | undefined;

  constructor(
    private route: ActivatedRoute,
    private zaposleniService: ZaposleniService
  ) { }

  ngOnInit(): void {
    const placaId = this.route.snapshot.paramMap.get('id');
    if (placaId) {
      this.zaposleniService.getPlacaById(+placaId).subscribe(data => {
        this.placa = data;
        // Ko dobimo plačo, bi lahko pridobili še podatke o zaposlenem
        // this.zaposleniService.getZaposlenById(data.zaposlenId).subscribe(...);
      });
    }
  }

  public generirajPDF(): void {
    const element = document.getElementById('placinaListaContent');
    const options = {
      filename: `placilna_lista_${this.placa?.id}.pdf`,
      image: { type: 'jpeg', quality: 0.98 },
      html2canvas: { scale: 2 },
      jsPDF: { unit: 'in', format: 'letter', orientation: 'portrait' }
    };

    html2pdf().from(element).set(options).save();
  }
}