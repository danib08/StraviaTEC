import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { AthleteInCompetition } from 'src/app/Models/athlete-in-competition';
import { ParticipantsReport } from 'src/app/Models/participants-report';
import { GetService } from 'src/app/Services/Get/get-service';
import html2canvas from 'html2canvas';
import jsPDF from 'jspdf';

@Component({
  selector: 'app-competition-positions',
  templateUrl: './competition-positions.component.html',
  styleUrls: ['./competition-positions.component.css']
})
export class CompetitionPositionsComponent implements OnInit {

  competitionSelected = '';
  athleteCompetitions: AthleteInCompetition[] = [];
  athlete4Report: ParticipantsReport[] = [];
  constructor(private getService:GetService, private cookieSvc:CookieService) { }

  ngOnInit(): void {
    this.getAthleteCompetitions();
  }
  getAthleteCompetitions(){
    this.getService.getAthleteinCompetition(this.cookieSvc.get('Username')).subscribe(
      res=>{
        this.athleteCompetitions = res;
      },
      err=> {
        alert('Ha ocurrido un error')
      }
    );
  }
  getCurrentCompetitionReport(){
    this.getService.getPositionsReport(this.competitionSelected).subscribe(
      res=>{
        this.athlete4Report = res;
      },
      err=> {
        alert('Ha ocurrido un error')
      }
    );
  }
  downloadPDF() {
    const DATA = document.getElementById('htmlData') as HTMLCanvasElement;
    const doc = new jsPDF('p', 'pt', 'a4');
    const options = {
      background: 'white',
      scale: 3
    };
    html2canvas(DATA, options).then((canvas) => {

      const img = canvas.toDataURL('image/PNG');
      const bufferX = 15;
      const bufferY = 15;
      const imgProps = (doc as any).getImageProperties(img);
      const pdfWidth = doc.internal.pageSize.getWidth() - 2 * bufferX;
      const pdfHeight = (imgProps.height * pdfWidth) / imgProps.width;
      doc.addImage(img, 'PNG', bufferX, bufferY, pdfWidth, pdfHeight, undefined, 'FAST');
      return doc;
    }).then((docResult) => {
      docResult.save(`${new Date().toISOString()}_ReportePosiciones.pdf`);
    });
  }


}
