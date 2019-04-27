import { PhotoService } from './../../services/photo.service';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-photo-form',
  templateUrl: './photo-form.component.html',
  styleUrls: ['./photo-form.component.css']
})
export class PhotoFormComponent implements OnInit {
  photo: File;
  @ViewChild('uploadLabel') uploadLabel: ElementRef;

  photoName: string;

  constructor(private photoService: PhotoService) { }

  ngOnInit() {
  }

  handlePhoto(files: FileList) {
    this.photo = files.item(0);
    const uploadLabel = this.uploadLabel.nativeElement;
    uploadLabel.classList.add('selected');
    uploadLabel.textContent = this.photo.name;
    this.photoName = this.photo.name;
  }

  uploadPhoto() {
    this.photoService.uploadPhoto(this.photo).subscribe(response => {
      console.log(response);
      console.log('uploaded success!');
    });
  }

}
