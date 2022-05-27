import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Pages/login/login.component';
import { ActivityComponent } from './Pages/activity/activity.component';
import { CreateCompetitionComponent } from './Pages/create-competition/create-competition.component';
import { CompetitionParticipantsComponent } from './Pages/competition-participants/competition-participants.component';
import { CompetitionPositionsComponent } from './Pages/competition-positions/competition-positions.component';
import { ModifyCompetitionComponent } from './Pages/modify-competition/modify-competition.component';
import { CreateChallengeComponent } from './Pages/create-challenge/create-challenge.component';
import { CreateGroupComponent } from './Pages/create-group/create-group.component';
import { AcceptRegistrationComponent } from './Pages/accept-registration/accept-registration.component';
import { ModifyChallengeComponent } from './Pages/modify-challenge/modify-challenge.component';

const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {path: 'activityInfo', component: ActivityComponent},
  {path: 'createCompetition', component: CreateCompetitionComponent},
  {path: 'participantsReport', component: CompetitionParticipantsComponent},
  {path: 'positionsReport', component: CompetitionPositionsComponent},
  {path: 'manageCompetition', component: ModifyCompetitionComponent},
  {path: 'createChallenges', component: CreateChallengeComponent},
  {path: 'createGroups', component: CreateGroupComponent},
  {path: 'acceptRegister', component: AcceptRegistrationComponent},
  {path: 'manageChallenge', component:ModifyChallengeComponent},
  {path: "**", redirectTo: "login", pathMatch:"full"}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
