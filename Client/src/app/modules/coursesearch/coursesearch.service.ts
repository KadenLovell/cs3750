import { Injectable } from "@angular/core";

// shared
import { HttpService } from "../../shared/http/http.service";

@Injectable()
export class CourseSearchService {
    constructor(private readonly _httpService: HttpService) { }
    async getClasses(): Promise<any> {
        const url = `/api/course/list`;

        return await this._httpService.get(url);
    }

    async searchClasses(model: any): Promise<any> {
        const url = `/api/course/searchlist`;

        return await this._httpService.post(url, model);
    }
}
