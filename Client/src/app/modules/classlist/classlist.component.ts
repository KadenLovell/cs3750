import { Component, ViewChild, OnInit } from '@angular/core';
import { ClassListService } from './classlist.service';
import { BaseComponent } from '../../base/base.component';

@Component({
  selector: 'app-classlist',
  templateUrl: './classlist.component.html',
  styleUrls: ['./classlist.component.scss'],
  providers: [ClassListService]
})
export class ClasslistComponent extends BaseComponent implements OnInit {
  @ViewChild("form") form: any;
  model: any;
  response: any;
  errors: any;
  view: any;
  rows: any;

  displayedColumns: string[] = ['id', 'name', 'department', 'instructor', 'code'];

  constructor(private readonly _classListService: ClassListService) {
    super();
  }

  ngOnInit(): void {
    this.view = 1;
    this.model = {};

    // initialize model defaults:
    this.model.monday = false;
    this.model.tuesday = false;
    this.model.wednesday = false;
    this.model.thursday = false;
    this.model.friday = false;
  }

  save() {
    if (this.isValid(this.form)) {
      this._classListService.addClass(this.model);
      this.model = {};
    }
  }
}
