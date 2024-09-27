export interface Contact {
    id?: number;
    name: string;
    phoneNumber: string;
    contactType: string;

    contactGroupId: number;
    contactGroupName: string;

    isEdit:boolean;
}

export interface ContactGroup {
    id?: number;
    contactGroupName: string;
}
