import { SharedUtilsDateModule } from '@drreview/shared/utils/date';
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
import { CreateReviewComponent } from './components/create-review/create-review.component';
import { AvatarModule } from 'ngx-avatars';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatProgressBarModule,
    AvatarModule,
    SharedUtilsDateModule,
    StarRatingModule,
    QuillModule
  ],
  declarations: [
    CommentComponent,
    ReviewComponent,
    SummaryComponent,
    RatingComponent,
    CreateReviewComponent
  ],
  exports: [CommentComponent, ReviewComponent, SummaryComponent, CreateReviewComponent]
})
export class SharedUiReviewModule {}
