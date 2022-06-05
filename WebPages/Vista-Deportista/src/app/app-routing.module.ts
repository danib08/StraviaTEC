import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Pages/login/login.component';
import { SignUpComponent } from './Pages/signup/sign-up/sign-up.component';
import { UpdateProfileComponent } from './Pages/update-profile/update-profile/update-profile.component';
import { SearchAthletesComponent } from './Pages/search-athletes/search-athletes.component';
import { CreateActivityComponent } from './Pages/create-activity/create-activity.component';
import { SubscriptionsComponent } from './Pages/subscriptions/subscriptions.component';
import { GroupSubscribeComponent } from './Pages/group-subscribe/group-subscribe.component';
import { ReportsComponent } from './Pages/reports/reports.component';
import { NewsfeedComponent } from './Pages/newsfeed/newsfeed.component';

// Routes for the different Pages on the Web Application
const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {path: 'signup', component: SignUpComponent},
  {path: 'updateProfile', component: UpdateProfileComponent},
  {path: 'search', component: SearchAthletesComponent},
  {path: 'createActivity', component: CreateActivityComponent},
  {path: 'subscriptions', component: SubscriptionsComponent},
  {path: 'groups', component: GroupSubscribeComponent},
  {path: 'reports', component: ReportsComponent},
  {path: 'feed', component: NewsfeedComponent},
  {path: "**", redirectTo: "login", pathMatch:"full"}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
