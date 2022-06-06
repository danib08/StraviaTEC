import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { CookieService } from 'ngx-cookie-service';
import { ActivityModel } from 'src/app/Models/activity-model';
import { Competition } from 'src/app/Models/competition';
import { Sponsor } from 'src/app/Models/sponsor';
import { DeleteService } from 'src/app/Services/Delete/delete-service';
import { GetService } from 'src/app/Services/Get/get-service';
import { PostService } from 'src/app/Services/Post/post-service';
import { PutService } from 'src/app/Services/Put/put-service';

@Component({
  selector: 'app-modify-competition',
  templateUrl: './modify-competition.component.html',
  styleUrls: ['./modify-competition.component.css']
})
export class ModifyCompetitionComponent implements OnInit {

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
  competitionSelected = '';
  competitionsArray: Competition[] = [];

  constructor(private formBuilder: FormBuilder, private getService: GetService, private cookieSvc:CookieService, private postService: PostService, private putService: PutService, private deleteService: DeleteService) { }

  ngOnInit(): void {
    this.getAllCompetitions(this.cookieSvc.get('Username'));
  }

  registerForm = this.formBuilder.group({
    competitionid: '',
    category: ''
  });
  
  registerForm2 = this.formBuilder.group({
    categories: this.formBuilder.array([], Validators.required)
  });

  get categories(){
    return this.registerForm2.get('categories') as FormArray;
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

  modifyCompetition(){
    this.putService.modifyActivity(this.associatedActivity).subscribe(
      res => {

      },
      err=>{
        alert('Ha ocurrido un error')
      }
    );

    this.putService.modifyCompetition(this.competition).subscribe(
      res => {
      },
      err=>{
        alert('Ha ocurrido un error')
      }
    );

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


  getActivity(){
    this.getService.getActivity(this.competition.activityid).subscribe(
      res => {
        this.associatedActivity = res;
      },
      err=>{
        alert('Ha ocurrido un error')
      }
    );
  }

  getAllCompetitions(AthleteID:string){
    this.getService.getAthleteCreatedCompetitions(AthleteID).subscribe(
      res => {
        this.competitionsArray = res;
      },
      err=>{
        alert('Ha ocurrido un error')
      }
    );
  }

  getCurrentCompetition(){
    for(let i = 0; i < this.competitionsArray.length; i++){
      if(this.competitionsArray[i].id == this.competitionSelected){
        this.competition = this.competitionsArray[i];
        break;
      }
    }
    this.getActivity();
  }
  
  deleteCompetition(){
    this.deleteService.deleteCompetition(this.competition.id).subscribe(
      res =>{
        location.reload()
      },
      err=>{
        alert('Ha ocurrido un error')
      }
    );
  }
}
