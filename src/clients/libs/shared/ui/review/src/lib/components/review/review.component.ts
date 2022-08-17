import { ChangeDetectionStrategy, Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'drreview-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ReviewComponent {
  @Input()
  public text = '';

  @Input()
  public createdBy = '';

  @Input()
  public updatedOn = new Date();

  @Input()
  public numberOfLikes = 0;

  @Input()
  public numberOfDislikes = 0;

  @Input()
  public givenRating = 0.0;

  @Input()
  public previousVote: boolean | undefined;

  @Output()
  public reviewVotedOn = new EventEmitter<boolean | undefined>();


  public voteOnReview(upvoted: boolean): void {
    const castVote = upvoted === this.previousVote ? undefined : upvoted;
    this.reviewVotedOn.emit(castVote);
  }
}
