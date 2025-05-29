var Example;
(function (Example) {
    var TeamMembers;
    (function (TeamMembers) {
        class editVueModel {
            constructor(hub, model) {
                this.hub = hub;
                this.model = model;
                if (this.hub) {
                    this.hub.on("NewMessage", async (idTeamMember, idMessage) => {
                        // do stuff with parameters
                    });
                }
            }
        }
        TeamMembers.editVueModel = editVueModel;
    })(TeamMembers = Example.TeamMembers || (Example.TeamMembers = {}));
})(Example || (Example = {}));
//# sourceMappingURL=Edit.js.map