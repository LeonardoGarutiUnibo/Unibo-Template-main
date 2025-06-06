var Example;
(function (Example) {
    var Requests;
    (function (Requests) {
        class editVueModel {
            constructor(hub, model) {
                this.hub = hub;
                this.model = model;
                if (this.hub) {
                    this.hub.on("NewMessage", async (idRequest, idMessage) => {
                        // do stuff with parameters
                    });
                }
            }
        }
        Requests.editVueModel = editVueModel;
    })(Requests = Example.Requests || (Example.Requests = {}));
})(Example || (Example = {}));
//# sourceMappingURL=Edit.js.map