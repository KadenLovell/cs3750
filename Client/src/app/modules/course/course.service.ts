import { Injectable } from "@angular/core";
import { HttpService } from "../../shared/http/http.service";

@Injectable()
export class CourseService {
    constructor(private readonly _httpService: HttpService) { }
    async getCourse(courseId: number | string): Promise<any> {
        const url = `/api/course/${courseId}`;

        return await this._httpService.get(url);
    }

    async addAssignment(model: any): Promise<any> {
        const url = `/api/assignment/add`;

        return await this._httpService.post(url, model);
    }

    async getAssignments(courseId: number | string): Promise<any> {
        const url = `/api/assignment/list/${courseId}`;

        return await this._httpService.get(url);
    }
}