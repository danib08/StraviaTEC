import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ActivityModel } from "src/app/Models/activity-model";
import { AthleteInCompetition } from "src/app/Models/athlete-in-competition";
import { AthleteModel } from "src/app/Models/athlete-model";
import { Challenge } from "src/app/Models/challenge";
import { Competition } from "src/app/Models/competition";
import { Group } from "src/app/Models/group";

@Injectable({
    providedIn: 'root'
})
export class PutService {

    private baseURL = 'https://localhost:5001/api/';

    constructor(private http: HttpClient) {
    }
    
    updateAthlete(athlete: AthleteModel):Observable<any>{
       return this.http.put<any>(this.baseURL, athlete);
    }

    modifyActivity(activity:ActivityModel):Observable<any>{
        return this.http.put<any>(this.baseURL + 'Activity' , activity);
    }

    modifyCompetition(competition:Competition):Observable<any>{
        return this.http.put<any>(this.baseURL + 'Competition' , competition);
    }

    modifyGroup(group:Group):Observable<any>{
        return this.http.put<any>(this.baseURL + 'Groups', group);
    }


    modifyChallenge(challenge:Challenge):Observable<any>{
        return this.http.put<any>(this.baseURL + 'Challenge', challenge);
    }

    modifyAthleteStatusInCompetition(athlete:AthleteInCompetition):Observable<any>{
        return this.http.put<any>(this.baseURL + 'AthleteInCompetition', athlete);
    }
}
