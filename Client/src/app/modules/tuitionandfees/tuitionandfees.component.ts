import { Component, OnInit } from '@angular/core';
import { TuitionAndFeesService } from './tuitionandfees.service';
import { BaseComponent } from '../../base/base.component';

@Component({
  selector: 'app-tuitionandfees',
  templateUrl: './tuitionandfees.component.html',
  styleUrls: ['./tuitionandfees.component.scss'],
  providers: [TuitionAndFeesService]
})
export class TuitionAndFeesComponent extends BaseComponent implements OnInit {
  model: any;
  response: any;
  errors: any;
  view: any;
  rows: any;

  constructor(private readonly _tuitionAndFeesService: TuitionAndFeesService) {
    super();
  }

  ngOnInit(): void {
    this.view = 1;
    this.model = {};
  }

  resetViewstate() {
    this.errors = {};
  }

}
