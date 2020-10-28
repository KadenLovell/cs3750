import { Component, OnInit } from '@angular/core';
import { TuitionAndFeesService } from './tuitionandfees.service';

@Component({
  selector: 'app-tuitionandfees',
  templateUrl: './tuitionandfees.component.html',
  styleUrls: ['./tuitionandfees.component.scss'],
  providers: [TuitionAndFeesService]
})
export class TuitionAndFeesComponent implements OnInit {
  model: any;
  response: any;
  errors: any;
  view: any;
  rows: any;

  constructor(private readonly _tuitionAndFeesService: TuitionAndFeesService) { }

  ngOnInit(): void {
    this.view = 1;
    this.model = {};
  }

  resetViewstate() {
    this.errors = {};
  }

}
