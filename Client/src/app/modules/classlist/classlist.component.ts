import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-classlist',
  templateUrl: './classlist.component.html',
  styleUrls: ['./classlist.component.scss']
})
export class ClasslistComponent implements OnInit {
  model: any;
  response: any;
  errors: any;
  view: any;

  constructor() { }

  ngOnInit(): void {
    this.view = 1;
    this.model = {};
  }

  save() {

  }
}
