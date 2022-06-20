import { SearchDoctorDto } from '@drreview/shared/data-access';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'drreview-doctor-card',
  templateUrl: './doctor-card.component.html',
  styleUrls: ['./doctor-card.component.scss']
})
export class DoctorCardComponent {
  @Input()
  public doctor!: SearchDoctorDto;
}
