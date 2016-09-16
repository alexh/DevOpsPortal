$(document).ready(function () {
    CreateDatePicker();
    $('#DeployDateTimeNotNull').val("Pick Date");

    //Needed for case where Testable is true but QA fields have not been entered
    var expanded = $('#TestableInQABool').is(':checked');
    if (!expanded) {
        var $section = $('#QAsection');
        $section.show(500);
    }

    var expanded1 = $('#TestableInQABool').is(':checked');
    if (!expanded1) {
        var $section1 = $('#QAsectionDB');
        $section1.show(500);
    }
    var teamId = $('#Team').val();
    if (teamId != null) {
        $.ajax({
            url: '/Teams/GetValidEnvironments',
            type: "GET",
            dataType: "JSON",
            data: { Team: teamId, User: userId },
            success: function (envs) {
                $("#Environment").html("");
                $.each(envs, function (i, env) {
                    $("#Environment").append(
                    $('<option></option').val(env.EnvName).html(env.EnvName));
                });
                EnvUpdate();
            }
        })
    }


});

function AppButtonClicked() {
    var Application = '';
    if ($('#AppName').length) {
        Application = $('#AppName').val();
    } else {
        Application = $('#Team').val();
    }
    ShowRequestHistory(Application);
}


function ApplicationUpdate() {
    if ($('#RequestHistory').is(':visible')) {
        AppButtonClicked();
    }
    $('#AppButton').show(500);

}



function CreateDatePicker() {
    var env = $('#Environment').val();
    var dates = "";
    var highlight = "";
    if (env == "Production") {
        dates = "0,5,6";
        highlight = "1,2,3,4";
    } else {
        dates = "0,6";
        highlight = "1,2,3,4,5";
    }
    $('.datepicker').datepicker({
        daysOfWeekDisabled: dates,
        daysOfWeekHighlighted: highlight,
        startDate: new Date(),
        autoclose: true,
        useCurrent: true,
    }).on('changeDate', function (e) {
        DateChange();
    });
}

function EnvUpdate() {
    $('.datepicker').datepicker('remove');
    CreateDatePicker();
    var env = $('#Environment').val();
    $('#DBEnv').text(env);
    if (env == 'QA') {
        $('#QTSQuestions').hide(500);
        $('#QAQuestions').show(500);
        $('#QTSQuestionsDB').hide(500);
        $('#QAQuestionsDB').show(500);
    } else if (env == 'QTS' || env == 'Production') {
        $('#QAQuestions').hide(500);
        $('#QTSQuestions').show(500);
        $('#QAQuestionsDB').hide(500);
        $('#QTSQuestionsDB').show(500);
    }
}

function DateChange() {
    var day = new Date($('#DeployDateTimeNotNull').val());
    if (!(day.getYear() == '101')) {
        var date = day;
        day = day.getDay();
        if (day == 1 || day == 3 || day == 4 || day == 5) {
            var times = [];
            if (new Date().getDate() == date.getDate() && new Date().getMonth() == date.getMonth()) {
                if (new Date().getHours() < 9) {
                    times.push("9:00 AM");
                }
                if (new Date().getHours() < 14) {
                    times.push("2:00 PM");
                }

                if (times.length == 0) {
                    times.push("No time slots available");
                }
            } else {
                times = ["9:00 AM", "2:00 PM", ];
            }
            $("#DeployTime").html("")
            $.each(times, function (i, time) {
                $("#DeployTime").append(
                $('<option></option').val(time).html(time));
            });
        } else {
            var times = [];
            if (new Date().getDate == date.getDate() && new Date().getMonth() == date.getMonth()) {
                if (new Date().getTime() < new Date("October 13, 2014 9:00:00").getTime()) {
                    times.push("9:00 AM");
                }
                if (new Date().getTime() < new Date("October 13, 2014 14:00:00").getTime()) {
                    times.push("2:00 PM");
                }
                if (new Date().getTime() < new Date("October 13, 2014 21:00:00").getTime()) {
                    times.push("9:00 PM");
                }

                if (times.length == 0) {
                    times.push("No time slots available");
                }
            } else {
                times = ["9:00 AM", "2:00 PM", "9:00 PM"];
            }

            $("#DeployTime").html("")
            $.each(times, function (i, time) {
                $("#DeployTime").append(
                $('<option></option').val(time).html(time));
            });
        }
    } else {
        times = ["Please select a date"];
        $("#DeployTime").html("")
        $.each(times, function (i, time) {
            $("#DeployTime").append(
            $('<option></option').val(time).html(time));
        });
    }

}

function FillDropDowns() {
    $('#DateTime').show(500);
    var teamId = $('#Team').val();
    var userId = $('#User').val();
    $('#DBTeam').text(teamId)

    $.ajax({
        url: '/Teams/GetValidEnvironments',
        type: "GET",
        dataType: "JSON",
        data: { Team: teamId, User: userId },
        success: function (envs) {
            $("#Environment").html("");
            $.each(envs, function (i, env) {
                $("#Environment").append(
                $('<option></option').val(env.EnvName).html(env.EnvName));
            });
            EnvUpdate();
        }
    })

    $.ajax({
        url: '/Applications/FillApps',
        type: "GET",
        dataType: "JSON",
        data: { Team: teamId },
        success: function (apps) {
            $("#AppName").html(""); //clear old items
            $.each(apps, function (i, app) {
                $("#AppName").append(
                $('<option></option>').val(app.AppName).html(app.AppName));
            });
            if ($('#AppName').html() != '') {
                ApplicationUpdate();
            }
        }
    });

    $.ajax({
        url: '/Teams/GetEmail',
        type: "GET",
        dataType: "JSON",
        data: { Team: teamId },
        success: function (email) {
            $('#ConfirmationEmail').val(email);
        }
    });
    $.ajax({
        url: '/Teams/GetQAs',
        type: 'GET',
        dataType: 'JSON',
        data: { Team: teamId, User: userId },
        success: function (qas) {
            $('#QATester').html('');
            $.each(qas, function (i, qa) {
                $('#QATester').append(
                $('<option></option>').val(qa.ID).html(qa.Name));
            })
        }

    });




}

function QaYesCode() {
    var $section = $('#QAsection');
    $section.hide(500);
}

function QaNoCode() {
    var $section = $('#QAsection');
    $section.show(500);
}

function QaYesDb() {
    var $section = $('#QAsectionDB');
    $section.hide(500);
}

function QaNoDb() {
    var $section = $('#QAsectionDB');
    $section.show(500);
}

function QTSYesCode() {
    var $section = $('#QTSsection');
    $section.hide(500);
}

function QTSNoCode() {
    var $section = $('#QTSsection');
    $section.show(500);
}

function QTSYesDb() {
    var $section = $('#QTSsectionDB');
    $section.hide(500);
}

function QTSNoDb() {
    var $section = $('#QTSsectionDB');
    $section.show(500);
}
//function QACheck() {
//    var isChecked = $('#TestableInQA').val();
//    if (isChecked) {
//        //Checked
//        //Display QA Section
//    } else {
//        //Undisplay
//    }
//}

//function QACheck() {
//    var $section = $('#QAsection');
//    var $check = $('#TestableInQABool');
//    var height = $section.height();
//    var expanded = $('#TestableInQABool').is(':checked');
//    if (expanded) {
//        $section.show(500);

//    } else {
//        $section.hide(500);
//    }
//    //if (expanded) {
//    //    height -= 500;
//    //}
//    //else {
//    //    height += 500;
//    //}
//    //$section.data('expanded', !expanded);
//    //$section.animate({
//    //    height: height
//    //}, 1000);
//};