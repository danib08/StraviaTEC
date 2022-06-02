/**
 * Model for the Activity Table
 */
export interface ActivityModel {
    id: string,
    name: string,
    route: string, // Check back which type will the route be
    date: string,
    duration: string, 
    kilometers: number,
    type: string,
    athleteUsername: string
}
