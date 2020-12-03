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
    this.calculateFees();
  }

  calculateFees(){
    console.log("this.user.paid = " + this.user.paid)
    if (this.user.paid == false){
    this._courseSearchService.getUserCourses().then(response => {
      this.user.fees = 0;
      for (var i = 0; i < response.length; i++) {
        this.user.fees += 800 * response[i].credits;
        console.log(800 * response[i].credits);
      }
      this._courseSearchService.updateFees(this.user).then(response => {
      });
      this._totalAmount = this.user.fees;
    });
    console.log(this.user.fees);
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

  async createStripeToken(amount) {
    const { token, error } = await stripe.createToken(this.card);
    if (token) {
      console.log("this is the amount = " + amount + " create");
      this.onSuccess(token, amount);
      this.sendPostRequest(token, amount);
    } else {
      this.onError(error);
    }
  }

  payDifferentAmount() {
    var inputValue = (<HTMLInputElement>document.getElementById("amount")).value;
    this.createStripeToken(inputValue);
    console.log("this is the amount = " + inputValue + " in Pay different");

  }


  onSuccess(token, amount) {
    if (this.user.paid == false){
    console.log("this is the amount = " + amount);
    if(amount < this.user.fees){
      this.user.fees = this.user.fees - amount;
    }
    else{
      this.user.fees = 0;
      this.user.paid = true;
    }
    this._courseSearchService.updateFees(this.user).then(response => {
      this._totalAmount = this.user.fees;
      if(this._totalAmount == 0){
        this.user.paid = true;
      }
      this._courseSearchService.updatePaid(this.user).then(response => {
      });
    });
  }
  }

  onError(error) {
    if (error.message) {
      this.cardError = error.message;
    }
  }

  sendPostRequest(token, amount) {
    const body = new HttpParams({
      fromObject: {
        amount: amount, // amount is equal to amount user chooses
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
