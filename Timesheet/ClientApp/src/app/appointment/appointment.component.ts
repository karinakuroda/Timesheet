import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { formatDate } from "@angular/common";
import { Appointment } from './appointment';
import { AppointmentService } from './appointment.service';

@Component({
  selector: 'app-appointment-component',
  templateUrl: './appointment.component.html'
})
export class AppointmentComponent implements OnInit {
  public appointmentForm;
  public appointmentStartForm;
  public timesheetId = "2067df1b-b937-4a8f-97af-08d7d1d8e7fb";
  public appointments: Appointment[] = [];

  constructor(private formBuilder: FormBuilder, private appointmentService: AppointmentService) {
    this.appointmentStartForm = this.formBuilder.group({
      projectId: 0,
      description: ''
    });

    this.appointmentForm = this.formBuilder.group({
      id: '',
      timesheetId: '',
      start: '',
      end: '',
      projectId: 0,
      description: ''
    });
  }

  ngOnInit() {
    this.refreshList();
  }

  refreshList() {
    this.appointmentService.getAll(this.timesheetId).subscribe(result => {
      this.appointments = result;
    }, error => console.error(error));
  }
  
  add(data) {
    this.appointmentService.post(this.timesheetId, data).subscribe(result => {
      this.refreshList();
      this.appointmentForm.reset();
    }, error => console.error(error));
  }
  
  update(data: Appointment) {
    this.appointmentService.put(this.timesheetId, data.id, data).subscribe(result => {
      this.refreshList();
      this.appointmentForm.reset();
    }, error => console.error(error));
  }

  deleteAppointment(id) {
    this.appointmentService.delete(this.timesheetId, id).subscribe(() => {
      this.refreshList();
    });
  }

  startAppointment(data) {
    let dateNow = formatDate(Date.now(), "MM/dd/yyyy HH:mm", "en-US");
    
    let startAppointment: Appointment = <Appointment>{
      timesheetId : this.timesheetId,
      start: dateNow,
      projectId : data.projectId,
      description : data.description,
    }

    this.add(startAppointment);
  }

  stopAppointment() {
    let dateNow = formatDate(Date.now(), "MM/dd/yyyy HH:mm", "en-US");
    var lastAppointment = this.appointments.filter(element => element.end === "0001-01-01T00:00:00");
    this.appointmentService.patchStopAppointment(this.timesheetId, lastAppointment[0].id, dateNow).subscribe(result => {
      this.refreshList();
      this.appointmentForm.reset();
    }, error => console.error(error));
  }

  onNewAppointment() {
    this.appointmentForm.reset();
  }
  
  onEditAppointment(data: Appointment) {
    let dateStart = formatDate(data.start, "MM/dd/yyyy HH:mm", "en-US");
    let dateEnd = formatDate(data.end, "MM/dd/yyyy HH:mm", "en-US");

    this.appointmentForm = this.formBuilder.group({
      id: data.id,
      timesheetId: data.timesheetId,
      start: dateStart,
      end: dateEnd,
      projectId: data.projectId,
      description: data.description
    });
  }

 
}
