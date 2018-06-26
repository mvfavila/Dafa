$('#events').on('click', '.clickable-row', function (event) {
    $(this).addClass('active').siblings().removeClass('active');
});

var FieldViewModel = function () {
    var self = this;

    self.FieldName = ko.observable();
    self.Events = ko.observableArray();
    self.EventTypes = ko.observableArray([]);
    self.ClientId = ko.observable();
    self.FieldId = ko.observable();
    self.Active = ko.observable();

    GetField();
    GetEventTypes();

    self.save = function () {
        saveField(self.FieldId, self.FieldName, self.Events(), self.Active, self.ClientId);        
    };

    self.Name = "";
    self.Description = "";
    self.Date = "";
    self.EventTypeId = "";

    self.addEvent = function () {
        var eventId = uuidv4();
        self.Events.push(ko.utils.extend({},
            new EventObj(eventId, self.Name, self.Description, self.Date, self.EventTypeId, self.FieldId)));
    };

    self.addEventObj = function (eventId, name, description, date, type, fieldId) {
        self.Events.push(ko.utils.extend({}, new EventObj(eventId, name, description, date, type, fieldId)));
    };

    self.addEventType = function (text, value) {
        self.EventTypes.push({ text: text, value: value });
    };

    self.remove = function (row) {
        self.Events.remove(row);
    };
};

function saveField(fieldId, fieldName, events, active, clientId) {
    var field = {
        FieldId: fieldId,
        Name: fieldName,
        Events: events,
        Active: active,
        ClientId: clientId
    };

    $.ajax({
        type: "POST",
        url: '/Field/Edit',
        data: ko.toJS(field),
        dataType: "json",
        success: function (data) {
            alert("Data Saved");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("some error");
        }
    });
}

function EventObj(id, name, description, date, type, fieldId) {
    var self = this;
    self.EventId = id;
    self.Name = name;
    self.Description = description;
    self.Date = date;
    self.EventTypeId = type;
    self.FieldId = fieldId;
}

function GetField() {
    var fieldId = $("#field-id").val();
    $.ajax({
        type: "GET",
        url: '/Field/Get',
        data: { id: fieldId },
        dataType: "json",
        success: function (data) {
            loadData(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            return null;
        }
    });
}

function loadData(data) {
    window.vm.FieldId(data.FieldId);
    window.vm.FieldName(data.Name);
    window.vm.ClientId(data.ClientId);
    window.vm.Active(data.Active);
    for (var i = 0; i < data.Events.length; i++) {
        var e = data.Events[i];
        var formatedDate = new Date(parseInt(e.Date.slice(6, -2))).toLocaleDateString();
        window.vm.addEventObj(e.EventId, e.Name, e.Description, formatedDate, e.EventTypeId, e.FieldId);
    }
}

function GetEventTypes() {
    $.ajax({
        type: "GET",
        url: '/EventType/GetAll',
        dataType: "json",
        success: function (data) {
            loadEventTypes(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            return null;
        }
    });
}

function loadEventTypes(data) {
    for (var i = 0; i < data.length; i++) {
        var et = data[i];
        window.vm.addEventType(et.Name, et.EventTypeId);
    }
}

function uuidv4() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c === 'x' ? r : r & 0x3 | 0x8;
        return v.toString(16);
    });
}

function itemId() {
    var clientId = window.vm.ClientId();
    return clientId;
}

window.vm = new FieldViewModel();
ko.applyBindings(vm);