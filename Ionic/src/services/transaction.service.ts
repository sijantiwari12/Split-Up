import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';

@Injectable()

export class TransactionService {

  constructor(private http: HttpClient) {}

  SITEURL = "https://localhost:44388/api/";

  SaveTransaction(billDetails: any, emails) {
    return this.http.post<any>(
      this.SITEURL + 'transaction/SaveTransaction',
      {'Transaction': billDetails, 'emails': emails});
  }

  GetAllBills = (): Observable<any> => this.http.get<any>(this.SITEURL + 'transaction/GetAllBills/' + localStorage.getItem('userId'));

  DeleteTransactions = (transactionId): Observable<any> => this.http.delete<any>(this.SITEURL + 'transaction/DeleteTransaction/' + transactionId);

  getUserIncludedBills = ():Observable<any> => this.http.get<any>(this.SITEURL + 'Transaction/GetUserIncludedTransactions/' + localStorage.getItem('userId'));
}
