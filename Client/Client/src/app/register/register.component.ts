import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model:any={};
  
  @Output() cancelRegister:EventEmitter<boolean>=new EventEmitter();
  constructor(private accountService:AccountService,private toaster:ToastrService) { }

  ngOnInit(): void {
  }
  register(){
    this.accountService.register(this.model).subscribe(
      Response=>{
      console.log(Response);
      this.cancel();
      },error=>{
        console.log(error)
        this.toaster.error(error.error);
      }
      
    );
    
  }
  cancel(){
    this.cancelRegister.emit(false);
    console.log("canceled");
  }
}
