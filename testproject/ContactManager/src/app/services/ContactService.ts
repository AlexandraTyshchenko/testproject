import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { API_BASE_URL } from "../../config";
import { Contact } from "../interfaces/Contact";

@Injectable({
  providedIn: 'root',
}) 
export class ContactService {
  
  constructor(private http: HttpClient) {}

  getContacts(): Observable<Contact> {
    return this.http.get<Contact>(`${API_BASE_URL}/api/Person`);
  }
  
  deleteContact(id:number){
    return this.http.delete(`${API_BASE_URL}/api/Person?id=${id}`);
  }

  UploadCsvFile(file:File){

    const formData = new FormData();
    formData.append('files', file);
    return this.http.post(`${API_BASE_URL}/api/Person/UploadCSV`, formData);
}
}