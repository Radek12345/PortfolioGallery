import { AuthService } from './../../services/auth.service';
import { Component, OnInit } from '@angular/core';

import { LoginResource } from 'src/app/models/loginResource';
import { P } from '@angular/core/src/render3';
import { Router } from '@angular/router';
import { Alertify } from 'src/app/common/alertify';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  resource: LoginResource;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.resource = {
      login: '',
      password: ''
    };
  }

  loginUser() {
    this.authService.login(this.resource).subscribe((response: any) => {
      Alertify.success('Logged in successful');
      this.router.navigate(['/gallery']);
    }, errorResponse => {
      console.log(errorResponse);
      if (errorResponse.status === 401) {
        Alertify.error('Unauthorized. Please check your login or/and password');
      } else {
        Alertify.error('Unknown error. Please try again or contact with administrator');
      }
    });
  }

}
