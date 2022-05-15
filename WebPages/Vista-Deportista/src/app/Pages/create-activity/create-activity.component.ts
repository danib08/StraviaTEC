import { Component, OnInit } from '@angular/core';
import { ActivityModel } from 'src/app/Models/activity-model';
import { PostService } from 'src/app/Services/Post/post-service';


@Component({
  selector: 'app-create-activity',
  templateUrl: './create-activity.component.html',
  styleUrls: ['./create-activity.component.css']
})

export class CreateActivityComponent implements OnInit {

  isEvent = false
  activity: ActivityModel = {
    ID: 0,
    Name: '',
    Route: '',
    Date: '',
    Duration: 0, 
    Kilometers: 0,
    Type: '',
    ChallengeID: 0
}

  constructor(private postService: PostService) { }

  ngOnInit(): void {
  }

  createActivity(){
    this.postService.createActivity(this.activity).subscribe(
      res =>{
        location.reload();
      }, err => {
        alert("Ha ocurrido un error")
      }
    );
  }

  radioSelect(event: Event) {
      var value = (event.target as HTMLInputElement).value;

      if (value == "yes") {
        this.isEvent = false
      }
      else {
        this.isEvent = true
      }
  }

}
