import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import jsPDF from 'jspdf';
import { CookieService } from 'ngx-cookie-service';
import { AthleteInCompetition } from 'src/app/Models/athlete-in-competition';
import { ParticipantsReport } from 'src/app/Models/participants-report';
import { GetService } from 'src/app/Services/Get/get-service';
import html2canvas from 'html2canvas';

@Component({
  selector: 'app-competition-participants',
  templateUrl: './competition-participants.component.html',
  styleUrls: ['./competition-participants.component.css']
})
export class CompetitionParticipantsComponent implements OnInit {

  competitionSelected = '';
  athleteCompetitions: AthleteInCompetition[] = [];
  athlete4Report: ParticipantsReport[] = [];
  Junior:ParticipantsReport[] = [];
  Sub23:ParticipantsReport[] = [];
  Open:ParticipantsReport[] = [];
  Elite:ParticipantsReport[] = [];
  MasterA:ParticipantsReport[] = [];
  MasterB:ParticipantsReport[] = [];
  MasterC:ParticipantsReport[] = [];

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
    this.Junior = [];
    this.Sub23 = [];
    this.Open = [];
    this.Elite = [];
    this.MasterA = [];
    this.MasterB = [];
    this.MasterC = [];
    this.getService.getAthletesReport(this.competitionSelected).subscribe(
      res=>{
        this.athlete4Report = res;
        this.organizeByCategories();
      },
      err=> {
        alert('Ha ocurrido un error')
      }
    );
  }

  organizeByCategories(){
    for(let i = 0; i < this.athlete4Report.length; i++){
      if(this.athlete4Report[i].category == 'Junior'){
        this.Junior.push(this.athlete4Report[i]);
      }
      else if(this.athlete4Report[i].category == 'Sub-23'){
        this.Sub23.push(this.athlete4Report[i]);
      }
      else if(this.athlete4Report[i].category == 'Open'){
        this.Open.push(this.athlete4Report[i]);
      }
      else if(this.athlete4Report[i].category == 'Elite'){
        this.Elite.push(this.athlete4Report[i]);
      }
      else if(this.athlete4Report[i].category == 'Master A'){
        this.MasterA.push(this.athlete4Report[i]);
      }
      else if(this.athlete4Report[i].category == 'Master B'){
        this.MasterB.push(this.athlete4Report[i]);
      }
      else if(this.athlete4Report[i].category == 'Master C'){
        this.MasterC.push(this.athlete4Report[i]);
      }
    }
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
      docResult.save(`${new Date().toISOString()}_ReporteParticipantes.pdf`);
    });
  }
}
