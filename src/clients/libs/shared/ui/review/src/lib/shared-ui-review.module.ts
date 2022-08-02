import { StarRatingModule } from 'angular-star-rating';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CommentComponent } from './components/comment/comment.component';
import { QuillModule } from 'ngx-quill';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ReviewComponent } from './components/review/review.component';
import { SummaryComponent } from './components/summary/summary.component';
import { RatingComponent } from './components/rating/rating.component';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { CurrentUserReviewComponent } from './components/current-user-review/current-user-review.component';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatProgressBarModule,
    StarRatingModule,
    QuillModule
  ],
  declarations: [
    CommentComponent,
    ReviewComponent,
    SummaryComponent,
    RatingComponent,
    CurrentUserReviewComponent
  ],
  exports: [CommentComponent, ReviewComponent, SummaryComponent]
})
export class SharedUiReviewModule {}
