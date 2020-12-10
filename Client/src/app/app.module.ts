// modules (keep alpahabetical)
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FlatpickrModule } from 'angularx-flatpickr';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from "@angular/common/http";
import { MaterialModule } from './material.module';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

// components (keep alphabetical)
import { AppComponent } from './app.component';
import { CalendarComponent } from './modules/calendar/calendar.component';
import { ClasslistComponent } from './modules/classlist/classlist.component';
import { CourseSearchComponent } from './modules/coursesearch/coursesearch.component';
import { HomeComponent } from './modules/home/home.component';
import { LoginComponent } from './modules/login/login.component';
import { ProfilePageComponent } from './modules/profilepage/profilepage.component';
import { TuitionAndFeesComponent } from './modules/tuitionandfees/tuitionandfees.component';

// shared components (keep alphabetical)
import { HttpService } from './shared/http/http.service';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { BaseComponent } from './base/base.component';
import { CourseComponent } from './modules/course/course.component';
import { GradesComponent } from './modules/grades/grades.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    CourseSearchComponent,
    ClasslistComponent,
    CalendarComponent,
    ProfilePageComponent,
    TuitionAndFeesComponent,
    BaseComponent,
    CourseComponent,
    GradesComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule, FlatpickrModule.forRoot(),
    FormsModule,
    HttpClientModule,
    MaterialModule,
    RouterModule.forRoot([
      { path: 'calendar', component: CalendarComponent },
      { path: 'grades', component: GradesComponent },
      { path: 'profilepage', component: ProfilePageComponent },
      { path: 'course', component: CourseComponent },
      { path: 'coursesearch', component: CourseSearchComponent },
      { path: 'classlist', component: ClasslistComponent },
      { path: 'tuitionandfees', component: TuitionAndFeesComponent },
      { path: 'login', component: LoginComponent },
      { path: 'login', component: LoginComponent },
      { path: 'home', component: HomeComponent },
      { path: "", redirectTo: "login", pathMatch: "full" },
      { path: "**", redirectTo: "login" }
    ]),
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    CalendarModule.forRoot({ provide: DateAdapter, useFactory: adapterFactory }),
  ],
  providers: [HttpService],
  bootstrap: [AppComponent]
})
export class AppModule { }
