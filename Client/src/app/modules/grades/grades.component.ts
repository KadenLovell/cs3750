import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { BaseComponent } from '../../base/base.component';
import { CourseService } from "../course/course.service";

// shared
import { User } from "../../shared/user/user";
import { UserService } from "../../shared/user/user.service";

@Component({
  selector: 'app-grades',
  templateUrl: './grades.component.html',
  styleUrls: ['./grades.component.scss'],
  providers: [CourseService]

})
export class GradesComponent extends BaseComponent implements OnInit {
  public state = '';
  model: any;
  rows: any;
  errors: any;
  view: any;

  @ViewChild('fileContent')
  fileContent: ElementRef;

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
    this.model.courseId = window.history.state.courseId;
    this._courseService.addAssignment(this.model).then(response => {
      this.getAssignments();
    })
  }

  getAssignments() {
    this._courseService.getAssignments(this.state).then(response => {
      this.rows = response;
    });
  }

  submitAssignment(event, object) {
    this.model.assignmentId = object.id;
    this.model.userId = this.user.id;
    const file = event.target.files[0];
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      this.model.contentType = reader.result.toString().split(',')[0];
      this.model.content = reader.result.toString().split(',')[1];
      console.log(this.model);
      this._courseService.addUserAssignment(this.model).then(x => {
        this.model.content = null;
      });
    };
    
    this.getAssignments();
  }
}