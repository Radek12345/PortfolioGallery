import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';
import { Photo } from '../models/photo';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {
  baseUrl = environment.apiUrl + 'photos/' + this.authService.getLoggedUserId();

  constructor(private http: HttpClient, private authService: AuthService) { }

  getPhotos() {
    return this.http.get<Photo[]>(this.baseUrl);
  }

  getPhoto(photoId: number) {
    return this.http.get<Photo>(this.baseUrl + '/' + photoId);
  }

  deletePhoto(photoId: number) {
    return this.http.delete(this.baseUrl + '/' + photoId);
  }

  uploadPhoto(photo: File) {
    const formData = new FormData();
    formData.append('photoFile', photo);
    return this.http.post(this.baseUrl, formData);
  }

  updatePhotoInfo(photoId: number, photo: Partial<Photo>) {
    return this.http.put(this.baseUrl + '/' + photoId, photo);
  }
}
