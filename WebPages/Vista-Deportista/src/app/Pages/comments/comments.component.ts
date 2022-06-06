import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { CommentModel } from 'src/app/Models/comment-model';
import { GetService } from 'src/app/Services/Get/get-service';
import { PostService } from 'src/app/Services/Post/post-service';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css']
})

export class CommentsComponent implements OnInit {

  text: string = '';
  commentsTmp: CommentModel[] = [];
  commentsArray: CommentModel[] = [];
  comment: CommentModel = {
    id: '',
    activityID: '',
    athleteID: '',
    text: ""
  }

  constructor(private cookieSvc: CookieService, private getSvc: GetService, private postSvc: PostService) { }

  ngOnInit(): void {
    this.getComments();
  }

  getComments() {
    this.getSvc.getComments().subscribe(
      res => {
        this.commentsTmp = res;
        this.organizeComments();
      },
      err=>{
        alert('Ha ocurrido un error')
      }
    );
  }

  organizeComments() {
    let id = this.cookieSvc.get("ActivityID");
    this.commentsTmp.forEach(element => {
      if (element.activityID == id) {
        this.commentsArray.push(element);
      }
    });
  }

  postComment() {
    this.comment.athleteID = this.cookieSvc.get("Username");
    this.comment.activityID = this.cookieSvc.get("ActivityID");
    let text = (<HTMLInputElement>document.getElementById("msg")).value;
    let textCopy = text;

    if (textCopy === "") {
      alert("Su comentario no puede estar vacío.")
    }
    else {
      this.comment.text = textCopy;
      this.postSvc.postComment(this.comment).subscribe(
        res => {
          alert("Comentario publicado exitosamente.")
          location.reload();
        },
        err=>{
          alert('Ha ocurrido un error')
        }
      );
    }
  }

}
