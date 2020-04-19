import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AlertModalComponent } from './alert-modal/alert-modal.component';



@NgModule({
  declarations: [AlertModalComponent],
  exports: [AlertModalComponent],
  entryComponents: [AlertModalComponent],
  imports: [
    CommonModule
  ]
})
export class SharedModule { }
