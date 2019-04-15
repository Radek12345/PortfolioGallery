import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';

import { AuthService } from './../../services/auth.service';
import { User } from '../models/user';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  user: User;
  repeatedPassword: String;
  @ViewChild('registerButton') registerButton: ElementRef;

  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.user = {
      name: '',
      email: '',
      password: ''
    };
  }

  register() {
    if (this.user.password !== this.repeatedPassword) {
      console.log('Repeated password is diffrent');
      return;
    }

    this.registerButton.nativeElement.setAttribute('disabled', 'true');

    this.authService.register(this.user).subscribe(() => {
      console.log('Register successful');
    }, error => {
      console.log(error);
      this.registerButton.nativeElement.removeAttribute('disabled');
    });
  }

}
