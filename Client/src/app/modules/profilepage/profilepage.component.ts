import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { ProfilePageService } from './profilepage.service';
@Component({
  selector: 'app-profilepage',
  templateUrl: './profilepage.component.html',
  styleUrls: ['./profilepage.component.scss'],
  providers: [ProfilePageService]
})
export class ProfilePageComponent implements OnInit {
  model: any;
  response: any;
  errors: any;
  view: any;

  constructor(private route: ActivatedRoute, private readonly router: Router, private readonly _profilePageService: ProfilePageService) { }

  ngOnInit(): void {
    this.view = 1;
    this.model = {};

    this._profilePageService.loadUser(1).then(response => {
      this.model = response;
    });
  }

  save() {
    // if there are loading animations, handle the state here: IE: this.loading = true;
    this._profilePageService.updateUser(this.model).then(response => {
      // if there are loading animations, handle the state here: IE: this.loading = false;
      this.response = response;
      console.log(this.response);
      this.errors = response.errors;

      // if result was successful: set shared user, then route to home component
      if (this.response && this.response.success) {
        this.view = 2;
      }
    });
  }
}
