import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'drreview-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SummaryComponent {
}
