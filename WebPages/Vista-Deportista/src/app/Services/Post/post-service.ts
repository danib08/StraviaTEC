import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { AthleteModel } from "src/app/Models/athlete-model";
import { Observable } from "rxjs";
import { AthleteSearch } from "src/app/Models/athlete-search";
import { AthleteFriends } from "src/app/Models/athlete-friends";
import { ActivityModel } from "src/app/Models/activity-model";
import { ActivityInChallenge } from "src/app/Models/activity-in-challenge";
import { AthleteInChallenge } from "src/app/Models/athlete-in-challenge";
import { AthleteInCompetition } from "src/app/Models/athlete-in-competition";


@Injectable({
    providedIn: 'root'
})

/**
 * Service for the Post Methods to the API
 */
export class PostService {
    private baseURL = 'https://localhost:5001/api/';
    private searchURL = 'https://pruebaa.free.beeceptor.com/search';

    constructor(private http: HttpClient) {
    }

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

    searchAthletes(athlete:AthleteSearch): Observable<any>{
        return this.http.post<any>(this.searchURL,athlete);
    }

    addFollower(athleteFriend: AthleteFriends): Observable<any>{
        return this.http.post<any>(this.baseURL, athleteFriend);
    }

    createActivity(activity: ActivityModel): Observable<any>{
        return this.http.post<any>(this.baseURL, activity);
    }

    createActivityInChallenge(activityInChallenge: ActivityInChallenge): Observable<any>{
        return this.http.post<any>(this.baseURL, activityInChallenge);
    }

    createAthleteInChallenge(aIC: AthleteInChallenge): Observable<any>{
        return this.http.post<any>(this.baseURL, aIC);
    }

    createAthleteInCompetition(aIC: AthleteInCompetition): Observable<any>{
        return this.http.post<any>(this.baseURL, aIC);
    }
}
