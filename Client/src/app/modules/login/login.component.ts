import { Component, OnInit, ViewChild } from "@angular/core";
import { FormControl, NgForm, NgModelGroup } from "@angular/forms";
import { Router, ActivatedRoute } from '@angular/router';

import { LoginService } from './login.service';

// shared
import { User } from "../../shared/user/user";
import { UserService } from "../../shared/user/user.service";

declare var particlesJS: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  providers: [LoginService]
})
export class LoginComponent implements OnInit {
  model: any;
  response: any;
  errors: any;
  view: any;

  @ViewChild("form") form: any;

  constructor(
    private route: ActivatedRoute,
    private readonly router: Router,
    private readonly _loginService: LoginService,
    private readonly _userService: UserService) { }

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
          authorized: true
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


  // improve this function by removing all logic outside of making control marked as touched
  isValid(container: NgForm | NgModelGroup): boolean {
    const form = container as NgForm;
    const group = container as NgModelGroup;

    const formGroup =
      form.form
        ? form.form
        : group.control;

    let count = 0;

    for (const k in formGroup.controls) {
      if (!Object.prototype.hasOwnProperty.call(formGroup.controls, k)) {
        continue;
      }

      const control = formGroup.get(k) as FormControl;

      if (!control) {
        continue;
      }

      control.markAsTouched();

      count += this.countErrors(control.errors);
    }

    return count === 0;
  }

  private countErrors(errors: Record<string, any> | null): number {
    if (!errors) {
      return 0;
    }

    let count = 0;

    for (const k in errors) {
      if (!Object.prototype.hasOwnProperty.call(errors, k)) {
        continue;
      }

      if (k !== "remote") {
        count++;
      }
    }

    return count;
  }
}
