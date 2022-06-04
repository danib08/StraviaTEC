import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule} from "@angular/forms";
import { HttpClient, HttpClientModule} from "@angular/common/http";
import { ComponentsModule } from '../Components/components.module';
import { LoginComponent } from './login/login.component';
import { SignUpComponent } from './signup/sign-up/sign-up.component';
import { UpdateProfileComponent } from './update-profile/update-profile/update-profile.component';
import { CreateActivityComponent } from './create-activity/create-activity.component';
import { SearchAthletesComponent } from './search-athletes/search-athletes.component';
import { HomeComponent } from './Home/home/home.component';
import { SubscriptionsComponent } from './subscriptions/subscriptions.component';
import { ActivityComponent } from './activity/activity.component';
import { GroupSubscribeComponent } from './group-subscribe/group-subscribe.component';
import { ReportsComponent } from './reports/reports.component';

@NgModule({
  declarations: [
    LoginComponent,
    SignUpComponent,
    HomeComponent,
    UpdateProfileComponent,
    CreateActivityComponent,
    SearchAthletesComponent,
    SubscriptionsComponent,
    ActivityComponent,
    GroupSubscribeComponent,
    ReportsComponent,
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
