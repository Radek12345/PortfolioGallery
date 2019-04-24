import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Alertify } from 'src/app/common/alertify';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');

    Alertify.success('Logged out successfully');
    this.router.navigate(['/home']);
  }
}
