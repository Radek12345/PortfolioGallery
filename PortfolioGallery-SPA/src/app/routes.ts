import { Routes } from '@angular/router';

import { GalleryComponent } from './components/gallery/gallery.component';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { PhotoFormComponent } from './components/photo-form/photo-form.component';

export const routes: Routes = [
    { path: '', component: HomeComponent, pathMatch: 'full' },
    { path: 'register', component: RegisterComponent },
    { path: 'login', component: LoginComponent },
    { path: 'gallery', component: GalleryComponent },
    { path: 'photo-form', component: PhotoFormComponent },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];
