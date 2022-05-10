import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { AthleteModel } from "src/app/Models/athlete-model";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class PostService {
    private baseURL = 'https://straviatec.free.beeceptor.com';

    /**
    * Método constructor
    * @param http 
    */
    constructor(private http: HttpClient) {

    }

    signInAthlete(athlete: AthleteModel): Observable<any>{
        return this.http.post<any>(this.baseURL, athlete);
    }

    signUpAthlete(athlete: AthleteModel): Observable<any>{
        return this.http.post<any>(this.baseURL, athlete);
    }
}
