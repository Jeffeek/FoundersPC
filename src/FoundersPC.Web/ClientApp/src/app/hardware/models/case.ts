import { Producer } from "src/app/hardware/models/producer";

export class Case {
    constructor(
        public id?: number,
        public hardwareTypeId?: number,
        public producerId?: number,
        public title?: string,
        public producer?: Producer,
        public created?: Date,
        public createdBy?: string,
        public lastModified?: Date,
        public lastModifiedBy?: string) { }
}