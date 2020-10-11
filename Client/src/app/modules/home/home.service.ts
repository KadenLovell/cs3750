import { Injectable } from "@angular/core";
import { HttpService } from "../../shared/http/http.service";

@Injectable()
export class HomeService {
    constructor(private readonly _httpService: HttpService) { }
}