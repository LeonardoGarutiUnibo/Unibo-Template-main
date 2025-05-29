var Example;
(function (Example) {
    var Teams;
    (function (Teams) {
        class editVueModel {
            constructor(hub, model) {
                this.hub = hub;
                this.model = model;
                if (this.hub) {
                    this.hub.on("NewMessage", async (idTeam, idMessage) => {
                        // do stuff with parameters
                    });
                }
            }
        }
        Teams.editVueModel = editVueModel;
    })(Teams = Example.Teams || (Example.Teams = {}));
})(Example || (Example = {}));
//# sourceMappingURL=Edit.js.map