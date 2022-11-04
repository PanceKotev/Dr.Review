import { Doctor } from './../interfaces/doctor';
import { Injectable } from "@angular/core";
import { EntityStore, EntityUIStore, StoreConfig } from "@datorama/akita";
import { initialState, DoctorsState, DoctorsUiState, DoctorUI } from "./model";

@StoreConfig({ name: 'doctors', idKey: 'suid' })
@Injectable({providedIn: 'root'})
export class DoctorsStore extends EntityStore<DoctorsState> {
  public override ui!: EntityUIStore<DoctorsUiState>;

  public constructor() {
    super(initialState);

    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    const defaults: (e: Doctor) => DoctorUI = _entity => ({ selected: false});

    this.createUIStore().setInitialEntityState(defaults);
  }
}
