import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { AthleteFriends } from 'src/app/Models/athlete-friends';
import { AthleteModel } from 'src/app/Models/athlete-model';
import { AthleteSearch } from 'src/app/Models/athlete-search';
import { PostService } from 'src/app/Services/Post/post-service';

@Component({
  selector: 'app-athletes-search',
  templateUrl: './athletes-search.component.html',
  styleUrls: ['./athletes-search.component.css']
})
export class AthletesSearchComponent implements OnInit {


  AthleteName: String = '';
  AthletesArray: AthleteModel[] = [];
  State=false;
  athleteSearch: AthleteSearch ={
    Name: '',
    Lastname: ''
  }

  athleteFriend: AthleteFriends = {
    AthleteID: '',
    FriendID: ''
  }
  constructor(private postService: PostService, private cookieSvc:CookieService) { }

  ngOnInit(): void {
  }

  getAthletes(){
    var splitted = this.AthleteName.split(" ", 2); 
    this.athleteSearch.Name = splitted[0];
    this.athleteSearch.Lastname = splitted[1];
    this.postService.searchAthletes(this.athleteSearch).subscribe(
      res => {
        this.AthletesArray = res;
        this.State = true;
      },
      err => {
        alert('Ha ocurrido un error')
      }
    );
  }

  followAthlete(Username:string){
    this.athleteFriend.AthleteID = this.cookieSvc.get('Username');
    this.athleteFriend.FriendID = Username;
    this.postService.addFollower(this.athleteFriend).subscribe(
      res => {
        console.log(res);
      },
      err => {
        alert('No se pudo seguir al atleta')
      }
    );
  }
}
