import {Component, ViewChild} from '@angular/core';
import {ModalController, Nav, NavController} from 'ionic-angular';
import {AlertController} from "ionic-angular";
import {TransactionService} from "../../services/transaction.service";
import {ViewCreditorBillDetails} from "../../modals/viewCreditorBillDetails/viewCreditorBillDetails";

@Component({
  selector: 'billsToPay',
  templateUrl: 'billsToPay.html',
})


export class BillsToPay {
  @ViewChild(Nav) nav: Nav;

  userIncludedBills: any;

  constructor(public NavController: NavController,
              private transactionService: TransactionService,
              public alertController: AlertController,
              public modalController: ModalController) {
    this.getUserIncludedBills();
  }
  loggedInUser = localStorage.getItem("userId");
  getUserIncludedBills = () => {
    this.transactionService.getUserIncludedBills().subscribe(
      response => {
        response.map(u => {
          let dateObj = new Date(u.purchaseDate);
          u.purchaseDate = (dateObj.getMonth()+1) +
            '/'+dateObj.getDate() + '/' + dateObj.getFullYear();
        });
        this.userIncludedBills = response;
        console.log(response);
      }
    );
  }

  openCreditorBillDetails(transactionId) {
    let viewCreditorBillDetails = this.modalController.create(ViewCreditorBillDetails, {billId: transactionId});
    viewCreditorBillDetails.present();
  }
}
