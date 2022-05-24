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

@NgModule({
  declarations: [
    LoginComponent,
    HomeComponent,
    ActivityComponent,
    CreateCompetitionComponent,
    CompetitionParticipantsComponent,
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
