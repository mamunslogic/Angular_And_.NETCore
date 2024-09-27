import { Routes } from '@angular/router';
import { ContactComponent } from './Components/contact/contact.component';

export const routes: Routes = [
    {
        path: "", component: ContactComponent
    },
    {
        path: "contact", component: ContactComponent
    }
];
