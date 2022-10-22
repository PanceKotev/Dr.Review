import { Component, Input } from '@angular/core';
import { TopDoctor } from '@drreview/shared/data-access';

@Component({
  selector: 'drreview-doctor-ranking-card',
  templateUrl: './doctor-ranking-card.component.html',
  styleUrls: ['./doctor-ranking-card.component.scss']
})
export class DoctorRankingCardComponent{
  @Input()
  public doctor: TopDoctor | undefined;
}
