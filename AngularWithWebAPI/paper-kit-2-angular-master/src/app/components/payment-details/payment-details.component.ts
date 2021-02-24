import { Component, OnInit } from '@angular/core';
import { PaymentDetail } from 'app/service/payment-detail.model';
import { PaymentDetailService } from 'app/service/payment-detail.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-payment-details',
  templateUrl: './payment-details.component.html',
  styleUrls: ['./payment-details.component.css']
})
export class PaymentDetailsComponent implements OnInit 
{
  constructor(public service:PaymentDetailService, private toastr:ToastrService) 
  {

  }

  ngOnInit(): void 
  {
    this.service.RefreshList();
  }
  populateForm(selectedRecord:PaymentDetail)
  {
    this.service.formData=Object.assign({}, selectedRecord);
  }
  onDelete(id:number)
  {
    if(confirm('Are you sure of deleting this record???'))
    {
      this.service.deletePaymentDetail(id).subscribe(
        res=>{
          this.service.RefreshList();
          this.toastr.error("Deleted Successfully", "Payment detail Register");
        },
        err=>{
          console.log(err);
        }
      );
    }
  }
}
