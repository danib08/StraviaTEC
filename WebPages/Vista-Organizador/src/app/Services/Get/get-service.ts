import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class GetService {

    private baseURL = 'https://straviatec.free.beeceptor.com';

    constructor(private http: HttpClient) {
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
        let URL = this.baseURL + '/' + ActivityID;
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

    getAthletesReport(ActivityID:string):Observable<any>{
        let URL = this.baseURL + '/report/' + ActivityID;
        return this.http.get<any[]>(URL);
    }
}
