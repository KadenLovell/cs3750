import { HttpClient, HttpParams } from '@angular/common/http';
import { OnInit, AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnDestroy, ViewChild } from '@angular/core';
import { TuitionAndFeesService } from './tuitionandfees.service';
import { CourseSearchService } from '../coursesearch/coursesearch.service';
import { User } from "../../shared/user/user";
import { UserService } from "../../shared/user/user.service";

@Component({
  selector: 'app-stripe-payment',
  templateUrl: './tuitionandfees.component.html',
  styleUrls: ['./tuitionandfees.component.scss'],
  providers: [TuitionAndFeesService, CourseSearchService]
})
export class TuitionAndFeesComponent implements OnDestroy, AfterViewInit, OnInit {
  [x: string]: any;
  @ViewChild('cardInfo') cardInfo: ElementRef;
  model: any;
  _totalAmount: number;
  card: any;
  cardHandler = this.onChange.bind(this);
  cardError: string;
  get user(): User {
    return this._userService.user;
  }
  constructor(private readonly _tuitionAndFeesService: TuitionAndFeesService, private cd: ChangeDetectorRef, private http: HttpClient, private readonly _userService: UserService, private readonly _courseSearchService: CourseSearchService) { }

  ngOnDestroy() {
    if (this.card) {
      // We remove event listener here to keep memory clean
      this.card.removeEventListener('change', this.cardHandler);
      this.card.destroy();
    }
  }

  ngOnInit(): void {
    this.model = {};
    this._totalAmount = this.user.fees;
  }

  ngAfterViewInit() {
    this.initiateCardElement();
  }

  initiateCardElement() {
    // Giving a base style here, but most of the style is in scss file
    const cardStyle = {
      base: {
        color: '#32325d',
        fontFamily: '"Helvetica Neue", Helvetica, sans-serif',
        fontSmoothing: 'antialiased',
        fontSize: '16px',
        '::placeholder': {
          color: '#aab7c4',
        },
      },
      invalid: {
        color: '#fa755a',
        iconColor: '#fa755a',
      },
    };
    this.card = elements.create('card', { cardStyle });
    this.card.mount(this.cardInfo.nativeElement);
    this.card.addEventListener('change', this.cardHandler);
  }

  onChange({ error }) {
    if (error) {
      this.cardError = error.message;
    } else {
      this.cardError = null;
    }
    this.cd.detectChanges();
  }

  async createStripeToken() {
    const { token, error } = await stripe.createToken(this.card);
    if (token) {
      console.log("this is the amount = " + this.model.amount + " create");
      this.onSuccess(token);
      this.sendPostRequest(token);
    } else {
      this.onError(error);
    }
  }

  onSuccess(token) {
    console.log("this is the amount = " + this.model.amount);
    if(this.model.amount <= this.user.fees){
      this.user.fees = this.user.fees - this.model.amount;
    }
    this._courseSearchService.updateFees(this.user).then(response => {
      this._totalAmount = this.user.fees;
      this._courseSearchService.updatePaid(this.user).then(response => {
      });
    });
  
  }

  onError(error) {
    if (error.message) {
      this.cardError = error.message;
    }
  }

  sendPostRequest(token) {
    const body = new HttpParams({
      fromObject: {
        amount: (this.model.amount), // amount is equal to amount user chooses
        currency: 'usd',
        source: `${token.id}`,
        description: 'testAPIcall',
      }
    });
    this._tuitionAndFeesService.postStripeCharge(`https://api.stripe.com/v1/charges`, body).then(response => {
      // do stuff here eventually
    });
  }
}
