import { RouterModule } from '@angular/router';
import { TableviewComponent } from './tableview/tableview.component';

export const  dataRouting = RouterModule.forChild([
    { path: 'tableview', component: TableviewComponent}
  ]);
