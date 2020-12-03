import { Component, OnInit, ViewChild } from "@angular/core";
import { Router, ActivatedRoute } from '@angular/router';

import { LoginService } from './login.service';

// shared
import { User } from "../../shared/user/user";
import { UserService } from "../../shared/user/user.service";
import { BaseComponent } from '../../base/base.component';

declare var particlesJS: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  providers: [LoginService]
})
export class LoginComponent extends BaseComponent implements OnInit {
  model: any;
  response: any;
  errors: any;
  view: any;

  @ViewChild("form") form: any;

  constructor(
    private route: ActivatedRoute,
    private readonly router: Router,
    private readonly _loginService: LoginService,
    private readonly _userService: UserService) {
    super();
  }

  ngOnInit(): void {
    particlesJS.load('particles-js', 'assets/particles.json');
    this.view = 1;
    this.model = {};
    this._loginService.logout(this.model);

    // initialize model defaults:
    this.model.dateOfBirth = [];
    this.model.role = false;
  }

  login() {
    if (this.isValid(this.form)) {
      this._loginService.login(this.model).then(response => {
        this.response = response;
        this.errors = response.errors;

        const user: User = {
          id: this.response.user.id,
          username: this.response.user.username,
          firstname: this.response.user.firstname,
          lastname: this.response.user.lastname,
          email: this.response.user.email,
          role: this.response.user.role,
          authorized: true,
          fees: this.response.user.fees,
          paid: this.response.user.paid
        };

        this._userService.setUser(user);
        if (this.response && this.response.success) {
          this.router.navigate(['../home'], { relativeTo: this.route });
        }
      });
    }
  }

  createUser() {
    if (this.isValid(this.form)) {
      this._loginService.addUser(this.model).then(response => {
        this.response = response;
        this.errors = response.errors;

        if (this.errors) {
          return;
        }

        if (this.response && this.response.success) {
          this.router.navigate(['../home'], { relativeTo: this.route });
        }
      });
    }
  }

  resetViewstate() {
    this.errors = {};
  }
}
