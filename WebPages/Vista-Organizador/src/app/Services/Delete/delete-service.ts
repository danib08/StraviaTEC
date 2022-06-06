import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class DeleteService {

    private baseURL = 'https://straviaapideploy.azurewebsites.net/api/';

    constructor(private http: HttpClient) {
    }

    deleteCompetition(CompetitionID:string):Observable<any>{
        return this.http.delete<any>(this.baseURL + 'Competition/Competition/' + CompetitionID);
    }

    deleteCategory(CompetitionID:string):Observable<any>{
        let URL = this.baseURL + '/' + CompetitionID;
        return this.http.delete<any>(URL);
    }

    deleteActivity(ActivityID:string):Observable<any>{
        let URL = this.baseURL + '/' + ActivityID;
        return this.http.delete<any>(URL);
    }

    deleteGroup(groupName:string):Observable<any>{
        return this.http.delete<any>(this.baseURL + 'Groups/' + groupName);
    }

    deleteChallenge(challenge:string):Observable<any>{
        return this.http.delete<any>(this.baseURL + 'Challenge/' + challenge);
    }
}
