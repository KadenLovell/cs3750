import { Component, OnInit } from '@angular/core';
import { Location } from "@angular/common";
import { Router, ActivatedRoute } from '@angular/router';

// shared
import { User } from "../../shared/user/user";
import { UserService } from "../../shared/user/user.service";

@Component({
  selector: 'sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  ready: boolean;
  isLogin: boolean;
  sidebar: boolean = false;

  constructor(
    private ActivatedRoute: ActivatedRoute,
    private location: Location,
    private readonly router: Router,
    private readonly _userService: UserService) {
    router.events.subscribe(() => {
      if (location.path() === "/login") {
        this.isLogin = true;
      } else {
        this.isLogin = false;
      }
    });
  }

  get user(): User {
    return this._userService.user;
  }

  ngOnInit(): void {
    this._userService.loadUser().then(() => {
      this.ready = true;
    });
  }
}
