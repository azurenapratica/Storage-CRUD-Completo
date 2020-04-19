import { Component, OnInit, ViewChild } from '@angular/core';
import { PeopleService } from '../people.service';
import { Person } from 'src/app/person';
import { Observable, empty, Subject } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AlertModalService } from 'src/app/shared/alert-modal.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-list-people',
  templateUrl: './list-people.component.html',
  styleUrls: ['./list-people.component.css']
})
export class ListPeopleComponent implements OnInit {

  deleteModalRef: BsModalRef;
  @ViewChild('deleteModal', {static: false}) deleteModal;

  people$: Observable<Person[]>;
  error$ = new Subject<boolean>();
  selectedPerson: Person;

  constructor(private service: PeopleService,
              private alertService: AlertModalService,
              private router: Router,
              private route: ActivatedRoute,
              private modalService: BsModalService) { }

  ngOnInit() {
    this.onRefresh();
  }

  handleError() {
    this.alertService.showAlertDanger("An error has occurred on List People");
  }

  onRefresh() {
    this.people$ = this.service.list()
      .pipe(
        catchError(error => {
          this.handleError();
          return empty();
        })
      );
  }

  onEdit(id) {
    this.router.navigate(['edit-person', id], {relativeTo: this.route});
  }

  onDelete(id) {
    this.selectedPerson = id;
    this.deleteModalRef = this.modalService.show(this.deleteModal, {class: 'modal-sm'});
  }

  onConfirmDelete() {
    this.service.remove(this.selectedPerson).subscribe(
      success => { 
        this.deleteModalRef.hide();
        this.alertService.showAlertSuccess('The person has been removed.');
        setTimeout(()=> { this.onRefresh();; }, 3000); 
      },
      error => {
        this.alertService.showAlertDanger('An error has occured when you try to remove a person.');
        this.deleteModalRef.hide();
      }
    );
  }

  onDeclineDelete() {
    this.deleteModalRef.hide();
  }
}
