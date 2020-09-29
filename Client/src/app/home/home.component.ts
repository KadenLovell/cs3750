import { Component, ViewChild, OnInit, TemplateRef } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { Router, ActivatedRoute } from '@angular/router';
import { HomeService } from './home.service';
import { MatDialog } from '@angular/material/dialog';

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

  constructor(private route: ActivatedRoute, private readonly router: Router, private readonly _homeService: HomeService, private breakpointObserver: BreakpointObserver, private modal: MatDialog) { }

  ngOnInit(): void {
    this.view = 1;
    this.model = {};
  }

  openModal(): void {
    // Notifications
    const modal = this.modal.open(this.modalContent, { width: '750px', data: {} });
    modal.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  //TODO THIS WILL BE CHANGED TO WHATEVER SERVICE(S) WE NEED
  createUser() {
    // if there are loading animations, handle the state here: IE: this.loading = true;
    this._homeService.addUser(this.model).then(response => {
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
