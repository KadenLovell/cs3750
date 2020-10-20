import { Component, OnInit, ViewChild } from "@angular/core";
import { FormControl, NgForm, NgModelGroup } from "@angular/forms";
import { Router, ActivatedRoute } from '@angular/router';

import { LoginService } from './login.service';

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
    private readonly _loginService: LoginService) { }

  ngOnInit(): void {
    particlesJS.load('particles-js', 'assets/particles.json');
    this.view = 1;
    this.model = {};

    // initialize model defaults:
    this.model.dateOfBirth = [];
    this.model.role = false;
  }

  login() {
    if (this.isValid(this.form)) {
      this._loginService.login(this.model).then(response => {
        this.response = response;
        this.errors = response.errors;

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
