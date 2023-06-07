import { HttpClient, HttpHeaders } from '@angular/common/http';
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
  phones?: IPhone[];
}

interface  INewPerson {
  name: string;
  cpf: number;
  birth: Date;
  phones?: {
    number: number;
  };
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

  createPerson(data: INewPerson) {
    const {birth, cpf, name} = data;
    const person = this.http.post<IPerson>(`${this.personApiUrl}/Person`, {
      birth: birth,
      cpf,
      name
    });

    return person;
  }

  updatePerson(data: IPerson, id: string) {
    const sendingData = {
      id,
      name: data.name,
      cpf: data.cpf,
      birth: data.birth,
      phones: data.phones,
    }

    const newPerson = this.http.put<IPerson>(`${this.personApiUrl}/Person/${id}`, sendingData);
    return newPerson;
  }

  deletePerson(id: string) {
    const hasDeleted = this.http.delete<boolean>(`${this.personApiUrl}/Person/${id}`);
    return hasDeleted;
  }
}
