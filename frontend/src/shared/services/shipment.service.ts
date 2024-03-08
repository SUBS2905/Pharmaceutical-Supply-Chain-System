import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import Shipment, { AddShipment } from '../models/Shipment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ShipmentService {
  private shipmentEndpoint = 'https://localhost:7024/api/Shipment';
  private jwt = this.cookieService.get('jwt');
  private headers = new HttpHeaders().set(
    'Authorization',
    `Bearer ${this.jwt}`
  );

  constructor(
    private http: HttpClient,
    private cookieService: CookieService,
  ) {}

  getAllShipments(): Observable<Shipment[]> {
    return this.http.get<Shipment[]>(this.shipmentEndpoint, {
      headers: this.headers,
    });
  }

  getShipmentByTrackingNo(trackingNo: string): Observable<Shipment> {
    const body = {
      trackingNo,
    };
    return this.http.post<Shipment>(
      `${this.shipmentEndpoint}/byTrackingNo`,
      body,
      { headers: this.headers }
    );
  }

  addShipment(shipmentReq: AddShipment): Observable<Shipment> {
    return this.http.post<Shipment>(this.shipmentEndpoint, shipmentReq, {headers: this.headers});
  }
}
