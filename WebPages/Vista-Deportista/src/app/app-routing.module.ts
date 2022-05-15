import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Pages/login/login.component';
import { SignUpComponent } from './Pages/signup/sign-up/sign-up.component';
import { UpdateProfileComponent } from './Pages/update-profile/update-profile/update-profile.component';
import { SearchAthletesComponent } from './Pages/search-athletes/search-athletes.component';
import { CreateActivityComponent } from './Pages/create-activity/create-activity.component';

const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {path: 'signup', component: SignUpComponent},
  {path: 'updateProfile', component: UpdateProfileComponent},
  {path: 'search', component: SearchAthletesComponent},
  {path: 'createActivity', component: CreateActivityComponent},
  {path: "**", redirectTo: "login", pathMatch:"full"},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
