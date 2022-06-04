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
        return this.http.get<any[]>(this.baseURL + 'AthleteInChallenge/Athlete/' + AthleteID);
    }

    /**
     * Gets challenge info according to the provided ID
     * @param ChallengeID the ID of the desired challenge
     * @returns the challenge object
     */
    getChallenge(ChallengeID:string):Observable<any>{
        return this.http.get<any>(this.baseURL + 'Challenge/' + ChallengeID);
    }

    /**
     * Gets all the competitions where the athelete is subscribed
     * @param AthleteID the desired athlete's username
     * @returns array of competitions
     */
    getAthleteinCompetition(AthleteID:string):Observable<any>{
        return this.http.get<any[]>(this.baseURL + 'AthleteInCompetition/Athlete/' + AthleteID);
    }

    /**
     * Gets competition info according to the provided ID
     * @param CompetitionID the ID of the desired competition
     * @returns the competition object
     */
    getCompetition(CompetitionID:string):Observable<any>{
        return this.http.get<any>(this.baseURL + 'Competition/' + CompetitionID);
    }

    /**
     * Gets activity info according to the provided ID
     * @param ActivityID the ID of the desired activity
     * @returns  the activity object
     */
    getActivity(ActivityID:string):Observable<any>{
        return this.http.get<any>(this.baseURL + 'Activity/' + ActivityID);
    }

    //*****
    getCompetitions():Observable<any>{
        return this.http.get<any[]>(this.baseURL + 'Competition');
    }

    //*****
    getChallenges():Observable<any>{
        return this.http.get<any[]>(this.baseURL + 'Challenge');
    }

    //*****
    getGroups():Observable<any>{
        return this.http.get<any[]>(this.baseURL + 'Groups');
    }

    /**
     * Gets information on the rankings of a specific competition
     * @param CompetitionID the ID of the competition to search for the ranking
     * @returns the ranking of the desired competition
     */
    getPositionsReport(CompetitionID: string):Observable<any>{
        return this.http.get<any[]>(this.baseURL + 'AthleteInCompetition/Report/' + CompetitionID);
    }

    /**
     * Gets information on the progress of a challenge
     * @param ChallengeID the ID of the challenge to search for
     * @returns the progress of the challenge
     */
    getChallengeReport(ChallengeID: string):Observable<any>{
        return this.http.get<any[]>(this.baseURL + '' + ChallengeID);
    }
}