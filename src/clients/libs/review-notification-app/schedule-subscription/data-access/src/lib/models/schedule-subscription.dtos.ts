import { ScheduleNotificationRange } from "@drreview/shared/data-access";

export interface CreateNewScheduleSubscriptionsDialogData {
  disabledDoctorUids: string[];
  selectedRange: ScheduleNotificationRange | undefined;
};
