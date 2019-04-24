import { AuthService } from './../../services/auth.service';
import { PhotoService } from './../../services/photo.service';
import { Component, OnInit } from '@angular/core';
import { Photo } from 'src/app/models/photo';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.css']
})
export class GalleryComponent implements OnInit {
  photos: Photo[];
  loggedUserId: number;

  constructor(private photoService: PhotoService, private authService: AuthService) { }

  ngOnInit() {
    this.photoService.getPhotos().subscribe(response => {
      this.photos = response;
    });

    this.loggedUserId = this.authService.getLoggedUserId();
  }

  isLoggedUserPhoto(userId: number): boolean {
    // tslint:disable-next-line:triple-equals
    return (userId == this.loggedUserId);
  }

  deletePhoto(id: number) {
    this.photoService.deletePhoto(id).subscribe(response => {
      const photo = this.photos.find(p => p.id === id);
      this.photos.splice(this.photos.indexOf(photo), 1);
    });
  }

}
