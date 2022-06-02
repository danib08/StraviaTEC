import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Group } from 'src/app/Models/group';
import { DeleteService } from 'src/app/Services/Delete/delete-service';
import { GetService } from 'src/app/Services/Get/get-service';
import { PostService } from 'src/app/Services/Post/post-service';
import { PutService } from 'src/app/Services/Put/put-service';

@Component({
  selector: 'app-create-group',
  templateUrl: './create-group.component.html',
  styleUrls: ['./create-group.component.css']
})
export class CreateGroupComponent implements OnInit {

  group:Group = {
    Name: '',
    AdminUsername: '',
  }

  editedGroup:Group = {
    Name: '',
    AdminUsername: '',
    OlderName: ''
  }

  groupSelected = '';

  groupsArray: Group[] = [];

  constructor(private cookieSvc:CookieService, private putService:PutService, private getService:GetService, private postService:PostService, private deleteService:DeleteService,) { }

  ngOnInit(): void {
    this.getGroups();
  }

  addGroup(){
    this.group.AdminUsername = this.cookieSvc.get('Username')
    this.postService.createGroup(this.group).subscribe(
      res=>{
        location.reload()
      },
      err=>{
        alert('Ha ocurrido un error')
      }
    );
  }

  getCurrentGroup(){
    for(let i = 0; i < this.groupsArray.length; i++){
      if(this.groupsArray[i].Name == this.groupSelected){
        this.editedGroup = this.groupsArray[i];
        break;
      }
    }
  }

  deleteGroup(){
    console.log(this.groupSelected)
    this.deleteService.deleteGroup(this.groupSelected).subscribe(
      res=>{
        location.reload()
      },
      err=>{
        alert('Ha ocurrido un error')
      }
    );
  }

  changeGroup(){
    this.editedGroup.AdminUsername = this.cookieSvc.get('Username');
    this.editedGroup.OlderName = this.groupSelected;
    this.putService.modifyGroup(this.editedGroup).subscribe(
      res=>{
        location.reload()
      },
      err=>{
        alert('Ha ocurrido un error')
      }
    );
  }

  getGroups(){
    this.getService.getAthleteCreatedGroups(this.cookieSvc.get('Username')).subscribe(
      res=>{
        this.groupsArray = res;
      },
      err=>{
        alert('Ha ocurrido un error')
      }
    );
  }

}
