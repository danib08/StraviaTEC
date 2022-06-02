import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule} from "@angular/forms";
import { HttpClient, HttpClientModule} from "@angular/common/http";
import { ComponentsModule } from '../Components/components.module';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './Home/home/home.component';
import { ActivityComponent } from './activity/activity.component';
import { CreateCompetitionComponent } from './create-competition/create-competition.component';
import { CompetitionParticipantsComponent } from './competition-participants/competition-participants.component';
import { CompetitionPositionsComponent } from './competition-positions/competition-positions.component';
import { ModifyCompetitionComponent } from './modify-competition/modify-competition.component';
import { CreateChallengeComponent } from './create-challenge/create-challenge.component';
import { CreateGroupComponent } from './create-group/create-group.component';
import { AcceptRegistrationComponent } from './accept-registration/accept-registration.component';
import { ModifyChallengeComponent } from './modify-challenge/modify-challenge.component';

@NgModule({
  declarations: [
    LoginComponent,
    HomeComponent,
    ActivityComponent,
    CreateCompetitionComponent,
    CompetitionParticipantsComponent,
    CompetitionPositionsComponent,
    ModifyCompetitionComponent,
    CreateChallengeComponent,
    CreateGroupComponent,
    AcceptRegistrationComponent,
    ModifyChallengeComponent,
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    RouterModule,
    ComponentsModule
  ]
})

export class PagesModule {

}
