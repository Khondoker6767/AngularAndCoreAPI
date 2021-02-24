import { Component, OnInit } from '@angular/core';
import { from } from 'rxjs';
import {NgForm} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { PaymentDetail } from 'app/service/payment-detail.model';
import { PaymentDetailService } from 'app/service/payment-detail.service';
@Component({
  selector: 'app-payment-detail-form',
  templateUrl: './payment-detail-form.component.html',
  styleUrls: ['./payment-detail-form.component.css'
  ]
})
export class PaymentDetailFormComponent implements OnInit 
{
  constructor(public service:PaymentDetailService, private toastr:ToastrService) 
  {

  }
  ngOnInit(): void 
  {
    
  }
  onSubmit(form:NgForm)
  {
    if(this.service.formData.paymentDetailId==0)
    {
      this.InsertRecord(form);
    }
    else
    {
      this.UpdateRecord(form);
    }
  } 
  InsertRecord(form: NgForm) 
  {
    this.service.postPaymentDetail().subscribe(
      res=>{
        this.ResetForm(form);
        this.service.RefreshList();
        this.toastr.info("Submit Successfull", "Payment detail Register");
      },
      err=>{
        console.log(err);
      }
    )
  }
  UpdateRecord(form: NgForm) 
  {
    this.service.putPaymentDetail().subscribe(
      res=>{
        this.ResetForm(form);
        this.service.RefreshList();
      },
      err=>{
        console.log(err);
      }
    )
  }
  ResetForm(form:NgForm) 
  {
    form.form.reset();
    this.service.formData= new PaymentDetail();
  }
 
}
