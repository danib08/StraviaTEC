import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PagesModule } from './Pages/pages.module';
import { ComponentsModule } from './Components/components.module';
import { RouterModule } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { MapService } from './Services/Map/map.service';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
  ],

  //Modules
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule,
    ComponentsModule,
    PagesModule,
    ReactiveFormsModule
  ],

  //Services
  providers: [CookieService, MapService],
  bootstrap: [AppComponent]
})

export class AppModule { }
