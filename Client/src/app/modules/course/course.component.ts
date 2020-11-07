import { Component, OnInit } from '@angular/core';

import { BaseComponent } from '../../base/base.component';
import { CourseService } from "../course/course.service";

// shared
import { User } from "../../shared/user/user";
import { UserService } from "../../shared/user/user.service";

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.scss'],
  providers: [CourseService]
})
export class CourseComponent extends BaseComponent implements OnInit {
  public state = '';
  model: any;
  rows: any;
  errors: any;
  view: any;

  get user(): User {
    return this._userService.user;
  }

  constructor(private readonly _userService: UserService, private readonly _courseService: CourseService) {
    super();
  }

  ngOnInit(): void {
    // get user if there is a refresh
    this._userService.loadUser();
    // initialize view
    this.view = 1;
    this.model = {};
    // get variables passed form home.component (temp fix, will break on refresh!!!!)
    // best path will be to use URL parameters, but it's not secure without having roles in the server!
    this.state = window.history.state.courseId;
    // get current course
    this._courseService.getCourse(this.state).then(response => {
      this.model.courseId = response.id;
    });
    this.getAssignments();
  }

  addAssignment() {
    this._courseService.addAssignment(this.model).then(response => {
      this.getAssignments();
    })
  }

  getAssignments() {
    this._courseService.getAssignments(this.state).then(response => {
      this.rows = response;
    });
  }
}
