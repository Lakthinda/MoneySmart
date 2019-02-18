import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { IAccount } from './account';

@Injectable(
  {
    providedIn: 'root'
  }
)
export class AccountService {

  private accountUrl = 'http://localhost:63703/api/savings';

  constructor(private http: HttpClient) {}

  getAccounts(): Observable<IAccount[]> {
     return this.http.get<IAccount[]>(this.accountUrl).pipe(
        tap(data => console.log('All: ' + JSON.stringify(data)))
     );
  }
}
