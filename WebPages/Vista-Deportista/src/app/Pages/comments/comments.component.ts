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

  commentsArray: CommentModel[] = [];

  constructor(private cookieSvc: CookieService, private getSvc: GetService, private postSvc: PostService) { }

  ngOnInit(): void {

  }

  getComments() {
    this.getSvc.getComments().subscribe(
      res => {
        this.commentsArray = res;
      },
      err=>{
        alert('Ha ocurrido un error')
      }
    );
  }

}
