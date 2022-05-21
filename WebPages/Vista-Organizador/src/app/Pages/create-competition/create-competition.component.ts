import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { ActivityModel } from 'src/app/Models/activity-model';
import { Competition } from 'src/app/Models/competition';
import { Sponsor } from 'src/app/Models/sponsor';
import { GetService } from 'src/app/Services/Get/get-service';

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

  constructor(private formBuilder: FormBuilder, private getService: GetService) { }
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

  }

}
