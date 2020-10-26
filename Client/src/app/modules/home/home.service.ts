import { Injectable } from "@angular/core";
import { HttpService } from "../../shared/http/http.service";

@Injectable()
export class HomeService {
    constructor(private readonly _httpService: HttpService) { }
    async getCourses(instructorId: number | string): Promise<any> {
        const url = `/api/course/list/${instructorId}`;

        return await this._httpService.get(url);
    }
}