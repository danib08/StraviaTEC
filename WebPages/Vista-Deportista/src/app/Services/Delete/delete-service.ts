import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})

/**
 * Service for the Delete Methods to the API
 */
export class DeleteService {

  private baseURL = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  /**
   * Deletes an athlete according to the username provided
   * @param AthleteID the username/id of the athlete to be deleted
   * @returns the API response
   */
  deleteAthlete(AthleteID:string):Observable<any>{
    return this.http.delete<any>(this.baseURL + "Athlete/" + AthleteID);
  }
}
