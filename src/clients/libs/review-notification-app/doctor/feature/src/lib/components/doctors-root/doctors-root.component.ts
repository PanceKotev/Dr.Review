import { Component } from '@angular/core';
import { filterOptions } from '@drreview/review-notification-app/doctor/data-access';

@Component({
  selector: 'drreview-doctors-root',
  templateUrl: './doctors-root.component.html',
  styleUrls: ['./doctors-root.component.scss']
})
export class DoctorsRootComponent {
  public filterOptions = filterOptions;

  public filterValue = '';


  public modelChanged(valeu: string): void {
    console.log('value changed', valeu);
  }
}
