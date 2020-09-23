import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
//import { UrlConstants } from '../../core/common/url.constants';
//import { AuthenService } from './authen.service';

@Injectable()
export class UtilityService {
  private _router: Router;
  private id: number;
  private content: string;
  private schedule: boolean;
  private selectedRowsData: any[] = [];
  // private messageSource = new BehaviorSubject('default message');
  // currentMessage = this.messageSource.asObservable();
  // constructor(router: Router, private http: Http, private authenService: AuthenService) {
  //   this._router = router;
  // }
  constructor(router: Router) {
    this._router = router;
  }

  public set viewSchedule(data: any) {
    this.schedule = data;
  } 

  public get viewSchedule(): any {
    return this.schedule;
  }

  public set selectedRows(data: any[]){
    this.selectedRowsData = data;
  }

  public get selectedRows(){
    return this.selectedRowsData;
  }
  
  // public set label(data: any) {
  //   this.labelName = data;
  //   console.log(this.labelName);
  // } 

  // public get label(): any {
  //   console.log(this.labelName);
  //   return this.labelName;
  // }

  // changeMessage(message: string) {
  //   this.messageSource.next(message);
  //   console.log(this.currentMessage, this.messageSource.value);
  // }
}
