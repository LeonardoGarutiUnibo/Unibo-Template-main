var Example;
(function (Example) {
    var AbsenceEvent;
    (function (AbsenceEvent) {
        class editVueModel {
            constructor(hub, model) {
                this.hub = hub;
                this.model = model;
                if (this.hub) {
                    this.hub.on("NewMessage", async (idAbsenceEvent, idMessage) => {
                        // do stuff with parameters
                    });
                }
            }
        }
        AbsenceEvent.editVueModel = editVueModel;
    })(AbsenceEvent = Example.AbsenceEvents || (Example.AbsenceEvents = {}));
})(Example || (Example = {}));
//# sourceMappingURL=Edit.js.map