import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { PeopleRoutingModule } from './people-routing.module';
import { ListPeopleComponent } from './list-people/list-people.component';
import { FormPeopleComponent } from './form-people/form-people.component';


@NgModule({
  declarations: [ListPeopleComponent, FormPeopleComponent],
  imports: [
    CommonModule,
    PeopleRoutingModule,
    ReactiveFormsModule
  ]
})
export class PeopleModule { }
