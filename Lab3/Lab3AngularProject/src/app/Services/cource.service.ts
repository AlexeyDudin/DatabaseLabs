import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ServerResponse } from '../Entityes/ServerResponse';
import { AppSettings } from '../Constants/Urls';

@Injectable({
  providedIn: 'root'
})
export class CourceService {
  result: any;
  constructor(private http: HttpClient) { }

  sendPost(routing: string, obj:any):Observable<ServerResponse> {
    return this.http.post<any>(AppSettings.hostAddress + routing, obj, {responseType: "json"});
  }
  sendGet(routing: string):Observable<ServerResponse> {
    return this.http.get<any>(AppSettings.hostAddress + routing, {responseType: "json"});
  }
}
