import { ChangeDetectionStrategy, Component, EventEmitter, Output } from '@angular/core';
import { ReviewChangedEvent } from '../events/review.events';

@Component({
  selector: 'drreview-create-review',
  templateUrl: './create-review.component.html',
  styleUrls: ['./create-review.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CreateReviewComponent {

  public comment : string | undefined;

  public rating = 0;

  public anonymous = true;

  @Output()
  public reviewCreate = new EventEmitter<ReviewChangedEvent>();

  public createReview() : void{
    this.reviewCreate.emit({ comment: this.comment, rating: this.rating, anonymous: this.anonymous});
  }

}
