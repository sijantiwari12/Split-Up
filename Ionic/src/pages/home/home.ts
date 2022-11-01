import { Component } from '@angular/core';
import {AlertController, NavController, ToastController} from 'ionic-angular';
import { ModalController } from "ionic-angular";
import { BillDetailsModal } from "../../modals/billDetailsModal/billDetailsModal";
import {UserService} from "../../services/user.service";
import {TransactionService} from "../../services/transaction.service";
import {ViewBillDetailsModal} from "../../modals/viewBillDetailsModal/viewBillDetailsModal";

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {

  username: string;
  genderIcon: string;
  gender: string;
  amountToPay?: any;
  amountToReceive?: any;
  allBills: any;

  constructor(public navCtrl: NavController,
              public modalController: ModalController,
              public userService: UserService,
              public transactionService: TransactionService,
              public alertCtrl: AlertController,
              public toastCtrl: ToastController) {
    this.username = localStorage.getItem('fullName');
    this.gender = localStorage.getItem('gender');
    if(this.gender == 'M') {
      this.genderIcon = "https://www.shareicon.net/download/2016/11/09/851666_user_512x512.png";
    } else if (this.gender == 'F') {
      this.genderIcon = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcThBNKVpuJZE0JcC6XD4rhGVgrIGQcBqDZ805aWiRk_EWZXB6cg";
    } else {
      this.genderIcon = "https://image.flaticon.com/icons/png/512/23/23228.png";
    }
    this.setAmounts();
    this.getAllBills();
  }

  setAmounts = () => {
    this.userService.GetAmounts().subscribe(
      response => {
        this.amountToPay = response['amountToPay'].toFixed(2);
        this.amountToReceive = response['amountToReceive'].toFixed(2);
      });
  }

  getAllBills = () => {
    this.transactionService.GetAllBills().subscribe(
      response => {
        response.map(u => {
          let dateObj = new Date(u.purchaseDate);
          u.purchaseDate = (dateObj.getMonth()+1) +
            '/'+dateObj.getDate() + '/' + dateObj.getFullYear();
        });
        this.allBills = response;
      }
    );
  }

  openModal = () => {
    let amountModal = this.modalController.create(BillDetailsModal);
    amountModal.onDidDismiss(() => {
      this.setAmounts();
      this.getAllBills();
    })
    amountModal.present();
  }

  DeleteTranscation (transactionId) {
    let alert = this.alertCtrl.create({
      title: "Delete Transaction",
      subTitle: "Are you sure you want to delete the transaction?",
      buttons: [
        {
          text: 'No',
        },
        {
          text: 'Yes',
          handler: () => {
            this.proceedTransactionDeletion(transactionId);
          }
        }
      ]
    });
    alert.present();
  }

  proceedTransactionDeletion = (transactionId) => {
    this.transactionService.DeleteTransactions(transactionId).subscribe(response => {
      if(response.status == "Success") {
        this.setAmounts();
        this.getAllBills();

        let toast = this.toastCtrl.create({
          message: response.message,
          duration: 3000,
          position: 'bottom',
        });

        toast.present();
      }
    })
  }

  openBillDetails = (transactionId) => {
    let viewBillDetails = this.modalController.create(ViewBillDetailsModal, {billId: transactionId});
    viewBillDetails.onDidDismiss(() => {
      this.setAmounts();
      this.getAllBills();
    })
    viewBillDetails.present();
  }
}
