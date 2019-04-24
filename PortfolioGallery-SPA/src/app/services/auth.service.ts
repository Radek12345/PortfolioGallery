import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';

import { environment } from 'src/environments/environment';
import { RegisterResource } from '../models/register-resource';
import { LoginResource } from 'src/app/models/login-resource';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'auth/';
  jwtHelper = new JwtHelperService();

  constructor(private http: HttpClient) { }

  register(resource: RegisterResource) {
    return this.http.post(this.baseUrl + 'register', resource);
  }

  login(resource: LoginResource) {
    return this.http.post(this.baseUrl + 'login', resource);
  }

  getLoggedUserId() {
    return this.jwtHelper.decodeToken(localStorage.getItem('token')).nameid;
  }

  isAuthenticated(): boolean {
    return !this.jwtHelper.isTokenExpired(localStorage.getItem('token'));
  }
}
