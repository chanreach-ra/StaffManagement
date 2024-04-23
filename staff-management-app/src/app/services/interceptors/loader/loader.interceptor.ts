import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, finalize } from 'rxjs';
import { LoaderService } from 'src/app/services/loader/loader.service';

@Injectable()
export class LoaderInterceptor implements HttpInterceptor {
  totalRequests = 0;
  completedRequests = 0;
  constructor(private loader: LoaderService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.loader.show();
    this.totalRequests++;

    return next.handle(request).pipe(
      finalize(() => {
        this.completedRequests++;

        if (this.completedRequests === this.totalRequests) {
          this.completedRequests = 0;
          this.totalRequests = 0;
          this.loader.hide();
        }
      }
      ),
    );
  }
}
