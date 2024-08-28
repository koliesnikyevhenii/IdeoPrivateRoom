export interface ApiEvent {
  id: string;
  user: ApiEventUser;
  title: string;
  start: Date;
  end: Date | undefined;
  status: number;
  reviewers: ApiEventReviewer[];
}

export interface ApiEventUser {
    id: string,
    name: string,
    icon: string
}

export interface ApiEventReviewer extends ApiEventUser {
  approvalStatus: number;
}

export interface ApiPaginatedResponse<T> {
  pageNumber: number;
  pageSize: number;
  totalRecords: number;
  totalPages: number;
  data: T[];
}