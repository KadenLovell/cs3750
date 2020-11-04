import { HttpClient, HttpParams } from '@angular/common/http';
import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnDestroy, ViewChild } from '@angular/core';
import { TuitionAndFeesService } from './tuitionandfees.service';

@Component({
  selector: 'app-stripe-payment',
  templateUrl: './tuitionandfees.component.html',
  styleUrls: ['./tuitionandfees.component.scss'],
  providers: [TuitionAndFeesService]
})
export class TuitionAndFeesComponent implements OnDestroy, AfterViewInit {
  @ViewChild('cardInfo') cardInfo: ElementRef;
  _totalAmount: number;
  card: any;
  cardHandler = this.onChange.bind(this);
  cardError: string;
  constructor(private readonly _tuitionAndFeesService: TuitionAndFeesService, private cd: ChangeDetectorRef, private http: HttpClient) { }

  ngOnDestroy() {
    if (this.card) {
      // We remove event listener here to keep memory clean
      this.card.removeEventListener('change', this.cardHandler);
      this.card.destroy();
    }
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
      this.onSuccess(token);
      this.sendPostRequest(token);
    } else {
      this.onError(error);
    }
  }

  onSuccess(token) {
  }

  onError(error) {
    if (error.message) {
      this.cardError = error.message;
    }
  }

  sendPostRequest(token) {
    const body = new HttpParams({
      fromObject: {
        amount: '200',
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