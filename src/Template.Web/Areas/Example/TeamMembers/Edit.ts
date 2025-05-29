module Example.TeamMembers {
    export class editVueModel {
        constructor(public hub: any, public model: Example.TeamMembers.Server.editViewModel) {
            if (this.hub) {
                this.hub.on("NewMessage", async (idTeamMembers: any, idMessage: any) => {
                    // do stuff with parameters
                });
            }
        }
    }
}