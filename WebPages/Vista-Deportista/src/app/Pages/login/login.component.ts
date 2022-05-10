import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AthleteModel } from 'src/app/Models/athlete-model';
import { PostService } from 'src/app/Services/Post/post-service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private router: Router, private postSvc: PostService) {}

  newAthlete: AthleteModel = {
    name: '',
    lastname: '',
    birthdate: '',
    nationality: '',
    age: 0,
    pass: '',
    category: '',
    photo: '',
    username: ''
  }

  ngOnInit(): void {}

  signUp() {
    this.router.navigate(["signup"]);
  }

  logInAthlete(){
    this.postSvc.signInAthlete(this.newAthlete).subscribe(
      res =>{
        console.log(res);
      }
    );
  }
}
