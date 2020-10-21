import { Component, OnInit } from '@angular/core';
import { ClassListService } from './classlist.service';

@Component({
  selector: 'app-classlist',
  templateUrl: './classlist.component.html',
  styleUrls: ['./classlist.component.scss'],
  providers: [ClassListService]
})
export class ClasslistComponent implements OnInit {
  model: any;
  response: any;
  errors: any;
  view: any;
  rows: any;
  
  displayedColumns: string[] = ['id', 'name', 'department', 'instructor', 'code'];

  constructor(private readonly _classListService: ClassListService) { }

  ngOnInit(): void {
    this.view = 1;
    this.model = {};
    this._classListService.getClasses().then(response => {
      this.rows = response; // this.rows is an array of json objects for the classes
      console.log(this.rows);
    });
  }

  save() {
    this._classListService.addClass(this.model);
  }
}
