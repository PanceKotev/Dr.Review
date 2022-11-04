import { ActiveState, EntityState } from "@datorama/akita";
import { FilterBy, FilterByValue } from "@drreview/shared/data-access";
import { Doctor } from "../interfaces";

export interface DoctorUI{
  selected: boolean;
}

export interface DoctorsState extends EntityState<Doctor, string>, ActiveState<string> {
  ui: {
    filter: FilterByValue | undefined
  }
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface DoctorsUiState extends EntityState<DoctorUI> {
}

export const initialState: DoctorsState = {
   active: null,
   ui: {
    filter: { property: FilterBy.ALL, value: ''}
   }
};
