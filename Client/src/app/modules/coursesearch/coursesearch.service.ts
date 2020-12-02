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

    async registerUserCourse(model: any): Promise<any> {
        const url = `/api/usercourses/add`;

        return await this._httpService.post(url, model);
    }

    async updateFees(model: any): Promise<any> {
        const url = `/api/user/updatefees`;

        return await this._httpService.post(url, model);
    }

    async getUserCourses(): Promise<any> {
        const url = `/api/usercourses/list`;

        return await this._httpService.get(url);
    }

    async deleteUserCourse(model: any): Promise<any> {
        const url = `/api/usercourses/delete`;

        return await this._httpService.post(url, model);
    }
}
