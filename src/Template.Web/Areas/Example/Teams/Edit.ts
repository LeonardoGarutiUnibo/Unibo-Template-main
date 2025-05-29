module Example.Teams {
    export class editVueModel {
        constructor(public hub: any, public model: Example.Teams.Server.editViewModel) {
            if (this.hub) {
                this.hub.on("NewMessage", async (idTeams: any, idMessage: any) => {
                    // do stuff with parameters
                });
            }
        }
    }
}