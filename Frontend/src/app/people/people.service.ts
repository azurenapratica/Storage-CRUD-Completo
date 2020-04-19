import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Person } from '../person';
import { environment } from '../../environments/environment'
import { take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PeopleService {

  private readonly _API = `${environment.API}Person`;

  constructor(private http: HttpClient) { }

  list() {
    return this.http.get<Person[]>(this._API);
  }

  getById(rowKey) {
    return this.http.get<Person>(`${this._API}/${rowKey}`)
      .pipe(take(1));
  }

  private create(person) {
    return this.http.post(this._API, person)
      .pipe(take(1));
  }

  private update(rowKey, person) {
    return this.http.put(`${this._API}/${rowKey}`, person)
      .pipe(take(1));
  }

  save(rowKey, person) {
    if (rowKey !== null)
      return this.update(rowKey, person);
    else
      return this.create(person);
  }

  remove(rowKey) {
    return this.http.delete(`${this._API}/${rowKey}`)
      .pipe(take(1));
  }
}
