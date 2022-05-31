import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { AthleteModel } from 'src/app/Models/athlete-model';
import { PostService } from 'src/app/Services/Post/post-service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private router: Router, private postSvc: PostService, private cookieSvc:CookieService) {}

  // Athlete model for the Log In functionality
  newAthlete: AthleteModel = {
    Name: '',
    Lastname: '',
    Birthdate: '',
    Nationality: '',
    Age: 0,
    Pass: '',
    Category: '',
    Photo: '',
    Username: ''
  }

  ngOnInit(): void {}

  /**
   * Navigates to the Sign Up page
   */
  signUp() {
    this.router.navigate(["signup"]);
  }

  /**
   * Makes a POST request to the API with the login info the
   * athlete (user) entered
   */
  logInAthlete(){
    this.postSvc.signInAthlete(this.newAthlete).subscribe(
      res =>{
        console.log(res);
        this.cookieSvc.set('Username', this.newAthlete.Username);
      },err => {
        alert('Contraseña o Username incorrectos')
      }
    );
  }
}
