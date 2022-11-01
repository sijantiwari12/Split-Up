import {Component, OnInit} from '@angular/core';
import {AlertController, NavParams, ToastController, ViewController} from 'ionic-angular';
import { ModalController} from "ionic-angular";
import {TransactionService} from "../../services/transaction.service";
import {CreditService} from "../../services/credit.service";

@Component({
  templateUrl: 'viewBillDetailsModal.html',
})

export class ViewBillDetailsModal {
  constructor(public viewCtrl: ViewController,
              public _creditService: CreditService,
              public navParam: NavParams,
              public _creditSerice: CreditService,
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
  totalAmountRemaining = 0;
  creditors: any;

  loggedInUserId = localStorage.getItem('userId');

  dismiss() {
    this.viewCtrl.dismiss();
  }

  getBillDetail(billId) {
    this._creditSerice.GetCreditorsByTransactionId(billId).subscribe(response => {
      console.log(response);
      this.billDetails = response[0].transaction;
      this.billName = this.billDetails.billName;
      this.amountPaid = this.billDetails.amountPaid;
      this.memo = this.billDetails.memo;
      this.purchaseDate = new Date(this.billDetails.purchaseDate);
      this.purchaseDate = (this.purchaseDate.getMonth()+1) +
        '/'+this.purchaseDate.getDate() + '/' + this.purchaseDate.getFullYear();
      this.noOfIndividuals = this.billDetails.noOfIndividuals;
      response.map(u => {
        if(!u.status && u.creditorId != this.loggedInUserId) {
          this.totalAmountRemaining += u.amountToPay;
        }
      });
      this.creditors = response;
    })
  }

  UpdateCreditorTransaction(transactionId, creditorId) {
    let alert = this.alertCtrl.create({
      title: "User Paid",
      subTitle: "Are you sure you want to complete the transaction with this user?",
      buttons: [
        {
          text: 'No',
        },
        {
          text: 'Yes',
          handler: () => {
            this.proceedUserTransactionCompletion(transactionId, creditorId);
          }
        }
      ]
    });
    alert.present();
  }

  proceedUserTransactionCompletion(transactionId, creditorId) {
    this._creditSerice.UpdateCreditorTransaction(transactionId, creditorId).subscribe(response => {
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

  cancelPing(transactionId, creditorId) {
    this._creditSerice.cancelPing(transactionId, creditorId).subscribe(response => {
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
}
