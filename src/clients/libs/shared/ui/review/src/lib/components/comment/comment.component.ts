import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'drreview-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.scss']
})
export class CommentComponent implements OnInit{

  public isEditMode = true;

  @Input()
  public comment: string | undefined;

  public initialComment: string | undefined;

  @Output()
  public commentSave = new EventEmitter<string>();

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
    this.commentSave.emit(this.comment);
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
