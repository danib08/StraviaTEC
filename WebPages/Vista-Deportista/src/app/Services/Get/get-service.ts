import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})

/**
 * Service for the Get Methods to the API
 */
export class GetService {

    private baseURL = 'https://localhost:5001/api/';
    private mongoURL = 'https://localhost:5050/api/';

    constructor(private http: HttpClient) {}

    /**
     * Gets the Athlete specified by its username
     * @param AthleteID the ID used to retrieve the athlete info
     * @returns the desired Athlete object
     */
    getAthlete(AthleteID:string):Observable<any>{
        return this.http.get<any>(this.baseURL + "Athlete/" + AthleteID);
    }

    /**
     * Gets all the challenges where an athlete is participating
     * @param AthleteID the desired athlete's username
     * @returns array of challenges where the athlete is participating
    */
    getAthleteChallenges(AthleteID:string) {
        return this.http.get<any>(this.baseURL + "AthleteInChallenge/Athlete/" + AthleteID);
    }

    /**
     * Gets all the challenges where the athelete is participating
     * @param AthleteID the desired athlete's username
     * @returns array of challenges
     */
     getOnGoingChallenges(AthleteID:string):Observable<any>{
        return this.http.get<any[]>(this.baseURL + 'AthleteInChallenge/Accepted/' + AthleteID);
    }

    /**
     * Gets challenge info according to the provided ID
     * @param ChallengeID the ID of the desired challenge
     * @returns the challenge object
     */
    getChallenge(ChallengeID:string):Observable<any>{
        return this.http.get<any>(this.baseURL + 'Challenge/' + ChallengeID);
    }

    /**
     * Gets all the competitions where the athelete is participating
     * and its receipt is accepted
     * @param AthleteID the desired athlete's username
     * @returns array of competitions
     */
     getAcceptedCompetitions(AthleteID:string):Observable<any>{
        return this.http.get<any[]>(this.baseURL + 'AthleteInCompetition/AcceptedComps/' + AthleteID);
    }

    /**
     * Gets competition info according to the provided ID
     * @param CompetitionID the ID of the desired competition
     * @returns the competition object
     */
    getCompetition(CompetitionID:string):Observable<any>{
        return this.http.get<any>(this.baseURL + 'Competition/' + CompetitionID);
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
     * Gets all competitions that an athlete has not joined yet
     * @param AthleteID the ID of the desired athlete
     * @returns array of competitions
     */
    getNotJoinedCompetitions(AthleteID: string):Observable<any>{
        return this.http.get<any[]>(this.baseURL + 'AthleteInCompetition/NotInscribed/' + AthleteID);
    }

    /**
     * Gets all challenges that an athlete has not joined yet
     * @param AthleteID the ID of the desired athlete
     * @returns array of challenges
     */
    getNotJoinedChallenges(AthleteID: string):Observable<any>{
        return this.http.get<any[]>(this.baseURL + 'AthleteInChallenge/NotInscribed/' + AthleteID);
    }

    /**
     * Gets all groups that an athlete has not joined yet
     * @param AthleteID the ID of the desired athlete
     * @returns array of groups
     */
    getNotJoinedGroups(AthleteID: string):Observable<any>{
        return this.http.get<any[]>(this.baseURL + 'GroupMember/NotInscribed/' + AthleteID);
    }

    /**
     * Gets information on the rankings of a specific competition
     * @param CompetitionID the ID of the competition to search for the ranking
     * @returns the ranking of the desired competition
     */
    getPositionsReport(CompetitionID: string):Observable<any>{
        return this.http.get<any[]>(this.baseURL + 'AthleteInCompetition/Report/' + CompetitionID);
    }

    /**
     * Gets information on the progress of a challenge
     * @param ChallengeID the ID of the challenge to search for
     * @returns the progress of the challenge
     */
    getChallengeReport(ChallengeID: string):Observable<any>{
        return this.http.get<any[]>(this.baseURL + '' + ChallengeID);
    }

    /**
     * Gets the activity feed of an athlete according to who it follows
     * @param AthleteID the ID of the athlete
     * @returns array of activities
     */
    getFeed(AthleteID: string):Observable<any>{
        return this.http.get<any[]>(this.baseURL + 'Athlete/Feed/' + AthleteID);
    }

    /**
     * Gets an specific athleteInChallenge
     * @param AthleteID the ID of the athlete
     * @param ChallengeID the ID of the challenge
     * @returns an athleteInCompetition object
     */
    getAthleteInChallenge(AthleteID: string, ChallengeID: string):Observable<any>{
        return this.http.get<any>(this.baseURL + 'AthleteInChallenge' + AthleteID + '/' + ChallengeID);
    }

    getComments():Observable<any>{
        return this.http.get<any[]>(this.mongoURL + 'Comment');
    }
}
