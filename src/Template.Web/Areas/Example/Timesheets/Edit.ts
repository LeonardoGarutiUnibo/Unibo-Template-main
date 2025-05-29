module Example.Timesheets {
    export class editVueModel {
        constructor(public hub: any, public model: Example.Timesheets.Server.editViewModel) {
            if (this.hub) {
                this.hub.on("NewMessage", async (idTimesheets: any, idMessage: any) => {
                    // do stuff with parameters
                });
            }
        }
    }
}