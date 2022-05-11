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
export class SignUpComponent implements OnInit {

  public files: any = [];
  imageSrc: string = '';
  constructor(private router: Router, private postSvc: PostService) { }

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
  
  ngOnInit(): void {
  }

  signIn() {
    this.router.navigate(["login"]);
  }

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

  signUpAthlete(){
    this.postSvc.signUpAthlete(this.newAthlete).subscribe(
      res =>{
        console.log(res);
      }
    );
  }
  

}
