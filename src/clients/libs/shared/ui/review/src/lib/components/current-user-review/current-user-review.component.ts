import { Component, Input } from '@angular/core';

@Component({
  selector: 'drreview-current-user-review',
  templateUrl: './current-user-review.component.html',
  styleUrls: ['./current-user-review.component.scss']
})
export class CurrentUserReviewComponent {
  @Input()
  public comment = '';

}
