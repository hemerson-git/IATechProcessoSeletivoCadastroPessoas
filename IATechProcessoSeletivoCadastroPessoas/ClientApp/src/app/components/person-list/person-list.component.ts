import { Component } from '@angular/core';
import { IPerson, PersonApiService } from 'src/app/services/person-api.service';

@Component({
  selector: 'app-person-list',
  templateUrl: './person-list.component.html',
  styleUrls: ['./person-list.component.scss']
})
export class PersonListComponent {
  personList$!: IPerson[];

  constructor(private service: PersonApiService) {

  }

  ngOnInit(): void {
    this.service.getPersonList().subscribe(person => this.personList$ = person);
    // this.deletePerson$ = (id: string) => this.service.deletePerson(id);
  }

  deletePerson(personId: string) {
    const hasDeleted = this.service.deletePerson(personId).subscribe();
    this.personList$ = this.personList$.filter(person => person.id !== personId)
    console.log(hasDeleted);
  }
}
