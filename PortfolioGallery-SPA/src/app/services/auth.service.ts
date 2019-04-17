import { LoginResource } from 'src/app/models/loginResource';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from 'src/environments/environment';
import { RegisterResource } from '../models/registerResource';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'auth/';

  constructor(private http: HttpClient) { }

  register(resource: RegisterResource) {
    return this.http.post(this.baseUrl + 'register', resource);
  }

  login(resource: LoginResource) {
    return this.http.post(this.baseUrl + 'login', resource);
  }
}
