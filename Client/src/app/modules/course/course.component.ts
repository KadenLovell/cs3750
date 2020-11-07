import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../../base/base.component';

// shared
import { User } from "../../shared/user/user";
import { UserService } from "../../shared/user/user.service";

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.scss']
})
export class CourseComponent extends BaseComponent implements OnInit {
  model: any;
  rows: any;
  errors: any;
  view: any;

  get user(): User {
    return this._userService.user;
  }

  constructor(private readonly _userService: UserService) {
    super();
  }

  ngOnInit(): void {
    this.view = 1;
    this.model = {};
  }
}
