import { ResultType } from "../enums/result.enum";

export interface Result<T> {
  value: T;
  isSuccess: boolean;
  isFailure: boolean;
  isEmpty: boolean;
  message: string;
  httpStatusCode: number;
  resultType: ResultType;
}
