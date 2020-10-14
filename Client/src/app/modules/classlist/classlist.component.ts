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

  constructor(private readonly _classListService: ClassListService) { }

  ngOnInit(): void {
    this.view = 1;
    this.model = {};
    this._classListService.getClasses().then(response => {
      this.rows = response;
    });
  }

  save() {
    this._classListService.addClass(this.model);
  }
}
