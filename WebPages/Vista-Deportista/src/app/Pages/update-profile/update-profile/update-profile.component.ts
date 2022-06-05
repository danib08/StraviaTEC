import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { AthleteModel } from 'src/app/Models/athlete-model';
import { DeleteService } from 'src/app/Services/Delete/delete-service';
import { GetService } from 'src/app/Services/Get/get-service';
import { PutService } from 'src/app/Services/Put/put-service';

@Component({
  selector: 'app-update-profile',
  templateUrl: './update-profile.component.html',
  styleUrls: ['./update-profile.component.css']
})

/**
* Update Profile component where the user is able to change its
* info and delete its account
*/
export class UpdateProfileComponent implements OnInit {

  public files: any = [];
  imageSrc: string = '';

  // Saves the current information of the user
  athlete: AthleteModel = {
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
   * 
   * @param getSvc service for GET requests to the API
   * @param putSvc service for PUT requests to the API
   * @param deleteSvc service for DELETE requests to the API 
   * @param cookieSvc service for cookie creating to store the username 
   * @param router used to re-route the user to different pages
   */
  constructor(private getSvc: GetService, private putSvc: PutService, 
    private deleteSvc: DeleteService, private cookieSvc: CookieService, private router: Router) { }

  /**
   * Called after Angular has initialized all data-bound properties
   */
  ngOnInit(): void {
    this.getProfile();
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
        var splitted = this.imageSrc.split(",", 2); 
        this.athlete.photo = splitted[1];
      };   
    }
  }
  
  /**
   * Makes a GET request to the API to retrieve the current
   * info of the user
   */
  getProfile() {
    this.getSvc.getAthlete(this.cookieSvc.get("Username")).subscribe(
      res =>{
        this.athlete = res;
      }, err => {
        alert("Ha ocurrido un error obteniendo tu información.");
      }
    );
  }

  /**
   * Makes a PUT request to the API with the new info the 
   * user desires to modify
   */
  updateProfile(){
    this.putSvc.updateAthlete(this.athlete).subscribe(
      res =>{
        alert("Cambios guardados exitosamente.");
      }, err => {
        alert("Ha ocurrido un error");
      }
    );
  }

   /**
   * Makes a DELETE request to the API to permanently delete
   * the athlete's account
   */
    deleteProfile(){
      this.deleteSvc.deleteAthlete(this.athlete.username).subscribe(
        res =>{
          this.cookieSvc.delete("Username");
          alert("Cuenta borrada permanentemente.");
          this.router.navigate(["login"]);
        }, err => {
          alert("Ha ocurrido un error");
        }
      );
    }
}
