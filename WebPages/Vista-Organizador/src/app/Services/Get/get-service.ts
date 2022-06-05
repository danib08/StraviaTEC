import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Competition } from "src/app/Models/competition";

@Injectable({
    providedIn: 'root'
})
export class GetService {

    private baseURL = 'https://localhost:5001/api/';

    constructor(private http: HttpClient) {
    }


    /**
     * Gets activity info according to the provided ID
     * @param ActivityID the ID of the desired activity
     * @returns  the activity object
     */
    getActivity(ActivityID:string):Observable<any>{
        return this.http.get<any>(this.baseURL + 'Activity/' + ActivityID);
    }

    /**
     * Gets the info according to the provided ID
     * @param CompetitionID the ID of the desired competition
     * @returns  an array with the info for the report
     */
    getAthletesReport(CompetitionID:string):Observable<any>{
        return this.http.get<any[]>(this.baseURL + '/AthleteInCompetition/Report/' + CompetitionID);
    }

    /**
     * Gets Athlete created competition according to the provided ID
     * @param AthleteID the ID of the Athlete
     * @returns  An array object
     */
    getAthleteCreatedCompetitions(AthleteID:string):Observable<any>{
        return this.http.get<any[]>(this.baseURL + 'Athlete/CompetitionCreator/' + AthleteID);
    }

    /**
     * Gets Athlete created groups according to the provided ID
     * @param AthleteID the ID of the Athlete
     * @returns  An array object
     */
    getAthleteCreatedGroups(AthleteID:string):Observable<any>{
        return this.http.get<any[]>(this.baseURL + 'Groups/' + AthleteID );
    }

    /**
     * Gets Athlete created challenges according to the provided ID
     * @param AthleteID the ID of the Athlete
     * @returns  An array object
     */
    getAthleteCreatedChallenges(AthleteID:string):Observable<any>{
        return this.http.get<any[]>(this.baseURL + 'Athlete/ChallengeCreator/' + AthleteID);
    }

    /**
     * Gets Athletes with the status No Aceptado in a Competition
     * @param CompetitionID the ID of the Athlete
     * @returns  An array object
     */
    getAthleteRegister(CompetitionID:string){
        return this.http.get<any[]>(this.baseURL + 'AthleteInCompetition/NotAcceptedAthletes/' + CompetitionID);
    }
}
