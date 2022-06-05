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

/**
 * Log In component where the user inputs its username and password
 */
export class LoginComponent implements OnInit {

  // Athlete model for the Log In functionality
  newAthlete: AthleteModel = {
    username: '',
    name: '',
    lastname: '',
    photo: '',
    age: 0,
    birthdate: '2022-06-04T23:52:43.803Z',
    pass: '',
    nationality: '',
    category: ''
  }

  /**
   * Creates the Log In component
   * @param router used to re-route the user to different pages
   * @param postSvc service for POST requests to the API
   * @param cookieSvc service for cookie creating to store the username
   */
  constructor(private router: Router, private postSvc: PostService, private cookieSvc:CookieService) {}

  // Filled with validation from the API
  validation = {
    Existe: ""
  }

  /**
   * Called after Angular has initialized all data-bound properties
   */
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
        this.validation = res;

        if (this.validation.Existe == "Si"){
          alert("Inicio de sesión exitoso");

          // Saves username to a cookie
          this.cookieSvc.set('Username', this.newAthlete.username);
          this.router.navigate(["feed"]);
        }
        else {
          alert("Nombre de usuario o contraseña incorrectos");
        }
      },
      err => {
        alert('Error')
      }
    );
  }
}
