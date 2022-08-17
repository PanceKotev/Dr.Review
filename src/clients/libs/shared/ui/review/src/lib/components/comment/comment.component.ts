import { ChangeDetectionStrategy, Component, forwardRef, Input } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { BaseControlValueAccessor } from '@drreview/shared/utils/form';

@Component({
  selector: 'drreview-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [{ provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => CommentComponent), multi: true }]

})
export class CommentComponent extends BaseControlValueAccessor<string | undefined> {

  @Input()
  public isEditable = false;

  @Input()
  public comment: string | undefined;
}
