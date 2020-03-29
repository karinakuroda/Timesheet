import { Appointment } from './appointment';
import { Observable, of } from 'rxjs';
import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { formatDate } from "@angular/common";

@Injectable({
  providedIn: 'root',
})
export class AppointmentService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  private dateTimeFormatWithoutSeconds = "MM/dd/yyyy HH:mm";
  private dateTimeFormat = "MM/dd/yyyy HH:mm:ss";

  getFormatedDate(date) {
    return formatDate(date, this.dateTimeFormat, "en-US");
  }

  getFormatedDateWithoutSeconds(date) {
    return formatDate(date, this.dateTimeFormatWithoutSeconds, "en-US");
  }

  getTimesheetByUser(username: string): Observable<any> {
    return this.http.get<any[]>(this.baseUrl + "api/timesheets?username=" + username);
  }

  getAll(timesheetId: string, projectId?: number, startSelected?, endSelected?): Observable<any> {
    let url = this.getUrl(timesheetId)+'?';

    if (projectId != null) {
      url += "projectId=" + projectId;
    }

    if (startSelected != null) {
      let dateStart = this.getFormatedDateWithoutSeconds(startSelected);
      url += "&start=" + dateStart;
    }

    if (endSelected != null) {
      let dateEnd = this.getFormatedDateWithoutSeconds(endSelected);
      url += "&end=" + dateEnd;
    }

    return this.http.get<any[]>(url);
  }

  patchStopAppointment(timesheetId: string, id: string, dateTimeEnd: string): Observable<any> {
    const patchData: any[] = [];

    patchData.push(this.createDataToModify(dateTimeEnd, "end"));

    return this.http.patch<any[]>(this.getUrl(timesheetId, id), patchData);
  }

  put(timesheetId: string, id: string, data): Observable<any> {
    return this.http.put<any[]>(this.getUrl(timesheetId, id), data);
  }

  post(timesheetId: string, data: any): Observable<any> {
    return this.http.post<any[]>(this.getUrl(timesheetId), data);
  }

  delete(timesheetId: string, id: string): Observable<any> {
    return this.http.delete<any[]>(this.getUrl(timesheetId, id));
  }

  private getUrl(timesheetId: string, id?: string): string {
    let url = this.baseUrl + "api/timesheets/" + timesheetId + "/appointments";

    if (id != null) {
      url += "/" + id;
    }

    return url;
  }

  private createDataToModify(value, path) {
    const patchData = {
      "value": value,
      "path": "/" + path,
      "op": "replace"
    }
    return patchData;
  }
}

