import { ApiEvent, ApiEventUserAproval } from '../api-models/event.model';
import { EventApproval, EventModel, EventStatus } from './event-list.models';

export function mapEvent(event: ApiEvent): EventModel {
  return {
    id: event.id,
    userId: event.user.id,
    userName: event.user.name,
    userIcon: event.user.icon,
    status: getEventStatusByValue(event.status),
    fromDate: new Date(event.start),
    toDate: event.end ? new Date(event.end) : undefined,
    userApprovalResponses: event.userApprovalResponses.map(mapApproval),
  };
}

function mapApproval(approval: ApiEventUserAproval): EventApproval {
  return {
    id: approval.id,
    userId: approval.user.id,
    userName: approval.user.name,
    userIcon: approval.user.icon,
    approvalStatus: getEventStatusByValue(approval.approvalStatus),
  };
}

function getEventStatusByValue(statusCode: number): EventStatus | undefined {
  return statusCode as EventStatus;
}
