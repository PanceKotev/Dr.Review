export interface IOptionItem<TValue = string> {
  label: string;
  value: TValue;
}

export interface IOptionItemWithLink<TValue = string> extends IOptionItem<TValue> {
  link: string;
}

export interface ILocation {
  suid: string;
  longitude : number;
  latitude: number;
  name: string;
}

export interface IAllOptionsItems {
  locations: ILocation[];
  institutions: string[];
  specializations: string[];
}
