import {Component, OnInit} from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {NavController} from "ionic-angular";
import {UserService} from "../../services/user.service";
import {AlertController} from "ionic-angular";


@Component({
  selector: 'register',
  templateUrl: 'register.html'
})

export class RegisterPage implements OnInit{

  registerForm: FormGroup;

  validating: boolean = false;

  userData = {
    "name": '',
    "email": '',
    "password": '',
    "confirmPassword": '',
    "gender": '',
  };

  constructor(public NavController: NavController,
              private _userService: UserService,
              public alertController: AlertController) {
  }

  ngOnInit() {
    let EMAILPATTERN = /^[a-z0-9!#$%&'*+\/=?^_`{|}~.-]+@[a-z0-9]([a-z0-9-]*[a-z0-9])?(\.[a-z0-9]([a-z0-9-]*[a-z0-9])?)*$/i;
    this.registerForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.pattern(EMAILPATTERN)]),
      password: new FormControl('', [Validators.required, Validators.minLength(6)]),
      confirmPassword: new FormControl('', [Validators.required]),
      gender: new FormControl('', [Validators.required]),
    });
  }

  register() {
    this.validating = true;
    this._userService.register(this.userData)
      .subscribe(response => {
        if (response.status == 'Error') {
          let alert = this.alertController.create({
            title: response.status,
            subTitle: response.message,
            buttons: ['Dismiss']
          });
          alert.present();
        } else if (response.status == 'Success'){
          let alert = this.alertController.create({
            title: response.status,
            subTitle: response.message,
            buttons: ['Dismiss']
          });
          alert.present();
          this.NavController.pop();
        }
        this.validating = false;
      });
  }

  loginPage() {
    this.NavController.pop();
  }
}
