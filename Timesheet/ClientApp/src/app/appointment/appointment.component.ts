import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CalendarModule } from 'primeng/calendar';
//import { TableModule } from 'primeng/table';

@Component({
  selector: 'app-appointment-component',
  templateUrl: './appointment.component.html'
})
export class AppointmentComponent implements OnInit {
  checkoutForm;
  items;
  success;
  public currentCount = 0;
  public timesheetId = "2067df1b-b937-4a8f-97af-08d7d1d8e7fb";
  public appointments: any[];
  public showManually: boolean = false;

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private formBuilder: FormBuilder) {

    this.checkoutForm = this.formBuilder.group({
      start: '',
      end: '',
      projectId: '',
      description:''
    });
  }

  ngOnInit() {
    this.items = [];//this.cartService.getItems();

    this.http.get<any[]>(this.baseUrl + '/timesheets/' + this.timesheetId + '/appointments').subscribe(result => {
      this.appointments = result;
    }, error => console.error(error));
  }

  onSubmit(data) {
    this.http.post<any[]>(this.baseUrl + '/timesheets/' + this.timesheetId + '/appointments', data).subscribe(result => {
      this.success = result;
    }, error => console.error(error));
  }

  public addManually() {
    this.showManually = !this.showManually;
  }

  public saveManually() {
   
  }

  public cancel() {
    this.showManually = false;
  }
}
