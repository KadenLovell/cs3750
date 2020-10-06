import { Injectable } from "@angular/core";

import { HttpService } from "../http.service";

@Injectable()
export class ProfilePageService {
    constructor(private readonly _httpService: HttpService) { }
    async saveProfile(model: any): Promise<any> {
        const url = `/api/saveprofile`;

        return await this._httpService.post(url, model);
    }
}