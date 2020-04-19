import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';
import { Person } from 'src/app/person';
import { PeopleService } from '../people.service';

@Injectable({
  providedIn: 'root'
})
export class PeopleResolverGuard implements Resolve<Person> {

  constructor(private service: PeopleService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Person | Observable<Person> | Promise<Person> {
    if (route.params && route.params['id']) {
      return this.service.getById(route.params['id']);
    }

    return of({
      rowKey: null,
      partitionKey: null,
      name: null,
      email: null
    });

  }

}
