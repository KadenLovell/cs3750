import { Component, ViewChild, TemplateRef } from '@angular/core';
import { Location } from "@angular/common";
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

// shared
import { UserService } from "./shared/user/user.service";
import { User } from "./shared/user/user";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent {
  @ViewChild('modalContent', { static: true }) modalContent: TemplateRef<any>;
  user: User;
  isLogin: boolean;
  sidebar: boolean = false;

  constructor(
    private readonly router: Router,
    private location: Location,
    private readonly _userService: UserService,
    private modal: MatDialog) {
    router.events.subscribe(() => {
      if (location.path() === "/login") {
        this.isLogin = true;
      } else {
        this.isLogin = false;
      }
    });
  }

  toggleSidebar() {
    if (!this.sidebar) {
      this.user = this._userService.user;
    }

    this.sidebar = !this.sidebar
  }

  clicked() {
    // placeholder
    this.sidebar = false;
  }

  openModal(): void {
    // Notifications
    this.modal.open(this.modalContent, { width: '750px', data: {} });
  }
}
