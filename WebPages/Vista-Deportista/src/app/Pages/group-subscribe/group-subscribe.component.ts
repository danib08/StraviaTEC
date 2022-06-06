import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Group } from 'src/app/Models/group';
import { GroupMember } from 'src/app/Models/group-member';
import { GetService } from 'src/app/Services/Get/get-service';
import { PostService } from 'src/app/Services/Post/post-service';


@Component({
  selector: 'app-group-subscribe',
  templateUrl: './group-subscribe.component.html',
  styleUrls: ['./group-subscribe.component.css']
})

/**
 * Group Subscribe component where the user can join groups
 */
export class GroupSubscribeComponent implements OnInit {

  State = false;
  GroupsArray: Group[] = [];

  groupMember: GroupMember = {
    groupname: '',
    memberid: ''
  }

  /**
   * Creates the Group Subscribe component
   * @param postSvc service for POST requests to the API
   * @param getSvc service for GET requests to the API
   * @param cookieSvc service for cookie creating to store the username
   */
  constructor(private postSvc: PostService, private getSvc: GetService, private cookieSvc: CookieService) { }

  ngOnInit(): void {
    this.getGroups();
  }

  /**
   * Gets all groups that the athlete is a member of
   */
  getGroups() {
    this.getSvc.getNotJoinedGroups(this.cookieSvc.get("Username")).subscribe(
      res => {
        this.GroupsArray = res;
      },
      err => {
        alert('Hubo un error al intentar conseguir los atletas.');
      }
    );
    this.State = true;
  }

  /**
   * Adds the athlete to the group
   * @param groupID ID of the group where it will be added
   * @param $event triggered by the mouse click
   */
  joinGroup(groupID: string,  $event: MouseEvent) {
    ($event.target as HTMLButtonElement).disabled = true;

    this.groupMember.memberid = this.cookieSvc.get('Username');
    this.groupMember.groupname = groupID;
    this.postSvc.createGroupMember(this.groupMember).subscribe(
      res => {
      },
      err => {
        alert('No se pudo unir al grupo')
      }
    );
    location.reload();
  }

}
