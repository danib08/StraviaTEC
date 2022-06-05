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
import { Competition } from "src/app/Models/competition";
import { FormControl } from "@angular/forms";
import { Sponsor } from "src/app/Models/sponsor";
import { Challenge } from "src/app/Models/challenge";
import { Group } from "src/app/Models/group";


@Injectable({
    providedIn: 'root'
})
export class PostService {
    private baseURL = 'https://localhost:5001/api/';

    /**
    * Método constructor
    * @param http 
    */
    constructor(private http: HttpClient) {
    }

    signInAthlete(athlete: AthleteModel): Observable<any>{
        return this.http.post<any>(this.baseURL, athlete);
    }

    signUpAthlete(athlete: AthleteModel): Observable<any>{
        return this.http.post<any>(this.baseURL, athlete);
    }

    searchAthletes(athlete:AthleteSearch): Observable<any>{
        return this.http.post<any>(this.baseURL,athlete);
    }

    addFollower(athleteFriend: AthleteFriends): Observable<any>{
        return this.http.post<any>(this.baseURL, athleteFriend);
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
     * Posts a new Competition to register it on the database 
     * @param comp the new competition to be registered
     * @returns the API response
     */
     createCompetition(comp: Competition): Observable<any>{
        return this.http.post<any>(this.baseURL + "Competition", comp);
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

    createChallenge(challenge:Challenge):Observable<any>{
        return this.http.post<any>(this.baseURL,challenge);
    }

    /**
     * Posts a new CompetitionCategories to register it on the database 
     * @param category the new competition categories to be registered
     * @returns the API response
     */
    createCompetitionCategories(category:FormControl):Observable<any>{
        return this.http.post<any>(this.baseURL + "CompetitionCategories", category);
    }

     /**
     * Posts a new Sponsor to register it on the database 
     * @param sponsor the new sponsor to be registered
     * @returns the API response
     */
    createSponsor(sponsor:Sponsor):Observable<any>{
        return this.http.post<any>(this.baseURL + "Sponsor", sponsor);
    }

    createGroup(group:Group):Observable<any>{
        return this.http.post<any>(this.baseURL,group);
    }
}
