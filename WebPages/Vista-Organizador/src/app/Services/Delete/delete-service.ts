import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class DeleteService {

    private baseURL = 'https://pruebaa.free.beeceptor.com';

    constructor(private http: HttpClient) {
    }

    deleteCompetition(CompetitionID:string):Observable<any>{
        let URL = this.baseURL + '/' + CompetitionID;
        return this.http.delete<any>(URL);
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
        let URL = this.baseURL + '/' + groupName;
        return this.http.delete<any>(URL);
    }

    deleteChallenge(challenge:string):Observable<any>{
        let URL = this.baseURL + '/' + challenge;
        return this.http.delete<any>(URL);
    }
}
