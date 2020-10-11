import { Component, OnInit } from "@angular/core";
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

  constructor(
    private route: ActivatedRoute,
    private readonly router: Router,
    private readonly _loginService: LoginService) { }

  ngOnInit(): void {
    particlesJS.load('particles-js', 'assets/particles.json', function () {
      console.log('callback - particles.js config loaded');
    });
    this._loginService.logout();

    this.view = 1;
    this.model = {};
    this.model.dateOfBirth = [];
  }

  login() {
    // if there are loading animations, handle the state here: IE: this.loading = true;
    this._loginService.login(this.model).then(response => {
      // if there are loading animations, handle the state here: IE: this.loading = false;
      this.response = response;
      this.errors = response.errors;

      // if result was successful: set shared user, then route to home component
      if (this.response && this.response.success) {
        this.router.navigate(['../home'], { relativeTo: this.route });
      }
    });
  }

  createUser() {
    // if there are loading animations, handle the state here: IE: this.loading = true;
    this._loginService.addUser(this.model).then(response => {
      // if there are loading animations, handle the state here: IE: this.loading = false;
      this.response = response;
      this.errors = response.errors;

      if (this.errors) {
        return;
      }

      // if result was successful: set shared user, then route to home component
      if (this.response && this.response.success) {
        this.router.navigate(['../home'], { relativeTo: this.route });
      }
    });
  }

  resetViewstate() {
    this.errors = {};
  }
}
