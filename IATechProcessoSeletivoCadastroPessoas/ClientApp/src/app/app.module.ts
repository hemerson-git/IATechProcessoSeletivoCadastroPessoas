import { HttpClientModule } from '@angular/common/http';
import { registerLocaleData } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PersonApiService } from './services/person-api.service';
import { PersonListComponent } from './components/person-list/person-list.component';
import localePT from '@angular/common/locales/pt';
import { HeaderComponent } from './components/header/header.component';
import { EditPersonComponent } from './components/edit-person/edit-person.component';
import { ModalComponent } from './components/modal/modal.component';
import { CreatePersonComponent } from './components/create-person/create-person.component';
import { InputMaskModule } from '@ngneat/input-mask';

registerLocaleData(localePT);

@NgModule({
  declarations: [
    AppComponent,
    PersonListComponent,
    HeaderComponent,
    EditPersonComponent,
    ModalComponent,
    CreatePersonComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    InputMaskModule
  ],
  providers: [PersonApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }
