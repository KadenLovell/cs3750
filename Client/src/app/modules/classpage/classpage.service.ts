import { Injectable } from "@angular/core";
import { HttpService } from "../../shared/http/http.service";

@Injectable()
export class ClasspageService {
    constructor(private readonly _httpService: HttpService) { }

    addClass() {
        // const = api/class/add
          }
          
          loadClasses() {
        //  const = api/class/list
          }
          
          loadClass() {
        //  const = /api/c;lass/${classId}
          }
          
          updateClass() {
         // const = /api/class/update
          }
}
