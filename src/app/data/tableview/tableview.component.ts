import { Component, OnInit } from '@angular/core';
import { UserAuthService } from '../../services/user.auth.service';

@Component({
  selector: 'app-tableview',
  templateUrl: './tableview.component.html',
  styleUrls: ['./tableview.component.scss']
})
export class TableviewComponent implements OnInit {

  constructor(private uservice:UserAuthService) { }

  ngOnInit() {
  }



  getData(){
    this.uservice.getOrders().subscribe(x=>{
     debugger;
    });

  }

}
 