import { AuthService } from './../../services/auth.service';
import { PhotoService } from './../../services/photo.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Photo } from 'src/app/models/photo';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.css']
})
export class GalleryComponent implements OnInit {
  photos: Photo[];
  loggedUserId: number;
  modalRef: BsModalRef;

  nameForFilter: string;

  private photoIdForDeletion: number;

  constructor(private photoService: PhotoService, private authService: AuthService,
    private modalService: BsModalService) { }

  ngOnInit() {
    this.initPhotos();
    this.loggedUserId = this.authService.getLoggedUserId();
  }

  isLoggedUserPhoto(userId: number): boolean {
    // tslint:disable-next-line:triple-equals
    return (userId == this.loggedUserId);
  }

  deletePhoto() {
    this.photoService.deletePhoto(this.photoIdForDeletion).subscribe(response => {
      const photo = this.photos.find(p => p.id === this.photoIdForDeletion);
      this.photos.splice(this.photos.indexOf(photo), 1);
      this.modalRef.hide();
    });
  }

  filterPhotos() {
    this.initPhotos(this.nameForFilter);
  }

  initPhotos(nameForFilter?: string) {
    this.photoService.getPhotos(nameForFilter).subscribe(response => {
      this.photos = response;
    });
  }

  openModal(template: TemplateRef<any>, photoId: number) {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
    this.photoIdForDeletion = photoId;
  }

  close() {
    this.modalRef.hide();
  }

}
