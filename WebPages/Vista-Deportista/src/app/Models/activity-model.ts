/**
 * Model for the Activity Table
 */
export interface ActivityModel {
    ID: string,
    Name: string,
    Route: string, // Check back which type will the route be
    Date: string,
    Duration: string, 
    Kilometers: number,
    Type: string,
    AthleteUsername: string
}
