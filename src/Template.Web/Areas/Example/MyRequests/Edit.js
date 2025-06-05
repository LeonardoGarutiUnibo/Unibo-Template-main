var Example;
(function (Example) {
    var MyRequests;
    (function (MyRequests) {
        class editVueModel {
            constructor(hub, model) {
                this.hub = hub;
                this.model = model;
                if (this.hub) {
                    this.hub.on("NewMessage", async (idMyRequest, idMessage) => {
                        // do stuff with parameters
                    });
                }
            }
        }
        MyRequests.editVueModel = editVueModel;
    })(MyRequests = Example.MyRequests || (Example.MyRequests = {}));
})(Example || (Example = {}));
//# sourceMappingURL=Edit.js.map