import { Injectable } from "@angular/core";

// shared
import { HttpService } from "../../shared/http/http.service";

@Injectable()
export class ClassListService {
    constructor(private readonly _httpService: HttpService) { }
    async addClass(model: any): Promise<any> {
        const url = `/api/class/add`;

        return await this._httpService.post(url, model);
    }
}