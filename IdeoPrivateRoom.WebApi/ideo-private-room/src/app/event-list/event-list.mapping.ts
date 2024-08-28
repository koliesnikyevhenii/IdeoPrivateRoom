import { ApiEvent, ApiEventReviewer } from '../api-models/event.model';
import { EventApproval, EventModel, EventReviewer, EventStatus } from './event-list.models';

export function mapEvent(event: ApiEvent): EventModel {
  return {
    id: event.id,
    userId: event.user.id,
    userName: event.user.name,
    userIcon: event.user.icon,
    status: getEventStatusByValue(event.status) ?? EventStatus.Pending,
    fromDate: new Date(event.start),
    toDate: event.end ? new Date(event.end) : undefined,
    reviewers: event.reviewers.map(mapReviewer)
  };
}

function mapReviewer(reviewer: ApiEventReviewer): EventReviewer {
  return {
    id: reviewer.id,
    name: reviewer.name,
    icon: reviewer.icon,
    approvalStatus: reviewer.approvalStatus
  };
}

function getEventStatusByValue(statusCode: number): EventStatus | undefined {
  return statusCode as EventStatus;
}
