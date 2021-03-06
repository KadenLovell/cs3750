import { Component, OnInit } from '@angular/core';
import { CourseSearchService } from './coursesearch.service';
import { BaseComponent } from '../../base/base.component';

// shared
import { User } from "../../shared/user/user";
import { UserService } from "../../shared/user/user.service";

@Component({
  selector: 'app-coursesearch',
  templateUrl: './coursesearch.component.html',
  styleUrls: ['./coursesearch.component.scss'],
  providers: [CourseSearchService]
})
export class CourseSearchComponent extends BaseComponent implements OnInit {
  model: any;
  response: any;
  errors: any;
  view: any;
  rows: any;
  userCourseIds: any[];

  get user(): User {
    return this._userService.user;
  }

  displayedColumns: string[] = ['id', 'name', 'department', 'instructor', 'code'];

  constructor(private readonly _courseSearchService: CourseSearchService, private readonly _userService: UserService) {
    super();
  }

  ngOnInit(): void {
    this._userService.loadUser();
    this.view = 1;
    this.model = {};
    this.userCourseIds = [];

    this._courseSearchService.getUserCourses().then(response => {
      for (var i = 0; i < response.length; i++) {
        this.userCourseIds.push(response[i].courseId);
      }
    });

    this._courseSearchService.getClasses().then(response => {
      this.rows = response;
    });
  }

  search() {
    this._courseSearchService.searchClasses(this.model).then(response => {
      this.rows = response;
    });
  }

  unregister(id) {
    this.model.userCourseId = id;
    this._courseSearchService.deleteUserCourse(this.model).then(response => {
      if(response.success) {
        this.userCourseIds.splice(this.userCourseIds.indexOf(id), 1);
        this.deductFees(response.userCourse.creditHours);

      }
    });
  }

  register(id) {
    this.model.courseId = id;
    this.model.studentId = this.user.id;
    this.model.id = this.user.id;
    this._courseSearchService.registerUserCourse(this.model).then(response => { 
      if(response.success){
        this.addFees(response.userCourse.creditHours);
      }
      this.userCourseIds.push(id);
    });
  }

  addFees(creditHours){
    this.user.fees += creditHours * 800;
    this._courseSearchService.updateFees(this.user).then(response => {});
  }

  deductFees(creditHours){
    this.user.fees -= creditHours * 800;
    this._courseSearchService.updateFees(this.user).then(response => {});
  }

      //when a user registers for a class I want to be able to sum all creditHours that the user has for 
      //userCourse and add it to the User Model in the database for the fees column

  }
