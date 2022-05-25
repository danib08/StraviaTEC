import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormArray, Validators } from '@angular/forms';
import { CookieService } from 'ngx-cookie-service';
import { ActivityModel } from 'src/app/Models/activity-model';
import { Challenge } from 'src/app/Models/challenge';
import { GetService } from 'src/app/Services/Get/get-service';
import { PostService } from 'src/app/Services/Post/post-service';

@Component({
  selector: 'app-create-challenge',
  templateUrl: './create-challenge.component.html',
  styleUrls: ['./create-challenge.component.css']
})
export class CreateChallengeComponent implements OnInit {

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
  challenge:Challenge = {
    ID: '',
    Name: '',
    EndDate: '',
    StartDate: '',
    Privacy: '',
    Kilometers: 0,
    Type: ''
  }
  constructor(private formBuilder: FormBuilder, private getService: GetService, private cookieSvc:CookieService, private postService: PostService) { }

  ngOnInit(): void {
  }


  registerFormS = this.formBuilder.group({
    ID: '',
    Name: '',
    BankAccount: '',
    CompetitionID: '',
    ChallengeID: ''
  });

  registerFormS2 = this.formBuilder.group({
    Sponsors: this.formBuilder.array([], Validators.required)
  });
  
  get sponsors(){
    return this.registerFormS2.get('Sponsors') as FormArray;
  }

  addSponsor(){
    const SponsorsFormGroup = this.formBuilder.group({
      ID: '',
      Name: '',
      BankAccount: '',
      CompetitionID: '',
      ChallengeID: ''
    });
    this.sponsors.push(SponsorsFormGroup);
  }

  removeSponsor(index : number){
    this.sponsors.removeAt(index);
  }

  addChallenge(){
    this.associatedActivity.Name = this.challenge.Name;
    this.associatedActivity.Kilometers = this.challenge.Kilometers;
    this.associatedActivity.AthleteUsername = this.cookieSvc.get('Username');
    this.postService.createActivity(this.associatedActivity).subscribe(
      res =>{
      },
      err=>{
        alert('Ha ocurrido un error')
      }
    );

    this.postService.createChallenge(this.challenge).subscribe(
      res =>{
      },
      err=>{
        alert('Ha ocurrido un error')
      }
    );

    this.registerFormS.get('CompetitionID')?.setValue(this.challenge.ID);
    this.postService.createSponsor(this.registerFormS.value).subscribe(
      res =>{
      },
      err=>{
        alert('Ha ocurrido un error')
      }
    );

    for(let i = 0; i < this.sponsors.length; i++){
      this.sponsors.at(i).get('ChallengeID')?.setValue(this.challenge.ID);
      this.postService.createSponsor(this.sponsors.at(i).value).subscribe(
        res =>{
        },
        err=>{
          alert('Ha ocurrido un error')
        }
      );
    }
    
  }
}
