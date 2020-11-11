import { Component, OnInit } from '@angular/core';
import { CourseSearchService } from './coursesearch.service';
import { BaseComponent } from '../../base/base.component';

@Component({
  selector: 'app-coursesearch',
  templateUrl: './coursesearch.component.html',
  styleUrls: ['./coursesearch.component.scss'],
  providers: [CourseSearchService]
})
export class CourseSearchComponent extends BaseComponent implements OnInit {
  model: any;
  response: any;
  errors: any;
  view: any;
  rows: any;

  displayedColumns: string[] = ['id', 'name', 'department', 'instructor', 'code'];

  constructor(private readonly _courseSearchService: CourseSearchService) {
    super();
  }

  ngOnInit(): void {
    this.view = 1;
    this.model = {};
    this._courseSearchService.getClasses().then(response => {
      this.rows = response;
    });
  }

  search() {
    this._courseSearchService.searchClasses(this.model).then(response => {
      this.rows = response;
    });
  }

  register() {
    // dch need to know courseSearchService register method
  }
}
