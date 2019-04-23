import { PhotoService } from './../../services/photo.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.css']
})
export class GalleryComponent implements OnInit {
  photos: object;

  constructor(private photoService: PhotoService) { }

  ngOnInit() {
    this.photoService.getPhotos().subscribe(response => {
      this.photos = response;
    });
  }

}
