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

<<<<<<< HEAD
    async getUserCourses(): Promise<any> {
        const url = `/api/usercourses/list`;

        return await this._httpService.get(url);
=======
        async registerUserCourse(model: any): Promise<any> {
        const url = `/api/usercourses/add`;

        return await this._httpService.post(url, model);
>>>>>>> 878a2b78791e2d731a017d118b35eb2635c57394
    }
}
