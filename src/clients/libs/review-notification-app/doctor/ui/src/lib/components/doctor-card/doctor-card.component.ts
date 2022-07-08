import { SearchDoctorDto } from '@drreview/shared/data-access';
import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'drreview-doctor-card',
  templateUrl: './doctor-card.component.html',
  styleUrls: ['./doctor-card.component.scss']
})
export class DoctorCardComponent {

  public constructor(private router: Router){

  }

  @Input()
  public doctor!: SearchDoctorDto;

  public get doctorFullName(): string{
    if(!this.doctor){
      return '';
    }

    return `${this.doctor.firstName} ${this.doctor.lastName}`;
  }

  public goToDetails() : void{
    this.router.navigateByUrl(`/doctors/${this.doctor.suid}`);
  }
}
