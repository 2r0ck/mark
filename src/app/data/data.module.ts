import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableviewComponent } from './tableview/tableview.component';
import { dataRouting } from './data.routing';

@NgModule({
  imports: [
    CommonModule,
    dataRouting  
  ],
  declarations: [TableviewComponent]
})
export class DataModule { }
