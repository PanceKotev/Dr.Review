import { GetScheduleSubscriptionDto } from "@drreview/shared/data-access";

export interface Doctor {
   suid: string;
   firstName: string;
   lastName: string;
   institution?: string;
   specialization?: string;
   scheduleSubscription?: GetScheduleSubscriptionDto | null;
   location: string;
}
