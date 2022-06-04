import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { AthleteModel } from "src/app/Models/athlete-model";
import { Observable } from "rxjs";
import { AthleteSearch } from "src/app/Models/athlete-search";
import { AthleteFollower } from "src/app/Models/athlete-follower";
import { ActivityModel } from "src/app/Models/activity-model";
import { ActivityInChallenge } from "src/app/Models/activity-in-challenge";
import { AthleteInChallenge } from "src/app/Models/athlete-in-challenge";
import { AthleteInCompetition } from "src/app/Models/athlete-in-competition";
import { GroupMember } from "src/app/Models/group-member";


@Injectable({
    providedIn: 'root'
})

/**
 * Service for the Post Methods to the API
 */
export class PostService {
    private baseURL = 'https://localhost:5001/api/';

    constructor(private http: HttpClient) {}

    /**
     * Posts the provided Athlete to verify its login
     * @param athlete the AthleteModel with the username and 
     * password intended for login
     * @returns the API response
     */
    signInAthlete(athlete: AthleteModel): Observable<any>{
        return this.http.post<any>(this.baseURL + "Athlete/LogIn", athlete);
    }

    /**
     * Posts a new Athlete to register it on the database 
     * @param athlete the new athlete to be registered
     * @returns the API response
     */
    signUpAthlete(athlete: AthleteModel): Observable<any>{
        return this.http.post<any>(this.baseURL + "Athlete", athlete);
    }

    /**
     * Requests the API an array of athletes matching the given name and last name
     * @param athleteSerach object containing the name and last name to look for
     * @returns an array of athletes matching the conditions
     */
    searchAthletes(athleteSerach: AthleteSearch): Observable<any>{
        return this.http.post<any>(this.baseURL + "Athlete/Search", athleteSerach);
    }

    /**
     * Posts a new AthleteFollower object to the API indicating that the user
     * desires to follow a specific athelete
     * @param athleteFollower the object containing the necessary information to
     * get the athlete following done
     * @returns the API response
     */
    addFollower(athleteFollower: AthleteFollower): Observable<any>{
        return this.http.post<any>(this.baseURL + "AthleteFollower", athleteFollower);
    }

    /**
     * Posts a new Activity to register it on the database 
     * @param activity the new activity to be registered
     * @returns the API response
     */
    createActivity(activity: ActivityModel): Observable<any>{
        return this.http.post<any>(this.baseURL + "Activity", activity);
    }

    /**
     * Posts a new ActivityInChallenge object to the API indicating that the user
     * registered an activity related to a challenge
     * @param activityInChallenge the ActivityInChallenge object
     * @returns the API response
     */
    createActivityInChallenge(activityInChallenge: ActivityInChallenge): Observable<any>{
        return this.http.post<any>(this.baseURL + "Activity_In_Challenge", activityInChallenge);
    }

    /**
     * Posts a new AthleteInChallenge object to the API indicating that the user
     * joined a challenge
     * @param aIC the AthleteInChallenge object
     * @returns the API response
     */
    createAthleteInChallenge(aIC: AthleteInChallenge): Observable<any>{
        return this.http.post<any>(this.baseURL + "AthleteInChallenge", aIC);
    }

    /**
     * Posts a new AthleteInCompetition object to the API indicating that the user
     * joined a competition
     * @param aIC the AthleteInCompetition object
     * @returns the API response
     */
    createAthleteInCompetition(aIC: AthleteInCompetition): Observable<any>{
        return this.http.post<any>(this.baseURL + "AthleteInCompetition", aIC);
    }
    
    /**
     * Posts a new GroupMember object to the API indicating that the user
     * joined a group
     * @param groupMember the GroupMember object
     * @returns the API response
     */
    createGroupMember(groupMember: GroupMember): Observable<any>{
        return this.http.post<any>(this.baseURL + "GroupMember", groupMember);
    }
}
