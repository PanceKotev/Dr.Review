import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'drreview-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ReviewComponent {
}
