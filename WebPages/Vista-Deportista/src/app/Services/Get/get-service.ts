import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})

/**
 * Service for the Get Methods to the API
 */
export class GetService {

    private baseURL = 'https://localhost:5001/api/';

    constructor(private http: HttpClient) {}

    /**
     * Gets the Athlete specified by its username
     * @param AthleteID the ID used to retrieve the athlete info
     * @returns the desired Athlete object
     */
    getAthlete(AthleteID:string):Observable<any>{
        return this.http.get<any>(this.baseURL + "Athlete/" + AthleteID);
    }

    getAthleteinChallenge(AthleteID:string):Observable<any>{
        let URL = this.baseURL + '/' + AthleteID;
        return this.http.get<any[]>(URL);
    }

    getChallenge(ChallengeID:string):Observable<any>{
        let URL = this.baseURL + '/' + ChallengeID;
        return this.http.get<any>(URL);
    }

    getAthleteinCompetition(AthleteID:string):Observable<any>{
        let URL = this.baseURL + '/' + AthleteID;
        return this.http.get<any[]>(URL);
    }

    getCompetition(CompetitionID:string):Observable<any>{
        let URL = this.baseURL + '/' + CompetitionID;
        return this.http.get<any>(URL);
    }

    getActivity(ActivityID:string):Observable<any>{
        let URL = this.baseURL + 'Activity/' + ActivityID;
        return this.http.get<any>(URL);
    }

    getCompetitions():Observable<any>{
        let URL = this.baseURL + '/competitions';
        return this.http.get<any[]>(URL);
    }

    getChallenges():Observable<any>{
        let URL = this.baseURL + '/challenges';
        return this.http.get<any[]>(URL);
    }
}
