import {Component} from '@angular/core';
import {AlertController} from "ionic-angular";
import {ViewController} from "ionic-angular";
import {UserService} from "../../services/user.service";
import { NavParams} from "ionic-angular";
import {TransactionService} from "../../services/transaction.service";
import {HomePage} from "../../pages/home/home";
import { NavController} from "ionic-angular";
import { ToastController } from 'ionic-angular';

@Component({
  templateUrl: 'userListModal.html',
})

export class UserListModal {
  constructor(public alertController: AlertController,
              public viewCtrl: ViewController,
              public _userService: UserService, params: NavParams,
              public _transactionService: TransactionService,
              public NavController: NavController,
              public toastController: ToastController,
  ) {
    this.setEmails();
    this.showEmails = this.allEmails;
    this.billDetails = params.get('billDetails');
  }

  selectedEmails:any = [];
  email: string = "";
  allEmails: any = [];
  showEmails:any = [];
  billDetails:any;
  isCompleted = false;

  setEmails = () => {
    this._userService.getAllUsersEmail().subscribe(response => {
      this.allEmails = response;
    });
  }

  setEmailToInput = (email) => {
    this.email = email;
  }

  addEmails = () => {
    if(this.showEmails.find((email) => email == this.email)) {
      this.selectedEmails.push(this.email);
      let indexToDelete = this.showEmails.indexOf(this.email);
      this.showEmails.splice(indexToDelete, 1);
      this.email = "";
    } else {
      this.invalidEmailAlert();
    }
  }

  delete = (chip, email) => {
    this.selectedEmails.splice(chip, 1);
    this.showEmails.push(email);
  }

  invalidEmailAlert = () => {
    let alert = this.alertController.create({
      title: 'Invalid Email',
      subTitle: 'Please enter a valid user email',
      buttons: ["Dismiss"]
    });
    alert.present();
  }

  filterItems = (val) => {
    this.setEmailToInput(val);
    if(this.selectedEmails.length > 0) {
      this.showEmails = this.allEmails.filter(item => !this.selectedEmails.includes(item));
      this.showEmails = this.showEmails.filter(item =>  item.toLowerCase().includes(val.toLowerCase()));
    } else {
      this.showEmails = this.allEmails.filter(item => item.toLowerCase().includes(val.toLowerCase()));
    }
  }

  dismiss = () => {
    this.viewCtrl.dismiss(this.isCompleted);
  }

  submitBill = () => {
    this.isCompleted = true;
    this._transactionService.SaveTransaction(this.billDetails, this.selectedEmails)
      .subscribe(response => {
        if(response.status  == "Success" ) {
          this.toastSuccessMessage(response.message);
        }
      });

    this.dismiss();
  }

  toastSuccessMessage = (message) => {
    let toast = this.toastController.create({
      message: message,
      duration: 3000,
      position: 'bottom',
    });

    toast.present();
  }
}
