import { Router } from '@angular/router';
import { Alertify } from 'src/app/common/alertify';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';

import { Photo } from 'src/app/models/photo';
import { PhotoService } from './../../services/photo.service';

@Component({
  selector: 'app-photo-form',
  templateUrl: './photo-form.component.html',
  styleUrls: ['./photo-form.component.css']
})
export class PhotoFormComponent implements OnInit {
  photo: File;
  @ViewChild('uploadLabel') uploadLabel: ElementRef;

  photoInfo: Partial<Photo> = {};

  constructor(private photoService: PhotoService, private router: Router) { }

  ngOnInit() {
  }

  handlePhoto(files: FileList) {
    this.photo = files.item(0);
    const uploadLabel = this.uploadLabel.nativeElement;
    uploadLabel.classList.add('selected');
    uploadLabel.textContent = this.photo.name;
    this.photoInfo.name = this.photo.name;
  }

  uploadPhoto() {
    this.photoService.uploadPhoto(this.photo).subscribe((response: any) => {
      this.photoService.updatePhotoInfo(response.id, this.photoInfo).subscribe(() => {
        Alertify.success('Photo uploaded successfully');
        this.router.navigate(['/gallery']);
      });
    });
  }

}
