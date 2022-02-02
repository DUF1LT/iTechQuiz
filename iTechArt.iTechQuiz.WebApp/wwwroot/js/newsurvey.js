$(document).ready(function() {
    $('#singleAnswerOption').click(function() {
        console.log($("#newSurveyMainForm").serialize());
        $.ajax({
            async: true,
            data: $("#newSurveyMainForm").serialize(),
            type: 'POST',
            traditional: true,
            url: 'AddSingleAnswerQuestion',
            success: function(partialView) {
                $('#newSurveyMainForm').html(partialView);
            }
        });
    });
});

function EditQuestion(id) {
    console.log(id);
    console.log(`id=${id}&${$("#newSurveyMainForm").serialize()}`);
    $.ajax({
        async: true,
        data: $("#newSurveyMainForm").serialize(),
        type: 'POST',
        traditional: true,
        url: `EditQuestion/${id}`,
        success: function (partialView) {
            $('#newSurveyMainForm').html(partialView);
        }
    });
}

function SaveQuestion(id) {
    console.log(`id=${id}&${$("#newSurveyMainForm").serialize()}`);
}

function DeleteQuestion(id) {
    console.log(id);
    console.log(`id=${id}&${$("#newSurveyMainForm").serialize()}`);
    $.ajax({
        async: true,
        data: $("#newSurveyMainForm").serialize(),
        type: 'POST',
        traditional: true,
        url: `DeleteQuestion/${id}`,
        success: function (partialView) {
            $('#newSurveyMainForm').html(partialView);
        }
    });
}

function MoveUp(id) {
    console.log(id);
    console.log(`id=${id}&${$("#newSurveyMainForm").serialize()}`);
    $.ajax({
        async: true,
        data: $("#newSurveyMainForm").serialize(),
        type: 'POST',
        traditional: true,
        url: `MoveUp/${id}`,
        success: function (partialView) {
            $('#newSurveyMainForm').html(partialView);
        }
    });
}

function MoveDown(id) {
    console.log(id);
    console.log(`id=${id}&${$("#newSurveyMainForm").serialize()}`);
    $.ajax({
        async: true,
        data: $("#newSurveyMainForm").serialize(),
        type: 'POST',
        traditional: true,
        url: `MoveDown/${id}`,
        success: function (partialView) {
            $('#newSurveyMainForm').html(partialView);
        }
    });
}