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


  sponsor:Sponsor = {
    ID: '',
    Name: '',
    BankAccount: '',
    CompetitionID: '',
    ChallengeID: ''
  }
  associatedActivity: ActivityModel = {
    ID: '',
    Name: '',
    Route: '',
    Date: '',
    Duration: '',
    Kilometers: 0,
    Type: '',
    AthleteUsername: ''
  } 
  competition: Competition = {
    ID: '',
    Name: '',
    Route: '',
    Date: '',
    Privacy: '',
    BankAccount: '',
    Price: 0,
    ActivityID: ''
  }

  constructor(private formBuilder: FormBuilder, private getService: GetService, private cookieSvc:CookieService, private postService: PostService) { }
  ngOnInit(): void {
  }

  registerForm = this.formBuilder.group({
    CompetitionID: '',
    Category: ''
  });
  
  registerForm2 = this.formBuilder.group({
    Categories: this.formBuilder.array([], Validators.required)
  });

  get categories(){
    return this.registerForm2.get('Categories') as FormArray;
  }
  
  addCategories(){
    const CategoriesFormGroup = this.formBuilder.group({
      CompetitionID: '',
      Category: ''
    });
    this.categories.push(CategoriesFormGroup);
  }

  removeCategory(index : number){
    this.categories.removeAt(index);
  }

  addCompetition(){
    this.associatedActivity.Name = this.competition.Name;
    this.associatedActivity.Route = this.competition.Route;
    this.associatedActivity.Date = this.competition.Date;
    this.associatedActivity.AthleteUsername = this.cookieSvc.get('Username');
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

    this.sponsor.CompetitionID = this.competition.ID;
    this.postService.createSponsor(this.sponsor).subscribe(
      res =>{
      },
      err=>{
        alert('Ha ocurrido un error')
      }
    );

    this.registerForm.get('CompetitionID')?.setValue(this.competition.ID);
    this.postService.createCompetitionCategories(this.registerForm.value).subscribe(
      res =>{
      },
      err=>{
        alert('Ha ocurrido un error')
      }
    );

    for(let i = 0; i < this.categories.length; i++){
      this.categories.at(i).get('CompetitionID')?.setValue(this.competition.ID);
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
