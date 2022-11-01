import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';

@Injectable()

export class CreditService {

  constructor(private http: HttpClient) {}

  SITEURL = "https://localhost:44388/api/";

  GetCreditorsByTransactionId = (transactionId): Observable<any> => this.http.get<any>(this.SITEURL + 'Credit/GetCreditorsByTransaction/' + transactionId);

  UpdateCreditorTransaction = (transactionId, creditorId):Observable<any> => this.http.get(this.SITEURL + 'Credit/UpdateCreditorTransaction/' + transactionId + '/' + creditorId);

  pingTransaction = (transactionId, creditorId):Observable<any> => this.http.get(this.SITEURL + 'Credit/PingTheUser/' + transactionId + '/' + creditorId);

  cancelPing = (transactionId, creditorId):Observable<any> => this.http.get(this.SITEURL + 'Credit/cancelPing/' + transactionId + '/' + creditorId);
}
