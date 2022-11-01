import {Component, OnInit, ViewChild} from '@angular/core';
import { Nav, Platform } from 'ionic-angular';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { HomePage } from '../pages/home/home';
import { ListPage } from '../pages/list/list';
import { UserService} from "../services/user.service";
import { LoginPage } from "../pages/login/login";
import { AlertController } from "ionic-angular";
import { ToastController } from "ionic-angular";
import {BillsToPay} from "../pages/billsToPay/billsToPay";

@Component({
  templateUrl: 'app.html'
})
export class MyApp implements OnInit{
  @ViewChild(Nav) nav: Nav;

  rootPage: any;

  pages: Array<{title: string, component: any, icon: any}>;

  constructor(public platform: Platform,
              public statusBar: StatusBar,
              public splashScreen: SplashScreen,
              private _userService: UserService,
              private  alertController: AlertController,
              private  toastController: ToastController) {
    this.initializeApp();

    // used for an example of ngFor and navigation
    this.pages = [
      { title: 'Home', component: HomePage, icon: 'home' },
      { title: 'List', component: ListPage, icon: 'list' },
      {title: 'Bills To Pay', component: BillsToPay, icon: 'paper'}
    ];

  }

  ngOnInit() {
    if( this._userService.is_User_LoggedIn()) {
      this.rootPage = HomePage;
    } else {
      this.rootPage = LoginPage;
    }
  }

  initializeApp() {
    this.platform.ready().then(() => {
      // Okay, so the platform is ready and our plugins are available.
      // Here you can do any higher level native things you might need.
      this.statusBar.styleDefault();
      this.splashScreen.hide();
    });
  }

  openPage(page) {
    // Reset the content nav to have just this page
    // we wouldn't want the back button to show in this scenario
    this.nav.setRoot(page.component);
  }

  logOut() {
    this._userService.logOut();
    this.nav.setRoot(LoginPage);

    let toast = this.toastController.create({
      message: 'Log Out Successfull!',
      duration: 3000,
      position: 'bottom',
    });

    toast.present();
  }

  presentLogoutConfirm() {
    let alert = this.alertController.create({
      title: 'LogOut',
      message: 'Are you sure you want to logout?',
      buttons: [
        {
          text: 'Cancel',
          role: 'cancel',
          handler: () => {
          }
        },
        {
          text: 'Yes',
          handler: () => {
            this.logOut();
          }
        }
      ]
    });
    alert.present();
  }
}
