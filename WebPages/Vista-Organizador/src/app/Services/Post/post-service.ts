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
    private baseURL = 'https://straviaapideploy.azurewebsites.net/api/';

    /**
    * Método constructor
    * @param http 
    */
    constructor(private http: HttpClient) {
    }

     /**
     * Posts the provided Athlete to verify its login
     * @param athlete the AthleteModel with the username and 
     * password intended for login
     * @returns the API response
     */
    signInAthlete(athlete: AthleteModel): Observable<any>{
        return this.http.post<any>(this.baseURL + 'Athlete/LogIn', athlete);
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

    /**
     * Posts a new Challenge to register it on the database 
     * @param challenge the new challenge to be registered
     * @returns the API response
     */
    createChallenge(challenge:Challenge):Observable<any>{
        return this.http.post<any>(this.baseURL + 'Challenge',challenge);
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

    /**
     * Posts a new Group to register it on the database 
     * @param group the new group to be registered
     * @returns the API response
     */
    createGroup(group:Group):Observable<any>{
        return this.http.post<any>(this.baseURL + 'Groups',group);
    }
}
