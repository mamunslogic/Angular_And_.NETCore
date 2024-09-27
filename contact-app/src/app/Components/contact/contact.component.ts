import { Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators, FormsModule } from '@angular/forms';
import { Contact, ContactGroup } from '../../Models/contact';
import { ContactService } from '../../Services/contact.service';
import { ContactGroupService } from '../../Services/contact-group.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, FormsModule],
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.css'
})
export class ContactComponent implements OnInit {
  @ViewChild('myModal') modal: ElementRef | undefined;
  @ViewChild('contactGroupModal') modalContactGroup: ElementRef | undefined;

  contactGroupList: ContactGroup[] = [];
  contactList: Contact[] = [];
  contactService = inject(ContactService);
  contactGroupService = inject(ContactGroupService)
  contactForm: FormGroup = new FormGroup({});
  contactGroupForm : FormGroup = new FormGroup({});

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.setFormState();
    this.setFormStateContactGroup();
    this.getContactGroups();
    this.getContacts();
  }

  openModal() {
    const contactModal = document.getElementById('myModal');
    if (contactModal != null) {
      contactModal.style.display = 'block';
    }
  }

  openContactGroupModal() {
    const contactGroupModal = document.getElementById('contactGroupModal');
    if (contactGroupModal != null) {
      contactGroupModal.style.display = 'block';
    }
  }

  closeModal() {
    this.setFormState();
    if (this.modal != null) {
      this.modal.nativeElement.style.display = 'none';
    }
  }
  
  closeContactGroupModal() {
    this.setFormStateContactGroup();
    if (this.modalContactGroup != null) {
      this.modalContactGroup.nativeElement.style.display = 'none';
    }
  }

  getContactGroups() {
    this.contactGroupService.getAllContactGroups().subscribe((res) => {
      this.contactGroupList = res;
    },
      (error) => {
        console.error('Error fetching contact groups', error);
      })
  }

  getContacts() {
    this.contactService.getAllContacts().subscribe((res) => {
      this.contactList = res;
    })
  }

  setFormState() {
    this.contactForm = this.fb.group({
      id: [0],
      name: ['', [Validators.required]],
      phoneNumber: ['', [Validators.required]],
      contactType: ['', [Validators.required]],
      contactGroupId: ['', [Validators.required]],
    });
  }

  setFormStateContactGroup() {
    this.contactGroupForm = this.fb.group({
      id: [0],
      contactGroupName: ['', [Validators.required]]
    });
  }

  formValues: any;

  onSubmit() {
    console.log(this.contactForm.value);

    if (this.contactForm.invalid) {
      alert('Please fill all information.');
      return;
    }

    const contact: Contact = {
      name: this.contactForm.value.name,
      phoneNumber: this.contactForm.value.phoneNumber,
      contactType: this.contactForm.value.contactType,
      contactGroupId: this.contactForm.value.contactGroupId,
      contactGroupName: '',
      isEdit: false
    };
    console.log(contact);

    if (this.contactForm.value.id == 0) {
      this.contactService.addContact(contact).subscribe(
        (res) => {
          this.onSaveSuccess(res);
        },
        (error) => {
          alert(error.error.message);
        },
        () => {

        }
      );
    }
    else {
      contact.id = this.contactForm.value.id;
      this.contactService.updateContact(contact).subscribe(
        (res) => {
          this.onSaveSuccess(res);
        },
        (error) => {
          alert(error.error.message);
        },
        () => {

        }
      );
    }
  }

  onContactGroupSubmit() {
    console.log(this.contactGroupForm.value);

    if (this.contactGroupForm.invalid) {
      alert('Please fill all information.');
      return;
    }

    const contactGroup: ContactGroup = {
      contactGroupName: this.contactGroupForm.value.contactGroupName
    };
    console.log(contactGroup);

    if (this.contactGroupForm.value.id == 0) {
      this.contactGroupService.addContactGroup(contactGroup).subscribe(
        (res) => {
          this.onSaveSuccessContactGroup(res);
        },
        (error) => {
          alert(error.error.message);
        }
      );
    }
  }

  onSaveSuccess(res: any) {
    alert('Save successful.');
    this.getContacts();
    this.contactForm.reset();
    this.closeModal();
  }

  onSaveSuccessContactGroup(res: any) {
    alert('Save successful. You can get it on add or modification of contact');
    this.getContacts();
    this.getContactGroups();
    this.contactGroupForm.reset();
    this.closeContactGroupModal();
  }

  onEdit(contact: Contact) {
    this.openModal();
    this.contactForm.patchValue({
      id: contact.id,
      name: contact.name,
      phoneNumber: contact.phoneNumber,
      contactType: contact.contactType,
      contactGroupId: contact.contactGroupId,
      // contactGroup: {
      //   //id: contact.contactGroupId,
      //   //contactGroupName: contact.contactGroup.contactGroupName
      // }
    });
  }

  onInlineEdit(contact: Contact) {
    this.contactList.forEach(element => {
      element.isEdit = false;
    });
    contact.isEdit = true;
  }

  onInlineSave(contact: Contact){
    this.contactService.updateContact(contact).subscribe(
      (res) => {
        this.onSaveSuccess(res);
      },
      (error) => {
        alert(error.error.message);
      }
    );
  }

  onDelete(contact: Contact) {
    const isConfirm = confirm("Are you sure you want to delete the contact " + contact.name + "?");
    if (isConfirm) {
      this.contactService.deleteContact(contact).subscribe((res) => {
        alert('Delete successful.');
        this.getContacts();
      });
    }
  }
}
