import { Routes } from '@angular/router';

import { GalleryComponent } from './components/gallery/gallery.component';
import { HomeComponent } from './components/home/home.component';

export const routes: Routes = [
    { path: '', component: HomeComponent, pathMatch: 'full' },
    { path: 'gallery', component: GalleryComponent },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];
