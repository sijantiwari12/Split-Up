import {Component, OnInit, ViewChild} from '@angular/core';
import {Nav, NavController} from 'ionic-angular';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { RegisterPage } from '../register/register';
import { HomePage } from "../home/home";
import {UserService} from "../../services/user.service";
import {AlertController} from "ionic-angular";

@Component({
  selector: 'login',
  templateUrl: 'login.html',
  //styleUrls:['../Ionic/src/pages/login/login.css'],
})


export class LoginPage implements OnInit {
  @ViewChild(Nav) nav: Nav;

  constructor(public NavController: NavController,
              private _userService: UserService,
              public alertController: AlertController) {}

  loginform: FormGroup;
  validating: boolean;

  loginDetails = {
    'email': '',
    'password': '',
  }

  ngOnInit() {
    this.validating = false;
    let EmailPattern = /^[a-z0-9!#$%&'*+\/=?^_`{|}~.-]+@[a-z0-9]([a-z0-9-]*[a-z0-9])?(\.[a-z0-9]([a-z0-9-]*[a-z0-9])?)*$/i;
    this.loginform = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.pattern(EmailPattern)]),
      password: new FormControl('', [Validators.required])
    });
  }

  login() {
    this.validating = true;
    this._userService.login(this.loginDetails.email, this.loginDetails.password)
      .subscribe(response => {
        if (response.status == 'Error') {
          let alert = this.alertController.create({
            title: response.status,
            subTitle: response.message,
            buttons: ['Dismiss']
          });
          alert.present();
        } else {
          localStorage.setItem('userId', response.id);
          localStorage.setItem('fullName', response.fullName);
          localStorage.setItem('gender', response.gender);
          this.NavController.setRoot(HomePage);
        }
        this.validating = false;
      });
  }

  registerPage() {
    this.NavController.push(RegisterPage);
  }
}
