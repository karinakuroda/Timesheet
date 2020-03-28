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
  appointmentForm;
  appointmentStartForm;
  timesheetId;
  projectIdSelected: number;
  startSelected;
  endSelected;
  appointments: Appointment[] = [];
  hasOpenAppointment: boolean;
  elapsedTime: string;
  private timer;
  
  constructor(private formBuilder: FormBuilder, private appointmentService: AppointmentService) {
    this.appointmentStartForm = this.formBuilder.group({
      projectId: 0,
      description: ""
    });

    this.appointmentForm = this.formBuilder.group({
      id: "",
      timesheetId: "",
      start: "",
      end: "",
      projectId: 0,
      description: ""
    });
  }

  ngOnInit() {
    this.appointmentService.getTimesheetByUser("karina.kuroda").subscribe(result => {
      this.timesheetId = result[0].id;
      this.refreshList();
      },
      error => this.treatError(error));
  }

  setProjectId(value) {
    this.projectIdSelected = value;
    this.refreshList();
  }

  onSelectedStart() {
    this.refreshList();
  }

  refreshList() {
    this.appointmentService.getAll(this.timesheetId, this.projectIdSelected, this.startSelected, this.endSelected).subscribe(result => {
        this.appointments = result;
        this.configureOpenedAppointment();
      },
      error => this.treatError(error));
  }

  add(data) {
    this.appointmentService.post(this.timesheetId, data).subscribe(result => {
        this.refreshList();
        this.appointmentForm.reset();
      },
      error => this.treatError(error));
  }
  
  update(data: Appointment) {
    this.appointmentService.put(this.timesheetId, data.id, data).subscribe(result => {
      this.refreshList();
      this.appointmentForm.reset();
    }, error => this.treatError(error));
  }

  deleteAppointment(id) {
    this.appointmentService.delete(this.timesheetId, id).subscribe(() => {
      this.refreshList();
    });
  }

  startAppointment(data) {
    let startAppointment: Appointment = <Appointment>{
      timesheetId: this.timesheetId,
      start: this.appointmentService.getFormatedDate(Date.now()),
      projectId: data.projectId,
      description: data.description,
    }

    this.add(startAppointment);
  }

  stopAppointment() {
    let dateNow = this.appointmentService.getFormatedDate(Date.now());
    let lastAppointment = this.appointments.filter(element => element.end === null);
    this.appointmentService.patchStopAppointment(this.timesheetId, lastAppointment[0].id, dateNow).subscribe(result => {
      this.refreshList();
      this.appointmentForm.reset();
      clearInterval(this.timer);
    }, error => this.treatError(error));
  }

  onNewAppointment() {
    this.appointmentForm.reset();
  }

  onEditAppointment(data: Appointment) {
    let dateStart = this.appointmentService.getFormatedDateWithoutSeconds(data.start);
    let dateEnd = this.appointmentService.getFormatedDateWithoutSeconds(data.end);
    
    this.appointmentForm = this.formBuilder.group({
      id: data.id,
      timesheetId: data.timesheetId,
      start: dateStart,
      end: dateEnd,
      projectId: data.projectId,
      description: data.description
    });
  }

  private configureOpenedAppointment() {
    this.hasOpenAppointment = this.appointments.filter(element => element.end === null).length > 0;

    if (this.hasOpenAppointment) {
      let openedAppointment = this.appointments.filter(element => element.end === null)[0];
      this.configureTimer(openedAppointment.start);
    }
  }

  private configureTimer(startedDate:string) {
     this.timer = setInterval(() => {
      let eventStartTime = new Date(startedDate);
      let eventEndTime = new Date();
      let duration = eventEndTime.valueOf() - eventStartTime.valueOf();
      this.elapsedTime = formatDate(new Date(0, 0, 0, 0, 0, 0, duration), "HH:mm:ss", "en-US");
    }, 1000);
  }

  private treatError(err) {
    console.error(err.error);
    if (err.error.length > 0) {
      alert(err.error[0]);
    } else if (err.error.error.length > 0) {
      alert(err.error.error);
    }
  }
}
