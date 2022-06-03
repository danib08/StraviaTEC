import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { CookieService } from 'ngx-cookie-service';
import { ActivityModel } from 'src/app/Models/activity-model';
import { Competition } from 'src/app/Models/competition';
import { Sponsor } from 'src/app/Models/sponsor';
import { GetService } from 'src/app/Services/Get/get-service';
import { PostService } from 'src/app/Services/Post/post-service';

@Component({
  selector: 'app-create-competition',
  templateUrl: './create-competition.component.html',
  styleUrls: ['./create-competition.component.css']
})
export class CreateCompetitionComponent implements OnInit {


  associatedActivity: ActivityModel = {
    id: '',
    name: '',
    route: '',
    date: '',
    duration: '',
    kilometers: 0,
    type: '',
    athleteusername: ''
  } 
  competition: Competition = {
    id: '',
    name: '',
    route: '',
    date: '',
    privacy: '',
    bankaccount: '',
    price: 0,
    activityid: ''
  }

  constructor(private formBuilder: FormBuilder, private getService: GetService, private cookieSvc:CookieService, private postService: PostService) { }
  ngOnInit(): void {
  }

  registerForm = this.formBuilder.group({
    competitionid: '',
    category: ''
  });
  
  registerForm2 = this.formBuilder.group({
    Categories: this.formBuilder.array([], Validators.required)
  });

  registerFormS = this.formBuilder.group({
    id: '',
    name: '',
    bankaccount: '',
    competitionid: '',
    challengeid: ''
  });

  registerFormS2 = this.formBuilder.group({
    Sponsors: this.formBuilder.array([], Validators.required)
  });

  get categories(){
    return this.registerForm2.get('Categories') as FormArray;
  }

  get sponsors(){
    return this.registerFormS2.get('Sponsors') as FormArray;
  }

  addSponsor(){
    const SponsorsFormGroup = this.formBuilder.group({
      id: '',
      name: '',
      bankaccount: '',
      competitionid: '',
      challengeid: ''
    });
    this.sponsors.push(SponsorsFormGroup);
  }

  removeSponsor(index : number){
    this.sponsors.removeAt(index);
  }

  addCategories(){
    const CategoriesFormGroup = this.formBuilder.group({
      competitionid: '',
      category: ''
    });
    this.categories.push(CategoriesFormGroup);
  }

  removeCategory(index : number){
    this.categories.removeAt(index);
  }

  addCompetition(){
    this.associatedActivity.name = this.competition.name;
    this.associatedActivity.route = this.competition.route;
    this.associatedActivity.date = this.competition.date;
    this.associatedActivity.athleteusername = this.cookieSvc.get('Username');
    this.postService.createActivity(this.associatedActivity).subscribe(
      res =>{
      },
      err=>{
        alert('Ha ocurrido un error')
      }
    );

    this.postService.createCompetition(this.competition).subscribe(
      res =>{
      },
      err=>{
        alert('Ha ocurrido un error')
      }
    );

    this.registerFormS.get('competitionid')?.setValue(this.competition.id);
    this.postService.createSponsor(this.registerFormS.value).subscribe(
      res =>{
      },
      err=>{
        alert('Ha ocurrido un error')
      }
    );

    for(let i = 0; i < this.sponsors.length; i++){
      this.sponsors.at(i).get('competitionid')?.setValue(this.competition.id);
      this.postService.createSponsor(this.sponsors.at(i).value).subscribe(
        res =>{
        },
        err=>{
          alert('Ha ocurrido un error')
        }
      );
    }






    this.registerForm.get('competitionid')?.setValue(this.competition.id);
    this.postService.createCompetitionCategories(this.registerForm.value).subscribe(
      res =>{
      },
      err=>{
        alert('Ha ocurrido un error')
      }
    );

    for(let i = 0; i < this.categories.length; i++){
      this.categories.at(i).get('competitionid')?.setValue(this.competition.id);
      this.postService.createCompetitionCategories(this.categories.at(i).value).subscribe(
        res =>{
        },
        err=>{
          alert('Ha ocurrido un error')
        }
      );
    }
    
  }

}
