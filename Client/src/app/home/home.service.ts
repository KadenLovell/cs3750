import { Injectable } from "@angular/core";

import { HttpService } from "../http.service";

@Injectable()
export class HomeService {
    constructor(private readonly _httpService: HttpService) { }
    async addUser(model: any): Promise<any> {
        const url = `/api/user/add`;

        return await this._httpService.post(url, model);
    }
}