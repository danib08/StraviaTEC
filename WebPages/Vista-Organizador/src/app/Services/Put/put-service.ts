import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ActivityModel } from "src/app/Models/activity-model";
import { AthleteModel } from "src/app/Models/athlete-model";
import { Competition } from "src/app/Models/competition";

@Injectable({
    providedIn: 'root'
})
export class PutService {

    private baseURL = 'https://pruebaa.free.beeceptor.com';
    constructor(private http: HttpClient) {
    }
    
    updateAthlete(athlete: AthleteModel):Observable<any>{
       return this.http.put<any>(this.baseURL, athlete);
    }

    modifyActivity(activity:ActivityModel):Observable<any>{
        return this.http.put<any>(this.baseURL, activity);
    }
    modifyCompetition(competition:Competition):Observable<any>{
        return this.http.put<any>(this.baseURL, competition);
    }

}
