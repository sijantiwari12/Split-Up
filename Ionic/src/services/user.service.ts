import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable()

export class UserService {

  constructor(private http: HttpClient) {}

  SITEURL = "https://localhost:44388/api/";


  is_User_LoggedIn() {
    return localStorage.getItem('userId');
  }

  login(email: string, password: string) {
    return this.http.post<any>(
      this.SITEURL + 'users/login',
      {'Email': email, 'Password': password});
  }

  logOut(): void {
    localStorage.clear();
  }

  register = (user: any ) => {
    return this.http.post<any>(
      this.SITEURL + 'users/register',
      {'Email': user.email,
        'FullName': user.name,
        'Password': user.password,
        'Gender': user.gender
      }
    )
  }

  getAllUsersEmail = () => {
    return this.http.get(this.SITEURL + 'users/GetAllUsersEmail');
  }

  GetAmounts = () => {
    return this.http.get(this.SITEURL + "users/GetAmounts/" + localStorage.getItem('userId'));
  }

  getNameById = (userId) => {return  this.http.get(this.SITEURL + "users/GetNameById/" + userId)};
}
