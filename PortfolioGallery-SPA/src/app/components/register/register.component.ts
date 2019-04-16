import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from './../../services/auth.service';
import { User } from '../../models/user';
import { Alertify } from './../../common/alertify';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  user: User;
  repeatedPassword: String;
  @ViewChild('registerButton') registerButton: ElementRef;
  repeatedDifferent = false;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.user = {
      name: '',
      email: '',
      password: ''
    };
  }

  register() {
    if (this.user.password !== this.repeatedPassword) {
      Alertify.error('Repeated password is different');
      return;
    }

    this.registerButton.nativeElement.setAttribute('disabled', 'true');

    this.authService.register(this.user).subscribe(() => {
      Alertify.success('Registration successful');
      this.router.navigate(['/gallery'])
    }, errorResponse => {
      Alertify.error(errorResponse.error);
      this.registerButton.nativeElement.removeAttribute('disabled');
    });
  }

}
