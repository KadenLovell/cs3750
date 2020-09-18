import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from '@angular/router';

import { LoginService } from './login.service';
declare var particlesJS: any;
// shared

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

  constructor(private route: ActivatedRoute, private readonly router: Router, private readonly _loginService: LoginService) { }
  ngOnInit(): void {
    particlesJS.load('particles-js', 'assets/particles.json', function () {
      console.log('callback - particles.js config loaded');
    });

    this.view = 1;
    this.model = {};
    this.model.dateOfBirth = [];
  }

  resetPassword() {
    // TODO: 
    // send questions + new password to server,
    // validate the questions on the server for security,
    // if no match, show error message, else update password
    return;
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

  loadQuestions() {
    // TODO:
    // pass username to server
    // if server returns status 200, set questions in the model
    // if server returns error, "username doesn't exist"
    return;
  }

  resetViewstate() {
    this.errors = {};
  }
}
