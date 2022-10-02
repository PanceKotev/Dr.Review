export interface IOptionItem<TValue = string> {
  label: string;
  value: TValue;
}

export interface IOptionItemWithLink<TValue = string> extends IOptionItem<TValue> {
  link: string;
}

export interface ILocation {
  longitude : number;
  latitude: number;
  name: string;
}
