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
  public set rating(val: number | null | undefined) {
    this._rating = val ?? 0;
  }

  public get rating(): number {
    return this._rating;
  }

  private _rating!: number;

  public initialComment: string | undefined;

  @Output()
  public commentSave = new EventEmitter<{comment: string | undefined, rating: number}>();

  public ngOnInit(): void {
    this.initialComment = this.comment;
  }
  public modelChanged(value: number): void{
    console.log('Comment Value', this.comment);
    console.log(value);

  }

  public toggleEdit() : void{
    this.isEditMode = !this.isEditMode;
  }

  public saveComment() : void{
    this.initialComment = this.comment;
    this.commentSave.emit({comment: this.comment, rating: this.rating ?? 0});
    this.isEditMode = !this.isEditMode;
  }

  public cancelComment() : void{
    this.comment = this.initialComment;
    this.isEditMode = !this.isEditMode;
  }

  public resetComment() : void{
    this.comment = this.initialComment;
  }
}
