import { Injectable } from "@angular/core";
import { HttpService } from "../../shared/http/http.service";

@Injectable()
export class TuitionAndFeesService {
    constructor(private readonly _httpService: HttpService) { }
    async postStripeCharge(url, model: any): Promise<any> {
        await this._httpService.postStripe(url, model);
    }
}
