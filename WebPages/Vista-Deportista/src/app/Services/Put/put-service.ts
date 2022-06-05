import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AthleteInChallenge } from "src/app/Models/athlete-in-challenge";
import { AthleteInCompetition } from "src/app/Models/athlete-in-competition";
import { AthleteModel } from "src/app/Models/athlete-model";

@Injectable({
    providedIn: 'root'
})

/**
 * Service for the Put Methods to the API
 */
export class PutService {
    
    private baseURL = 'https://localhost:5001/api/';

    constructor(private http: HttpClient) {}
    
    /**
     * Puts the provided Athlete to change its info
     * @param athlete the Athlete Model with the new info of the athlete 
     * to be modified
     * @returns the API response
     */
    updateAthlete(athlete: AthleteModel):Observable<any>{
        return this.http.put<any>(this.baseURL + "Athlete", athlete);
   }

   /**
     * Puts the provided AthleteInCompetition to change its info
     * @param aIC the AthleteInCompetition Model with the new info to be modified
     * @returns the API response
     */
    updateAthleteInCompetition(aIC: AthleteInCompetition):Observable<any>{
        return this.http.put<any>(this.baseURL + "AthleteInCompetition", aIC);
    }

    /**
     * Puts the provided AthleteInChallenge to change its info
     * @param aIC the AthleteInChallenge Model with the new info to be modified
     * @returns the API response
     */
     updateAthleteInChallenge(aIC: AthleteInChallenge):Observable<any>{
        return this.http.put<any>(this.baseURL + "AthleteInChallenge", aIC);
    }
}
