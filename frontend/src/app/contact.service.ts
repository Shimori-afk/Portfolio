import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

export interface ContactPayload {
  name: string;
  email: string;
  message: string;
}

@Injectable({ providedIn: 'root' })
export class ContactService {
  private readonly endpoint = `${environment.apiUrl}/api/contact`;

  constructor(private http: HttpClient) {}

  send(payload: ContactPayload): Observable<unknown> {
    return this.http.post(this.endpoint, payload);
  }
}
