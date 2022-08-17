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

  @Input()
  public isEditable = true;

  public isInEditMode = false;

  @Output()
  public textChanged = new EventEmitter<string | undefined>();

  @Output()
  public reviewVotedOn = new EventEmitter<boolean | undefined>();


  public voteOnReview(upvoted: boolean): void {
    const castVote = upvoted === this.previousVote ? undefined : upvoted;
    this.reviewVotedOn.emit(castVote);
  }

  public modelChanged(): void {
    console.log(this.text);

  }

  public toggleEditMode(): void {
    if(this.isEditable){
      this.isInEditMode = !this.isInEditMode;
    }
  }

  public saveChanges(): void {
    this.textChanged.emit(this.text);
    console.log('changed');
    this.isInEditMode = false;
  }

  public resetChanges(): void {
    this.isInEditMode = false;

  }
}
