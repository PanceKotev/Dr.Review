import { ChangeDetectionStrategy, Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { ReviewChangedEvent } from '../events/review.events';

@Component({
  selector: 'drreview-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ReviewComponent implements OnInit {
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

  public initialComment: string | undefined;

  public initialRating = 0;


  @Output()
  public reviewChanged = new EventEmitter<ReviewChangedEvent>();

  @Output()
  public reviewVotedOn = new EventEmitter<boolean | undefined>();

  @Output()
  public deletedReview = new EventEmitter();


  public ngOnInit(): void {
      this.initialComment = this.text;
      this.initialRating = this.givenRating;
  }

  public voteOnReview(upvoted: boolean): void {
    const castVote = upvoted === this.previousVote ? undefined : upvoted;
    this.reviewVotedOn.emit(castVote);
  }


  public toggleEditMode(): void {
    if(this.isEditable){
      this.isInEditMode = !this.isInEditMode;
    }
  }

  public saveChanges(): void {
    this.initialComment = this.text;
    this.initialRating = this.givenRating;
    this.reviewChanged.emit({
      comment: this.text,
      rating: this.givenRating
    });
    this.isInEditMode = false;
  }

  public resetChanges(): void {
    this.text = this.initialComment ?? '';
    this.givenRating =this.initialRating;
  }

  public cancelChanges(): void {
    this.text = this.initialComment ?? '';
    this.givenRating = this.initialRating;
    this.isInEditMode = false;
  }

  public deleteReview(): void {
    if(this.isEditable){
      // eslint-disable-next-line no-alert
      const answer = confirm("Сигурно сакате да ја избришете рецензијата?");
      if(answer){
        this.deletedReview.emit(true);
      }
    }
  }
}
