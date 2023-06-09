import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { IPerson, PersonApiService } from 'src/app/services/person-api.service';

@Component({
  selector: 'app-person-list',
  templateUrl: './person-list.component.html',
  styleUrls: ['./person-list.component.scss']
})
export class PersonListComponent {
  personList$!: IPerson[];
  selectedPerson!: IPerson;
  editPersonForm!: FormGroup;
  searchQuery: string = '';

  constructor(private service: PersonApiService, private fb:FormBuilder) {
    this.editPersonForm = this.fb.group({
      editName: this?.selectedPerson?.name ?? 'test',
      editCpf: '',
      editBirth: "",
      phones: this.fb.array([]),
    });
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

  phones() : FormArray {
    return this.editPersonForm.get("phones") as FormArray
  }

  setSelectedPerson(person: IPerson) {
    const phones =  person.phones?.map(phone => this.fb.group({
      number: phone.number,
      id: phone.id
    })) ?? [];

    this.editPersonForm = this.fb.group({
      id: person.id,
      editName: person.name,
      editCpf: person.cpf,
      editBirth: new Date(person.birth).toISOString().slice(0, 10),
      phones: this.fb.array(phones),
    })
  }

  onChange(name: string) {
    if(name.length > 2) {
      console.log(name)
      this.service.getPersonByName(name).subscribe((person: IPerson[]) => {
        console.log(person)
        this.personList$ = person;
      });

      return;
    }

    this.service.getPersonList().subscribe((person: IPerson[]) => {
      return this.personList$ = person;
    });
  }
}
