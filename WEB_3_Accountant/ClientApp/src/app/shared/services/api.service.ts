import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http:HttpClient) { }

  public get(url:string, option? : any)
  {
    return this.http.get(url,option);
  }
  public put(url:string, data:any,option? : any)
  {
    return this.http.put(url,data,option);
  }
}
