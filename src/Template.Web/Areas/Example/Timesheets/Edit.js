var Example;
(function (Example) {
    var Timesheets;
    (function (Timesheets) {
        class editVueModel {
            constructor(hub, model) {
                this.hub = hub;
                this.model = model;
                if (this.hub) {
                    this.hub.on("NewMessage", async (idTimesheet, idMessage) => {
                        // do stuff with parameters
                    });
                }
            }
        }
        Timesheets.editVueModel = editVueModel;
    })(Timesheets = Example.Timesheets || (Example.Timesheets = {}));
})(Example || (Example = {}));
//# sourceMappingURL=Edit.js.map