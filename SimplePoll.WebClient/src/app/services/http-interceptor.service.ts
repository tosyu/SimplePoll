import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(
    private router: Router,
    private snackBar: MatSnackBar
  ) { }

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      tap({
        next: () => null,
        error: (error: HttpErrorResponse) => {
          if (error) {
            switch (error.status) {
              case 401:
              case 403:
                this.router.navigate(["/login"]);
                break;
              case 404:
                this.snackBar.open("Zasób nie istnieje", "Zamknij"); // should be i18n, but not required by task
                break;
              case 400:
                this.snackBar.open("Błąd komunikacji z serwerem", "Zamknij"); // should be i18n, but not required by task
                break;
              default:
                this.snackBar.open("Błąd komunikacji serwera, spróbuj ponownie później", "Zamknij"); // should be i18n, but not required by task
                break;
            }
          }
        },
      })
    );
  }
}
