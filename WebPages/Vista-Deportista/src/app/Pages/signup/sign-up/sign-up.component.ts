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
  constructor(private router: Router, private postSvc: PostService) { }

  // Athlete model for the Sign Up functionality
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

  
  ngOnInit(): void {
  }

  /**
   * Navigates to the Sign In page
   */
  signIn() {
    this.router.navigate(["login"]);
  }

  /**
   * Receives image uploaded by user, saves it to the
   * Athlete model and then displays it
   * @param event 
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
        console.log(res);
      }
    );
  }
}
