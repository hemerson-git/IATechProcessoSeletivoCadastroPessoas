import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { IPerson, PersonApiService } from 'src/app/services/person-api.service';

@Component({
  selector: 'app-person-list',
  templateUrl: './person-list.component.html',
  styleUrls: ['./person-list.component.scss']
})
export class PersonListComponent {
  personList$!: Observable<IPerson[]>;

  constructor(private service: PersonApiService) {

  }

  ngOnInit(): void {
    this.personList$ = this.service.getPersonList();
  }
}
