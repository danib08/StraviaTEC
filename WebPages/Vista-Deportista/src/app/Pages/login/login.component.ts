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

  constructor(private router: Router, private postSvc: PostService, private cookieSvc:CookieService) {}

  // Athlete model for the Log In functionality
  newAthlete: AthleteModel = {
    username: '',
    name: '',
    lastName: '',
    photo: '',
    age: 0,
    birthDate: '2022-05-31T04:35:02.318Z',
    pass: '',
    nationality: '',
    category: ''
  }

  // Filled with validation from the API
  validation = {
    Existe: ""
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
    console.log(this.newAthlete);
    this.postSvc.signInAthlete(this.newAthlete).subscribe(
      res =>{
        this.validation = res;
        console.log(res);

        if (this.validation.Existe == "Si"){
          alert("Inicio de sesión exitoso");

          // Saves username to a cookie
          this.cookieSvc.set('Username', this.newAthlete.username);
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
