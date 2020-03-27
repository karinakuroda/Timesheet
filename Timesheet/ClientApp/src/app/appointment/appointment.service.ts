import { Appointment } from './appointment';
import { Observable, of } from 'rxjs';
import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AppointmentService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

  }

  public getAll(timesheetId: string): Observable<any> {
    return this.http.get<any[]>(this.getUrl(timesheetId));
  }

  public patchStopAppointment(timesheetId: string, id:string, dateTimeEnd:string): Observable<any> {
    let patchData: any[] = [];

    patchData.push(this.createDataToModify(dateTimeEnd, 'end'));

    return this.http.patch<any[]>(this.getUrl(timesheetId, id), patchData);
  }

  public put(timesheetId: string, id: string, data): Observable<any> {
    return this.http.put<any[]>(this.getUrl(timesheetId, id), data);
  }

  public post(timesheetId: string, data: any): Observable<any> {
    return this.http.post<any[]>(this.getUrl(timesheetId), data);
  }

  public delete(timesheetId: string, id: string): Observable<any> {
    return this.http.delete<any[]>(this.getUrl(timesheetId, id));
  }

  private getUrl(timesheetId: string, id?: string): string {
    let url = this.baseUrl + '/timesheets/' + timesheetId + '/appointments';

    if (id != null) {
      url += '/' + id;
    }

    return url;
  }

  private createDataToModify(value, path) {
    let patchData = {
      "value": value,
      "path": "/" + path,
      "op": "replace"
    }
    return patchData;
  }
}

