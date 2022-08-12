import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'drreview-current-user-review',
  templateUrl: './current-user-review.component.html',
  styleUrls: ['./current-user-review.component.scss']
})
export class CurrentUserReviewComponent implements OnInit{
  @Input()
  public comment: string | undefined;

  public isEditMode = false;

  @Input()
  public rating = 0;

  public initialRating = 0;

  public initialComment: string | undefined;

  @Output()
  public commentSave = new EventEmitter<{comment: string | undefined, rating: number}>();

  public ngOnInit(): void {
    this.initialComment = this.comment;
  }
  public modelChanged(): void{
    console.log(this.comment);
  }

  public toggleEdit() : void{
    this.isEditMode = !this.isEditMode;
  }

  public saveComment() : void{
    this.initialComment = this.comment;
    this.initialRating = this.rating;
    this.commentSave.emit({comment: this.comment, rating: this.rating});
    this.isEditMode = !this.isEditMode;
  }

  public cancelComment() : void{
    this.comment = this.initialComment;
    this.rating = this.initialRating;
    this.isEditMode = !this.isEditMode;
  }

  public resetComment() : void{
    this.comment = this.initialComment;
    this.rating = this.initialRating;
  }
}
