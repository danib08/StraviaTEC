import { Component, OnInit } from '@angular/core';
import { AthleteModel } from 'src/app/Models/athlete-model';
import { PutService } from 'src/app/Services/Put/put-service';

@Component({
  selector: 'app-update-profile',
  templateUrl: './update-profile.component.html',
  styleUrls: ['./update-profile.component.css']
})
export class UpdateProfileComponent implements OnInit {

  public files: any = [];
  imageSrc: string = '';

  athlete: AthleteModel = {
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

  constructor(private putService: PutService) { }

  ngOnInit(): void {
  }

  onFileChange(event:any) {
    const reader = new FileReader();
     
    if(event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);
     
      reader.onload = () => { 
        this.imageSrc = reader.result as string;
        this.athlete.photo = this.imageSrc;
      };   
    }
  }
  
  updateProfile(){
    this.putService.updateAthlete(this.athlete).subscribe(
      res =>{
        location.reload();
      }, err => {
        alert("Ha ocurrido un error")
      }
    );
  }
}
