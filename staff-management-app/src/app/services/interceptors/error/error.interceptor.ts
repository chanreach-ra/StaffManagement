import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import Swal from 'sweetalert2';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor() { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((err) => {
        setTimeout(() => {
          if (err.status === 406) {
            if (err.errors) {
              Swal.fire({
                animation: false,
                backdrop: false,
                allowOutsideClick: false,
                icon: 'error',
                title: 'ERORR',
                text: err?.error?.message ?? 'Something is wrong!',
                showConfirmButton: false,
                showCloseButton: true
              })
            }
            else {
              Swal.fire({
                animation: false,
                backdrop: false,
                allowOutsideClick: false,
                icon: 'error',
                title: 'ERORR',
                text: err?.error?.message ?? 'Something is wrong!',
                showConfirmButton: false,
                showCloseButton: true
              })
            }
          } else {
            Swal.fire({
              animation: false,
              backdrop: false,
              allowOutsideClick: false,
              icon: 'error',
              title: 'ERORR',
              text: err?.error?.message ?? 'Something is wrong!',
              showConfirmButton: false,
              showCloseButton: true
            })
          }
        }, 1);

        const error = err?.error?.message || err.statusText;
        return throwError(() => error);
      })
    );
  }
}
