import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Observable } from 'rxjs';
import Recall from '../models/Recall';

@Injectable({
  providedIn: 'root',
})
export class RecallService {
  private recallEndpoint = 'https://localhost:7024/api/Recall';
  private jwt = this.cookieService.get('jwt');
  private headers = new HttpHeaders().set(
    'Authorization',
    `Bearer ${this.jwt}`
  );

  constructor(private http: HttpClient, private cookieService: CookieService) {}

  getAllRecalls(): Observable<Recall[]> {
    return this.http.get<Recall[]>(this.recallEndpoint, {
      headers: this.headers,
    });
  }

  recallProduct(productId: string, reason: string): Observable<Recall> {
    const body = {
      productId,
      reason,
    };
    return this.http.post<Recall>(this.recallEndpoint, body, {
      headers: this.headers,
    });
  }
}
