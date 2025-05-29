module Example.AbsenceEvents {
    export class editVueModel {
        constructor(public hub: any, public model: Example.AbsenceEvents.Server.editViewModel) {
            if (this.hub) {
                this.hub.on("NewMessage", async (idAbsenceEvents: any, idMessage: any) => {
                    // do stuff with parameters
                });
            }
        }
    }
}