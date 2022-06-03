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

    /**
     * Gets all the challenges where the athelete is subscribed
     * @param AthleteID the desired athlete's username
     * @returns array of challenges
     */
    getAthleteinChallenge(AthleteID:string):Observable<any>{
        let URL = this.baseURL + 'AthleteInChallenge/Athlete/' + AthleteID;
        return this.http.get<any[]>(URL);
    }

    /**
     * Gets challenge info according to the provided ID
     * @param ChallengeID the ID of the desired challenge
     * @returns the challenge object
     */
    getChallenge(ChallengeID:string):Observable<any>{
        let URL = this.baseURL + 'Challenge/' + ChallengeID;
        return this.http.get<any>(URL);
    }

    /**
     * Gets all the competitions where the athelete is subscribed
     * @param AthleteID the desired athlete's username
     * @returns array of competitions
     */
    getAthleteinCompetition(AthleteID:string):Observable<any>{
        let URL = this.baseURL + 'AthleteInCompetition/Athlete/' + AthleteID;
        return this.http.get<any[]>(URL);
    }

    /**
     * Gets competition info according to the provided ID
     * @param CompetitionID the ID of the desired competition
     * @returns the competition object
     */
    getCompetition(CompetitionID:string):Observable<any>{
        let URL = this.baseURL + 'Competition/' + CompetitionID;
        return this.http.get<any>(URL);
    }

    /**
     * Gets activity info according to the provided ID
     * @param ActivityID the ID of the desired activity
     * @returns  the activity object
     */
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
