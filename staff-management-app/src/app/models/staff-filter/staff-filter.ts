export class StaffFilter {
    staffID: string | null;
    gender: number;
    fromDate: string | null;
    toDate: string | null;

    constructor(staffID: string | null, gender: number, fromDate: string | null, toDate: string | null) {
        this.staffID = staffID;
        this.gender = gender;
        this.fromDate = fromDate;
        this.toDate = toDate;
    }
}
