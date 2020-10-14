import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { User } from './user';

// shared
import { HttpService } from "../http/http.service";

@Injectable({
    providedIn: 'root'
})
export class UserService {
    user: User;

    constructor(private readonly _httpService: HttpService, private readonly _router: Router) { }
    async loadUser() {
        try {
            const url = `/api/user/activeuser`;
            var activeUser = await this._httpService.get(url);
            const user: User = {
                id: activeUser.id,
                username: activeUser.username,
                firstname: activeUser.firstname,
                lastname: activeUser.lastname,
                email: activeUser.email,
                role: activeUser.role // dch
            }
            this.user = user;
        }
        catch (e) {
            this._router.navigateByUrl("https://localhost:4200/login");
        }
    }
}
