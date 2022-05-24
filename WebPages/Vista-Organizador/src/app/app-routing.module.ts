import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Pages/login/login.component';
import { ActivityComponent } from './Pages/activity/activity.component';
import { CreateCompetitionComponent } from './Pages/create-competition/create-competition.component';
import { CompetitionParticipantsComponent } from './Pages/competition-participants/competition-participants.component';

const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {path: 'activityInfo', component: ActivityComponent},
  {path: 'createCompetition', component: CreateCompetitionComponent},
  {path: 'participantsReport', component: CompetitionParticipantsComponent},
  {path: "**", redirectTo: "login", pathMatch:"full"}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
