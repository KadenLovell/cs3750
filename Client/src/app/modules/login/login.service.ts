import { Injectable } from "@angular/core";

import { HttpService } from "../../shared/http/http.service";

@Injectable()
export class LoginService {
    constructor(private readonly _httpService: HttpService) { }
    async login(model: any): Promise<any> {
        const url = `/api/login/login`;

        return await this._httpService.post(url, model);
    }

    async logout(): Promise<any> {
        const url = `/api/login/logout`;

        return await this._httpService.post(url);
    }

    async addUser(model: any): Promise<any> {
        const url = `/api/user/add`;

        return await this._httpService.post(url, model);
    }
}