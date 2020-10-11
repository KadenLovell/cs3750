import { Component, ViewChild, OnInit, TemplateRef } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { Router, ActivatedRoute } from '@angular/router';
import { HomeService } from './home.service';
import { MatDialog } from '@angular/material/dialog';

// shared
import { User } from "../../shared/user/user";
import { UserService } from "../../shared/user/user.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  providers: [HomeService]
})
export class HomeComponent implements OnInit {
  model: any;
  response: any;
  errors: any;
  view: any;

  @ViewChild('modalContent', { static: true }) modalContent: TemplateRef<any>;

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  constructor(
    private route: ActivatedRoute,
    private readonly router: Router,
    private readonly _homeService: HomeService,
    private breakpointObserver: BreakpointObserver,
    private readonly _userService: UserService,
    private modal: MatDialog) { }

  get user(): User {
    return this._userService.user;
  }

  ngOnInit(): void {
    this._userService.loadUser();
    this.view = 1;
    this.model = {};
  }

  openModal(): void {
    // Notifications
    this.modal.open(this.modalContent, { width: '750px', data: {} });
  }

  resetViewstate() {
    this.errors = {};
  }
}
