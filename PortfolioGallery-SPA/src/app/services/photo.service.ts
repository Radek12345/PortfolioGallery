import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';
import { UserInfo } from '../common/user-info';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getPhotos() {
    return this.http.get(this.baseUrl + 'photos/' + UserInfo.getLoggedUserId());
  }
}
