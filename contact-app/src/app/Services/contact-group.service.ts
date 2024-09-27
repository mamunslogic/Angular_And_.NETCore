import { HttpClient } from '@angular/common/http';
import { inject, Inject, Injectable } from '@angular/core';
import { ContactGroup } from '../Models/contact';

@Injectable({
  providedIn: 'root'
})
export class ContactGroupService {
  private apiURL = 'https://localhost:7033/api/ContactGroup/'

  constructor() { }

  http = inject(HttpClient);

  getAllContactGroups() {
    return this.http.get<ContactGroup[]>(this.apiURL);
  }

  addContactGroup(data: any) {
    return this.http.post(this.apiURL, data);
  }

  // updateContactGroup(contact: ContactGroup) {
  //   return this.http.put(`${this.apiURL}${contact.id}`, contact);
  // }

  // deleteContactGroup(contact: ContactGroup) {
  //   return this.http.delete(`${this.apiURL}${contact.id}`);
  // }
}
