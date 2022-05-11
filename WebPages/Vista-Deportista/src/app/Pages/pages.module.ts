import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule} from "@angular/forms";
import { HttpClient, HttpClientModule} from "@angular/common/http";
import { ComponentsModule } from '../Components/components.module';
import { LoginComponent } from './login/login.component';
import { SignUpComponent } from './signup/sign-up/sign-up.component';
import { HomeComponent } from './home/home/home.component';
import { UpdateProfileComponent } from './update-profile/update-profile/update-profile.component';

@NgModule({
  declarations: [
    LoginComponent,
    SignUpComponent,
    HomeComponent,
    UpdateProfileComponent,
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
