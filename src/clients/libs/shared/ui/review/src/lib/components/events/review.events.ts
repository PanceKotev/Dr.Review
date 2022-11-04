export interface ReviewChangedEvent {
  rating: number;
  comment: string | undefined;
  anonymous: boolean;
}
