var SurveyListModel = function() {
    var self = this;

    self.destroy = function (item, event) {
        var $target = $(event.target);

        if (confirm('Are you sure you want to delete this survey?')) {
            $.post($target.attr('href'), function(data) {
                //$target.parents('tr').remove();
                window.location.href = data;
            });
        }

        return false;
    };
};