import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListPeopleComponent } from './list-people/list-people.component';
import { FormPeopleComponent } from './form-people/form-people.component';
import { PeopleResolverGuard } from './guards/people-resolver.guard';

const routes: Routes = [
  { path: '', component: ListPeopleComponent },
  {
    path: 'new-person', component: FormPeopleComponent,
    resolve: {
      person: PeopleResolverGuard
    }
  },
  {
    path: 'edit-person/:id', component: FormPeopleComponent,
    resolve: {
      person: PeopleResolverGuard
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PeopleRoutingModule { }
