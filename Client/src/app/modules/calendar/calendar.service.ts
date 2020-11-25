import { Injectable } from "@angular/core";

// shared
import { HttpService } from "../../shared/http/http.service";

@Injectable()
export class CalendarService {
    constructor(private readonly _httpService: HttpService) { }
    async getCourses(): Promise<any> {
        const url = `/api/course/list`;

        return await this._httpService.get(url);
    }
}