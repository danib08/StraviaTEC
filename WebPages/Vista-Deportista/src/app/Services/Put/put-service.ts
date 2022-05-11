import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AthleteModel } from "src/app/Models/athlete-model";

@Injectable({
    providedIn: 'root'
})
export class PutService {

    private baseURL = 'https://straviatec.free.beeceptor.com';
    constructor(private http: HttpClient) {
    }
    
    updateAthlete(athlete: AthleteModel):Observable<any>{
       return this.http.put<any>(this.baseURL, athlete);
   }
}
