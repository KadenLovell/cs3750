import { Component, OnInit } from '@angular/core';
import { CourseSearchService } from './coursesearch.service';

@Component({
  selector: 'app-coursesearch',
  templateUrl: './coursesearch.component.html',
  styleUrls: ['./coursesearch.component.scss'],
  providers: [CourseSearchService]
})
export class CourseSearchComponent implements OnInit {
  model: any;
  response: any;
  errors: any;
  view: any;
  rows: any;
  
  displayedColumns: string[] = ['id', 'name', 'department', 'instructor', 'code'];

  constructor(private readonly _courseSearchService: CourseSearchService) { }

  ngOnInit(): void {
    this.view = 1;
    this.model = {};
    this._courseSearchService.getClasses().then(response => {
      this.rows = response; // this.rows is an array of json objects for the classes
      console.log(this.rows);
    });
  }

  search() {
    this._courseSearchService.searchClasses(this.model).then(response => {
      this.rows = response; // this.rows is an array of json objects for the classes
      console.log(this.rows);
    });
  }

}
