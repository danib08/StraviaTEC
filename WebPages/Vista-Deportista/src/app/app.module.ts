import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './Components/header/header/header.component';
import { FooterComponent } from './Components/footer/footer/footer.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { LogInComponent } from './Pages/LogIn/log-in/log-in.component';
import { HomeComponent } from './Pages/Home/home/home.component';


const appRoutes: Routes = [
  {path: 'LogIn', component: LogInComponent},
  {path: 'Home', component: HomeComponent}
]
@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    LogInComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    RouterModule.forRoot(appRoutes,{enableTracing:true})
  ],

  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
