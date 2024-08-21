export interface ApiEvent {
    id: string,
    user: ApiEventUser,
    title: string,
    start: Date,
    end: Date | undefined,
    status: number,
    userApprovalResponses: ApiEventUserAproval[]
}

export interface ApiEventUser {
    id: string,
    name: string,
    icon: string
}

export interface ApiEventUserAproval {
    id: string,
    user: ApiEventUser,
    approvalStatus: number
}