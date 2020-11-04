import { Injectable } from "@angular/core";

// shared
import { HttpService } from "../../shared/http/http.service";

@Injectable()
export class ProfilePageService {
    constructor(private readonly _httpService: HttpService) { }
    async loadUser(): Promise<any> {
        const url = `/api/user/loaduser`;

        return await this._httpService.get(url);
    }

    async updateUser(model: any): Promise<any> {
        const url = `/api/user/update`;

        return await this._httpService.post(url, model);
    }
}