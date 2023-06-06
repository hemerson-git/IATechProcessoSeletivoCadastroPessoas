import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface IPhone {
  id: string;
  number: number;
}

export interface IPerson {
  id: string;
  name: string;
  cpf: number;
  birth: Date;
  phones: IPhone[];
}

@Injectable({
  providedIn: 'root'
})
export class PersonApiService {
  readonly personApiUrl = "https://localhost:7084/api"

  constructor(private http:HttpClient) {

  }

  getPersonList(): Observable<IPerson[]> {
    const people = this.http.get<IPerson[]>(`${this.personApiUrl}/Person`);
    console.log(people)
    return people;
  }

  getPersonById(id: string): Observable<IPerson> {
    const person = this.http.get<IPerson>(`${this.personApiUrl}/Person/${id}`);
    return person;
  }

  createPerson(data: any) {
    const person = this.http.post(`${this.personApiUrl}/Person`, data);
    return person;
  }
}
