import { Injectable } from "@angular/core";
import { EntityUIQuery, QueryEntity } from "@datorama/akita";
import { DoctorsStore } from "./doctors.store";
import { DoctorsState, DoctorsUiState } from "./model";

@Injectable({
  providedIn: 'root'
})
export class DoctorsQuery extends QueryEntity<DoctorsState> {
  public doctors$ = this.selectAll();
  public override ui!: EntityUIQuery<DoctorsUiState>;

  public constructor(protected override store: DoctorsStore) {
    super(store);
    this.createUIQuery();
  }

}
