import {Component, OnInit} from '@angular/core';
import { ViewController } from 'ionic-angular';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { ModalController} from "ionic-angular";
import {UserListModal} from "../userListModal/userListModal";

@Component({
  templateUrl: 'billDetailsModal.html',
})

export class BillDetailsModal implements OnInit {
  constructor(public viewCtrl: ViewController, public modalController: ModalController) {}
  maxDate = new Date().toISOString();

  billForm: FormGroup;


  billDetails: any = {
    'amountPaid': '',
    'billName': '',
    'noOfIndividuals': '',
    'purchaseDate': new Date().toISOString(),
    'memo': '',
    'userId': localStorage.getItem('userId')
  }

  ngOnInit() {
    this.billForm = new FormGroup({
      amountPaid: new FormControl('', [Validators.required, Validators.min(0.1)]),
      billName: new FormControl('', [Validators.required]),
      noOfIndividuals: new FormControl('', [Validators.required, Validators.min(1)]),
      purchaseDate: new FormControl('', [Validators.required]),
      memo: new FormControl(),
    })
  }

  dismiss() {
    this.viewCtrl.dismiss();
  }

  nextStep() {
    let usersList = this.modalController.create(UserListModal, {billDetails: this.billDetails});
    usersList.onDidDismiss(response => {
      console.log(response);
      if (response) {
        this.dismiss();
      }
    })
    usersList.present();
  }
}
