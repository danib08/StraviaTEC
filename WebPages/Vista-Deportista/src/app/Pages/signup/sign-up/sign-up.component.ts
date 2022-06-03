import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { AthleteModel } from 'src/app/Models/athlete-model';
import { PostService } from 'src/app/Services/Post/post-service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})

/**
 * Sign Up component where the user registers itself on the social app
 */
export class SignUpComponent implements OnInit {

  public files: any = [];
  imageSrc: string = '';

  // Athlete model for the Sign Up functionality
  newAthlete: AthleteModel = {
    username: '',
    name: '',
    lastname: '',
    photo: '',
    age: 0,
    birthdate: '',
    pass: '',
    nationality: '',
    category: ''
  }
  
  /**
   * Creates the Sign Up component
   * @param router used to re-route the user to different pages
   * @param postSvc service for POST requests to the API
   */
  constructor(private router: Router, private postSvc: PostService) { }

  /**
   * Called after Angular has initialized all data-bound properties
   */
  ngOnInit(): void {}

  /**
   * Navigates to the Sign In page
   */
  signIn() {
    this.router.navigate(["login"]);
  }

  /**
   * Receives image uploaded by user, saves it to the
   * Athlete model and then displays it
   * @param event triggered by the file upload
   */
  onFileChange(event:any) {
    const reader = new FileReader();
     
    if(event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);
     
      reader.onload = () => { 
        this.imageSrc = reader.result as string;
        this.newAthlete.photo = this.imageSrc;
      };   
    }
  }

  /**
   * Makes a POST request to the API with the sign up info the
   * athlete (user) entered
   */
  signUpAthlete(){
    this.postSvc.signUpAthlete(this.newAthlete).subscribe(
      res =>{
        if (res == "") {
          this.router.navigate(["login"]);
        }
        else {
          if (res[0].message_id == 2601) {
            alert("El nombre de usuario ingresado ya existe.");
          }
        }
      }
    );
  }
}
