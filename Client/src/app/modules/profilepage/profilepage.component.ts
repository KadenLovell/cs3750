import { Component, ViewChild, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { ProfilePageService } from './profilepage.service';
import { BaseComponent } from '../../base/base.component';

@Component({
  selector: 'app-profilepage',
  templateUrl: './profilepage.component.html',
  styleUrls: ['./profilepage.component.scss'],
  providers: [ProfilePageService]
})
export class ProfilePageComponent extends BaseComponent implements OnInit {
  @ViewChild("form") form: any;
  model: any;
  response: any;
  errors: any;
  view: any;

  constructor(
    private route: ActivatedRoute,
    private readonly router: Router,
    private readonly _profilePageService: ProfilePageService) {
    super();
  }

  ngOnInit(): void {
    this.view = 1;
    this.model = {};

    this._profilePageService.loadUser().then(response => {
      this.model = response;
      console.log(this.model);
    });
  }

  save() {
    this._profilePageService.updateUser(this.model).then(response => {
      console.log(this.model.avatar);
      this.model = response;
      this.errors = response.errors;

      if (this.response && this.response.success) {
        this.view = 1;
      }
    });
  }

  handleUpload(event) {
    const file = event.target.files[0];
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      console.log(reader.result);
      this.model.avatar = reader.result.toString().split(',')[1];
    };
  }
}
