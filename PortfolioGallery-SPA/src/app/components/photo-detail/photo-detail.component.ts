import { PhotoService } from './../../services/photo.service';
import { Photo } from 'src/app/models/photo';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-photo-detail',
  templateUrl: './photo-detail.component.html',
  styleUrls: ['./photo-detail.component.css']
})
export class PhotoDetailComponent implements OnInit {
  photo: Photo;

  constructor(private activatedRoute: ActivatedRoute, private photoService: PhotoService) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(p => {
      this.photoService.getPhoto(+p['id']).subscribe(photo => {
        this.photo = photo;
      });
    });
  }

}
