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