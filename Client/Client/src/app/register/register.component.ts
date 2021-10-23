import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model:any={};
  
  @Output() cancelRegister:EventEmitter<boolean>=new EventEmitter();
  constructor(private accountService:AccountService) { }

  ngOnInit(): void {
  }
  register(){
    this.accountService.register(this.model).subscribe(
      Response=>{
      console.log(Response);
      this.cancel();
      },error=>console.log(error)
      
    );
    
  }
  cancel(){
    this.cancelRegister.emit(false);
    console.log("canceled");
    
  }
}
