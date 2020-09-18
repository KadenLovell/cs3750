import { Injectable } from "@angular/core";
import { HttpEventType, HttpClient, HttpErrorResponse, HttpRequest, HttpResponse } from "@angular/common/http";
import { Router } from "@angular/router";
import { retry } from "rxjs/operators";

// shared
@Injectable()
export class HttpService {
    base = 'https://localhost:5001';
    constructor(private readonly _router: Router, private readonly _httpClient: HttpClient) { }

    get(url: string): Promise<any> {
        const result =
            this._httpClient
                .get(this.base + url)
                .pipe(retry(3))
                .toPromise()
                .then(response => {
                    return Promise.resolve(response);
                })
                .catch(err => {
                    if (this.attemptViewDestroyedError(err)) {
                        return Promise.resolve(null);
                    }

                    if (err && err.status === 401) {
                        // this.navigate("/login");
                        return Promise.reject(err);
                    }

                    if (err && err.status === 403) {
                        // different from post
                        this._router.navigate(["/"]);

                        return Promise.reject(err);
                    }

                    // this._alertsService.addResponse(err);

                    return Promise.resolve(null);
                });

        return result;
    }

    post(url: string, model = null): Promise<any> {
        const result =
            this
                ._httpClient
                .post(this.base + url, model)
                .toPromise()
                .then(response => {
                    return Promise.resolve(response);
                })
                .catch(err => {
                    if (err && err.status === 401) {
                        // this.navigate("/login");

                        return Promise.reject(err);
                    }

                    // this._alertsService.addResponse(err);

                    return Promise.resolve({});
                });

        return result;
    }

    upload(url: string, formData: FormData, uploading: (percentage: number) => void, uploaded: (response) => void, error: (reason: string) => void, completed: () => void) {
        const req = new HttpRequest("POST", this.base + url, formData, {
            reportProgress: true,
        });

        this
            ._httpClient
            .request(req)
            .subscribe(
                (event) => {
                    if (event.type === HttpEventType.UploadProgress) {
                        uploading(Math.round(event.loaded / event.total * 100));
                    }
                    else if (event instanceof HttpResponse) {
                        uploaded(event.body);
                    }
                },
                (err: HttpErrorResponse) => {
                    // const reason = this._alertsService.getReason(err);

                    // error(reason);
                },
                () => {
                    completed();
                }
            );
    }

    private attemptViewDestroyedError(err): boolean {
        if (!err.message.startsWith("ViewDestroyedError:")) {
            return false;
        }

        console.error(err.message);

        return true;
    }
}