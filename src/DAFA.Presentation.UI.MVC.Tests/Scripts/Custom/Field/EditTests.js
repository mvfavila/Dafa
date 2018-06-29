/// <reference path="../../../../dafa.presentation.ui.mvc/scripts/knockout-3.4.2.js" />
/// <reference path="../../../../dafa.presentation.ui.mvc/scripts/custom/field/edit.js" />

describe('addNewEvent', function () {
    var fieldId, eventsList;

    // Arrange
    beforeEach(function () {
        fieldId = 'f979e123-6f11-4bac-9c41-8d534afc640a';
        eventsList = ko.observableArray([]);
    });

    it('New event must be added to list', function () {
        // Act
        var initialListLength = eventsList().length;
        addNewEvent(fieldId, eventsList);
        var newListLength = eventsList().length;
        // Assert
        expect(newListLength).toBe(initialListLength + 1);
    });

    it('Correct event must be added to list', function () {
        // Act
        addNewEvent(fieldId, eventsList);
        var unwrappedList = eventsList();
        var expectedNewEntry = { EventId: fieldId };
        var selectedEvents = unwrappedList.filter(function (item) {
            return item.FieldId === fieldId;
        });
        var selectedEventsLength = selectedEvents.length;
        // Assert
        expect(selectedEventsLength).toBe(1);
    });

    it('Event must be added to the end of the list', function () {
        // Act
        addNewEvent(fieldId, eventsList);
        var unwrappedList = eventsList();
        var lastEntry = unwrappedList[unwrappedList.length - 1];
        // Assert
        expect(lastEntry.FieldId).toBe(fieldId);
    });
});

describe('addNewEventObj', function () {
    var eventId, name, description, date, type, fieldId, eventsList;

    // Arrange
    beforeEach(function () {
        eventId = '46bcc34f-e20f-4448-bcbf-7ec0aa142b4a';
        name = 'DNPM Licence';
        description = 'Licence required by the DNPM';
        date = '29/06/2018';
        type = 'b019838e-c20f-4cc2-9276-deaa209f7434';
        fieldId = 'f979e123-6f11-4bac-9c41-8d534afc640a';
        eventsList = ko.observableArray([]);
    });

    it('New event must be added to list', function () {
        // Act
        var initialListLength = eventsList().length;
        addNewEventObj(eventId, name, description, date, type, fieldId, eventsList);
        var newListLength = eventsList().length;
        // Assert
        expect(newListLength).toBe(initialListLength + 1);
    });

    it('Correct event must be added to list', function () {
        // Act
        addNewEventObj(eventId, name, description, date, type, fieldId, eventsList);
        var unwrappedList = eventsList();
        var expectedNewEntry = {
            EventId: eventId,
            Name: name,
            Description: description,
            Date: date,
            EventTypeId: type,
            FieldId: fieldId            
        };
        // Assert
        expect(unwrappedList).toContain(expectedNewEntry);
    });

    it('Event must be added to the end of the list', function () {
        // Act
        addNewEventObj(eventId, name, description, date, type, fieldId, eventsList);
        var unwrappedList = eventsList();
        var lastEntry = unwrappedList[unwrappedList.length - 1];
        // Assert
        expect(lastEntry.EventId).toBe(eventId);
    });
});