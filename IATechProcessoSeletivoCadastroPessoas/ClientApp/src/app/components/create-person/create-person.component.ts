import { Component, NgModule } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { InputMaskConfig, createMask } from '@ngneat/input-mask';
import { IPerson, IPhone, PersonApiService } from 'src/app/services/person-api.service';

@Component({
  selector: 'app-create-person',
  templateUrl: './create-person.component.html',
  styleUrls: ['./create-person.component.scss']
})
export class CreatePersonComponent {
  name = '';
  cpf = '';
  birth = '';
  phoneForm: FormGroup;
  cpfInputMask = createMask({
    alias: '999.999.999-99',
    inputFormat: '999.999.999-99',
    parser: (value: string) => {
      const parsedValue = value.replaceAll('.', '').replaceAll('-', '');
      return parsedValue;
    }
  })
  phoneMask = createMask({
    alias: '(99) 99999-9999',
    nullable: false,
    parser: (value: string) => {
      const parsedValue = value.replaceAll(' ', '').replaceAll('-', '').replace('(', '').replace(')', '');
      return Number(parsedValue);
    }
  })

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
    if (!name || !birth || !cpf) return;
    let isPhoneValid = true;

    phones.forEach((phone: IPhone) => {
      if(String(phone.number).length < 11) {
        isPhoneValid = false;
        return;
      };
    })

    if(!isPhoneValid) return;

    const person = {
      birth: new Date(birth),
      name,
      cpf: Number(cpf),
      phones
    }

    console.log(person)
    this.personService.createPerson(person).subscribe();
    location.reload();
  }
}
