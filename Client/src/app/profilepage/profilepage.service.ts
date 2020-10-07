import { Injectable } from "@angular/core";

import { HttpService } from "../http.service";

@Injectable()
export class ProfilePageService {
    constructor(private readonly _httpService: HttpService) { }
    async loadUser(userId: any): Promise<any> {
        const url = `/api/user/${userId}`;

        return await this._httpService.get(url);
    }

    async updateUser(model: any): Promise<any> {
        const url = `/api/user/update`;

        return await this._httpService.post(url, model);
    }
}