import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ContactService } from '../services/ContactService';
import { Contact } from '../interfaces/Contact';
import { MatSelectChange } from '@angular/material/select';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.css'],
})
export class ContactsComponent implements OnInit, OnDestroy {
  contacts: Contact[] = [];
  displayedColumns: string[] = ['name', 'DateOfBirth', 'phone', 'IsMarried', 'Salary', 'id'];
  private ngUnsubscribe = new Subject();
  file: File | null= null;
  errorMessage:string|undefined=undefined;
  items: any[] = [
    {value: 'name'},
    {value: 'salary'},
  ];
  toggle = false;
  onSelectionChange(event: MatSelectChange): void {
      const selectedValue = event.value.value;
      console.log(selectedValue); // Check the selected value in the console
      switch (selectedValue) {
        case 'name':
          this.sortByName();
          break;
        case 'salary':
          this.sortBySalary();
          break;
      }
    }
    sortBySalary(): void {
      this.contacts = [...this.contacts.sort((a, b) => a.salary - b.salary)];
      console.log(this.contacts);
    }

    edit(contact:Contact){
       contact.edit = true;
    }
    
    apply(contact:Contact){
      contact.edit = false;
    }

    cancel(contact:Contact){
      contact.edit = false;
    }
  sortByName(){
    this.contacts = [...this.contacts.sort((a, b) => a.name.localeCompare(b.name))];
    console.log(this.contacts);
  }
  constructor(private contactService: ContactService, ) {}

  private loadContacts(): void {
    this.contactService
      .getContacts()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: any) => {
          this.contacts = response;
        },
        error: (error) => {
          console.error('Error fetching data:', error);
        },
      });
  }
  upload(){
    if(this.file==null){
        this.errorMessage = "file is not uploaded";
    }else{
      console.log(this.file);
      this.contactService
      .UploadCsvFile(this.file)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: () => {
          this.loadContacts();
          window.alert(`Success: ${"Success"}`);
          this.clearFileInput();
        },
        error: (error) => {
          this.errorMessage = error.error.Details;
        },
      });
    }
   
  }
  clearFileInput() {
    // Reset the file input value to an empty string
    const fileInput = document.querySelector('.file-upload') as HTMLInputElement;
    if (fileInput) {
      fileInput.value = '';
    }
  }
    async onFileSelected(event:any){
      this.file = event.target.files[0]; 
 
    }
    
 
  deleteContact(id: number): void {
    this.contactService
      .deleteContact(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: any) => {
          this.loadContacts();
        },
        error: (error) => {
          console.error('Error deleting contact:', error);
        },
      });
  }

  ngOnInit() {
    this.loadContacts();
    console.log(this.items);
  }

  ngOnDestroy() {
    this.ngUnsubscribe.unsubscribe();
  }
}
