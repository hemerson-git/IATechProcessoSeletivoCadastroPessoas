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

registerLocaleData(localePT);

@NgModule({
  declarations: [
    AppComponent,
    PersonListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [PersonApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }
