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


@Injectable({
    providedIn: 'root'
})
export class PostService {
    private baseURL = 'https://pruebaa.free.beeceptor.com';
    private searchURL = 'https://straviatec.free.beeceptor.com/search';
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

    createCompetition(competition:Competition):Observable<any>{
        return this.http.post<any>(this.baseURL,competition);
    }

    createChallenge(challenge:Challenge):Observable<any>{
        return this.http.post<any>(this.baseURL,challenge);
    }

    createCompetitionCategories(category:FormControl):Observable<any>{
        return this.http.post<any>(this.baseURL, category);
    }

    createSponsor(sponsor:Sponsor):Observable<any>{
        return this.http.post<any>(this.baseURL,sponsor);
    }
}
