
export class Staff {
    id: number;
    staffID: string;
    fullName: string;
    birthday: string;
    genders: string;
    gender: number;
    createdDateTime: Date

    constructor(id: number, staffID: string, fullName: string, birthday: string, genders: string, gender: number, createdDateTime: Date) {
        this.id = id;
        this.staffID = staffID;
        this.fullName = fullName;
        this.birthday = birthday;
        this.genders = genders;
        this.gender = gender;
        this.createdDateTime = createdDateTime
    }
}
