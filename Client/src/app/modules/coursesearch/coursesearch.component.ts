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
      }
    })
  }

  register(id) {
    this.model.courseId = id;
    this.model.studentId = this.user.id;

    this._courseSearchService.registerUserCourse(this.model).then(response => { // returns a false if registration failed
      //console.log("Logging course register response: " + response.toString());
      if (response == false) {
        // var x = document.getElementById("toast");
        // document.getElementById("toast-header").innerHTML = "Operation failed";
        // document.getElementById("toast-body").innerHTML = "Could not add registration for class";
        // x.className = "show";
        // setTimeout(function(){ x.className = x.className.replace("show", ""); }, 3000);

      }
      else {        
        // var x = document.getElementById("toast");
        // document.getElementById("toast-header").innerHTML = "Operation success";
        // document.getElementById("toast-body").innerHTML = "Added registration for class";
        // x.className = "show";
        // setTimeout(function(){ x.className = x.className.replace("show", ""); }, 3000);
      }
      this.userCourseIds.push(id);
    });
  }
}
