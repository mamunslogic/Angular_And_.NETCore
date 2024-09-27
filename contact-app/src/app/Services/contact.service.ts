import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Contact } from '../Models/contact';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  private apiURL = 'https://localhost:7033/api/Contact/'

  constructor() { }

  http = inject(HttpClient)

  getAllContacts() {
    return this.http.get<Contact[]>(this.apiURL);
  }

  addContact(data: any) {
    return this.http.post(this.apiURL, data);
  }

  updateContact(contact: Contact) {
    return this.http.put(`${this.apiURL}${contact.id}`, contact);
  }

  deleteContact(contact: Contact) {
    return this.http.delete(`${this.apiURL}${contact.id}`);
  }
}
