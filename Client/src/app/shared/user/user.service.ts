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
    loadUser() {
        const url = `/api/user/activeuser`;
        this._httpService.get(url).then(activeUser => {
            try {
                const user: User = {
                    id: activeUser.id,
                    username: activeUser.username,
                    firstname: activeUser.firstname,
                    lastname: activeUser.lastname,
                    email: activeUser.email,
                    role: activeUser.role,
                    authorized: true,
                    fees: activeUser.fees
                }

                this.user = user;
            }

            catch (e) {
                const user: User = {
                    id: null,
                    username: null,
                    firstname: null,
                    lastname: null,
                    email: null,
                    role: null,
                    authorized: false,
                    fees: null
                };
                this.user = user;

                this._router.navigateByUrl("https://localhost:4200/login");
            }
        });
    }

    setUser(user: User) {
        this.user = user;
    }
}