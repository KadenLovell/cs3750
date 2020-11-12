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
  userCourseIds: any;

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
    this._courseSearchService.getUserCourses().then(response => {
      this.userCourseIds = response;
      console.log(this.userCourseIds);
    });

    this._courseSearchService.getClasses().then(response => {
      this.rows = response;
      console.log(this.rows);
    });
  }

  search() {
    this._courseSearchService.searchClasses(this.model).then(response => {
      this.rows = response;
    });
  }

  register(id) {
    this.model.courseId = id;
    this.model.studentId = this.user.id;

    this._courseSearchService.registerUserCourse(this.model).then(response => {
      // success dialog
      // turn register button into an unregister
      // maybe requery rows
    });
  }
}
