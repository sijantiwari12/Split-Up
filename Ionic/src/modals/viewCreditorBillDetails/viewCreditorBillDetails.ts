import {Component} from '@angular/core';
import {AlertController, NavParams, ToastController, ViewController} from 'ionic-angular';
import {CreditService} from "../../services/credit.service";
import {UserService} from "../../services/user.service";

@Component({
  templateUrl: 'viewCreditorBillDetails.html',
})

export class ViewCreditorBillDetails {
  constructor(public viewCtrl: ViewController,
              public navParam: NavParams,
              public _creditService: CreditService,
              public _userService : UserService,
              public alertCtrl: AlertController,
              public toastCtrl: ToastController) {
    this.getBillDetail(this.navParam.get('billId'));
  }

  billName: any;
  billDetails: any;
  amountPaid: any;
  memo: any;
  purchaseDate: any;
  noOfIndividuals: any;
  totalAmountRemaining: any;
  creditors: any;
  billOwnerName: any = "";

  loggedInUserId = localStorage.getItem('userId');

  dismiss() {
    this.viewCtrl.dismiss();
  }

  getBillDetail(billId) {
    this._creditService.GetCreditorsByTransactionId(billId).subscribe(response => {
      this.billDetails = response[0].transaction;
      this.billName = this.billDetails.billName;
      this.amountPaid = this.billDetails.amountPaid;
      this.memo = this.billDetails.memo;
      this.purchaseDate = new Date(this.billDetails.purchaseDate);
      this.purchaseDate = (this.purchaseDate.getMonth()+1) +
        '/'+this.purchaseDate.getDate() + '/' + this.purchaseDate.getFullYear();
      this.noOfIndividuals = this.billDetails.noOfIndividuals;
      this.totalAmountRemaining = 0;
      this.GetBillOwnerName(this.billDetails.userId);
      response.map(u => {
        if(!u.status && u.creditorId != localStorage.getItem('userId')) {
          this.totalAmountRemaining += u.amountToPay;
        }
      });
      this.creditors = response;
    })
  }

  pingTransaction(transactionId, creditorId) {
    let alert = this.alertCtrl.create({
      title: "Paid Bill?",
      subTitle: "Are you sure you want to ping this user?",
      buttons: [
        {
          text: 'No',
        },
        {
          text: 'Yes',
          handler: () => {
            this.proceedPing(transactionId, creditorId);
          }
        }
      ]
    });
    alert.present();
  }

  proceedPing(transactionId, creditorId) {
    this._creditService.pingTransaction(transactionId, creditorId).subscribe(response => {
      if(response.status == "Success") {
        this.getBillDetail(transactionId);

        let toast = this.toastCtrl.create({
          message: response.message,
          duration: 3000,
          position: 'bottom',
        });

        toast.present();
      }
    })
  }

  GetBillOwnerName = (userId) => {
    this._userService.getNameById(userId).subscribe(response => {
      this.billOwnerName = response['name'];
    });
  }
}
