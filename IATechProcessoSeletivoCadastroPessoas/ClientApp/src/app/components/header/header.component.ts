import { Component } from '@angular/core';
import { FormGroup, FormControl, FormArray, FormBuilder } from '@angular/forms'
import { PersonApiService } from 'src/app/services/person-api.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: [ './header.component.scss' ]
})
export class HeaderComponent  {
  name = '';
  cpf = '';
  birth = '';
  phoneForm: FormGroup;

  constructor(private fb:FormBuilder, private personService: PersonApiService) {
    const { birth, cpf, name } = this;
    this.phoneForm = this.fb.group({
      name,
      cpf,
      birth,
      phones: this.fb.array([]),
    });
  }

  phones() : FormArray {
    return this.phoneForm.get("phones") as FormArray
  }

  newPhone(): FormGroup {
    return this.fb.group({
      number: ''
    })
  }

  addPhone() {
    this.phones().push(this.newPhone());
  }

  removePhone(i:number) {
    this.phones().removeAt(i);
  }

  createPerson() {
    const {birth, name, cpf, phones} = this.phoneForm.value;
    const person = {
      birth: new Date(birth),
      name,
      cpf: Number(cpf),
      phones
    }

    this.personService.createPerson(person).subscribe();
  }
}
