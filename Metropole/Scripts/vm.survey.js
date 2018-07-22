var SurveyModel = function(data) {
    var self = this;

    // Properties

    self.modal = $('#add-question');
    self.questions = ko.observableArray([]);
    // This is the Add/Edit dialogue bound context.
    self.current = ko.observable();
    self.isEdit = false;
    // Don't change the calling screen with updates until user saves the changes.
    self.editCopy = null;

    self.hasQuestions = ko.computed(function() {
        return self.questions().length > 0;
    }, self);

    self.questionCount = ko.computed(function() {
        return self.questions().length;
    }, self);
    
    // Functions

    self.newQuestion = function () {
        // Each of the question properties is observable - though the QuestionModel is not an observable.
        self.current(new QuestionModel());
        // Configure popup for processing on save.
        self.isEdit = false;
        // Cancel - there is nothing to do on cancel.
        self.modal.find('.modal-title').text("Add Question");
        self.modal.modal();
    };

    self.editQuestion = function (item) {

        // Track the item being edited.
        // Only make changes to it when the user requests save.
        // If the user clicks cancel - there is nothing to do as no changes to the calling screen
        // have been made.
        // Updating this item will update the calling screen due to binding.
        self.editCopy = item;

        // Copy the edited item property values.
        var edit = new QuestionModel()
        edit.title(self.editCopy.title());
        edit.type(self.editCopy.type());
        edit.body(self.editCopy.body());
        self.current(edit);

        // Copy the edited item  - its just an object now, not a QuestionModel.
        //self.current(ko.toJS(item));
        self.isEdit = true;
        self.modal.find('.modal-title').text("Edit Question");
        self.modal.modal();
    };

    self.saveQuestion = function(item) {
        var index;
        if (item.isValid()) {
            if (self.isEdit) {
                // Update the edited properties to cause an update on the calling screen.
                // Note: The hidden named fields for postback are also bound to these properties and will update.
                // TODO: Better way to update entire object.
                self.editCopy.title(item.title());
                self.editCopy.type(item.type());
                self.editCopy.body(item.body());
                self.editCopy = null;
            } else {
                // Update calling screen with new observable.
                // Note: item is a QuestionModel
                self.questions.push(item);
            }

            self.modal.modal('hide');
        }
        else {
            alert('Error: All fields are required!');
        }
    };

    self.moveUp = function(item) {
        var currIndex = self.questions.indexOf(item),
            prevIndex = currIndex - 1;

        if (currIndex > 0) {
            self.questions.splice(currIndex, 1);
            self.questions.splice(prevIndex, 0, item);
        }
    };
    
    self.moveDown = function(item) {
        var currIndex = self.questions.indexOf(item),
            nextIndex = currIndex + 1,
            lastIndex = self.questions().length - 1;

        if (currIndex < lastIndex) {
            self.questions.splice(currIndex, 1);
            self.questions.splice(nextIndex, 0, item);
        }
    };

    // Callbacks

    self.afterAdd = function(elem) {
        var el = $(elem);
        if (elem.nodeType === 1) {
            el.before("<div/>");
            el.prev()
                .width(el.innerWidth())
                .height(el.innerHeight())
                .css({
                    "position": "absolute",
                    "background-color": "#ffff99",
                    "opacity": "0.5"
                })
                .fadeOut(500);
        }
    };
    
    // Initialize - with json data

    if (data != null) {
        for (var i = 0; i < data.Questions.length; i++) {
            var q = new QuestionModel();
            q.id(data.Questions[i].Id);
            q.title(data.Questions[i].Title);
            q.type(data.Questions[i].Type);
            q.body(data.Questions[i].Body);
            q.isActive(data.Questions[i].IsActive);
            self.questions.push(q);
        }
    }

    return self;
};