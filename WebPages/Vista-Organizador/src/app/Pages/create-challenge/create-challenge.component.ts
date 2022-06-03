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
    id: '',
    name: '',
    route: '',
    date: '',
    duration: '',
    kilometers: 0,
    type: '',
    athleteusername: ''
  } 
  
  challenge:Challenge = {
    id: '',
    name: '',
    enddate: '',
    startdate: '',
    privacy: '',
    kilometers: 0,
    type: ''
  }
  constructor(private formBuilder: FormBuilder, private getService: GetService, private cookieSvc:CookieService, private postService: PostService) { }

  ngOnInit(): void {
  }




  addChallenge(){
    this.associatedActivity.name = this.challenge.name;
    this.associatedActivity.kilometers = this.challenge.kilometers;
    this.associatedActivity.athleteusername = this.cookieSvc.get('Username');
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
    
  }
}
