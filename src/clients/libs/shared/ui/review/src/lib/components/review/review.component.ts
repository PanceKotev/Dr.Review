import { ChangeDetectionStrategy, Component, Input, Output, EventEmitter, OnInit, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DeleteDialogData } from '@drreview/shared/data-access';
import { ThemesService } from '@drreview/shared/services/themes';
import { DeleteDialogComponent } from '@drreview/shared/ui/dialog';
import { filter, Subject, takeUntil } from 'rxjs';
import { ReviewChangedEvent } from '../events/review.events';

@Component({
  selector: 'drreview-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ReviewComponent implements OnInit, OnDestroy {
  private destroying$ = new Subject<boolean>();
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

  @Input()
  public anonymous = false;

  public initialAnonymous = false;

  public isInEditMode = false;

  public darkTheme = false;

  public initialComment: string | undefined;

  public initialRating = 0;


  @Output()
  public reviewChanged = new EventEmitter<ReviewChangedEvent>();

  @Output()
  public reviewVotedOn = new EventEmitter<boolean | undefined>();

  @Output()
  public deletedReview = new EventEmitter();

  public constructor(
    private dialogService: MatDialog,
    private themeService: ThemesService){
    this.themeService.isDarkTheme$.pipe(takeUntil(this.destroying$)).subscribe(darkTheme => this.darkTheme = darkTheme);
  }

  public ngOnInit(): void {
      this.initialComment = this.text;
      this.initialRating = this.givenRating;
      this.initialAnonymous = this.anonymous;
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
    this.initialAnonymous = this.anonymous;
    this.reviewChanged.emit({
      comment: this.text,
      rating: this.givenRating,
      anonymous: this.anonymous
    });
    this.isInEditMode = false;
  }

  public resetChanges(): void {
    this.text = this.initialComment ?? '';
    this.givenRating =this.initialRating;
    this.anonymous = this.initialAnonymous;
  }

  public cancelChanges(): void {
    this.text = this.initialComment ?? '';
    this.givenRating = this.initialRating;
    this.anonymous = this.initialAnonymous;
    this.isInEditMode = false;
  }

  public deleteReview(): void {
    if(this.isEditable){

      const dialogRef = this.dialogService.open<DeleteDialogComponent, DeleteDialogData, boolean>(DeleteDialogComponent, {

        width: '550px',
        minHeight: '150px',
        hasBackdrop: true,
        panelClass: this.darkTheme ? 'dark-theme' : '',
        data: {
          deleteTitle: 'Избриши ја рецензијата?',
          deleteButtonName: undefined
        }});

      dialogRef.afterClosed().pipe(takeUntil(this.destroying$), filter(x => !!x))
          .subscribe(() =>  this.deletedReview.emit(true));
      }
    }

  public ngOnDestroy(): void {
    this.destroying$.next(true);
    this.destroying$.complete();
  }
}
