
var GlobalTable = '';
Date.prototype.formatMMDDYYYY = function () {
    return (this.getMonth() + 1) +
    "/" + this.getDate() +
    "/" + this.getFullYear();
}
function HideHistoryButtonClicked() {
    $('#RequestHistory').hide(500);
}

function GenerateRequestHistory(currentForm) {

    var dataTableCols;
    var submittedTime = {
        "data": "SubmissionTime",
        "title": "Submitted",
        "render": function (data, type, full, meta) {
            var jsonDate = data;
            var re = /-?\d+/;
            var m = re.exec(jsonDate);
            var d = new Date(parseInt(m[0]));
            return d.format();
        }
    };
    if (currentForm == 'Deployment (App Only)') {
        dataTableCols = [
           submittedTime,

             {
                 "data": null,
                 "title": "Copy"
             },
             {
                 "data": "User",
                 "title": "User"
             },
             {
                 "data": "Team",
                 "title": "Team"
             },
             {
                 "data": "Environment",
                 "title": "Environment"
             },
             {
                 "data": "AppName",
                 "title": "Application"
             },
             {
                 "data": "JenkinsBuild",
                 "title": "Jenkins Build"
             },
             {
                 "data": "DINumber",
                 "title": "DI Number"
             },
              {
                  "data": "TFSWorkItems",
                  "title": "TFS Work Items"
              },
              {
                  "data": "SpecialNotes",
                  "title": "Special Notes"
              },

        ];
    } else if (currentForm == 'Deployment (SQL Only)') {
        dataTableCols = [
                submittedTime,

                 {
                     "data": null,
                     "title": "Copy"
                 },
                 {
                     "data": "User",
                     "title": "User"
                 },
                 {
                     "data": "Team",
                     "title": "Team"
                 },
                 {
                     "data": "Environment",
                     "title": "Environment"
                 },
                 {
                     "data": "DatabaseName",
                     "title": "Database title"
                 },
                 {
                     "data": "CodeLocation",
                     "title": "Code Location"
                 },
                 {
                     "data": "Changesets",
                     "title": "Changesets"
                 },
                 {
                     "data": "TFSWorkItems",
                     "title": "TFS Work Items SQL"
                 },
                 {
                     "data": "DataScriptNames",
                     "title": "Data Script Names"
                 }, ];
    } else if (currentForm == 'Deployment (App + SQL)') {
        dataTableCols = [
            submittedTime,

             {
                 "data": null,
                 "title": "Copy"
             },
            {
                "data": "User",
                "title": "User"
            },
             {
                 "data": "Team",
                 "title": "Team"
             },
             {
                 "data": "Environment",
                 "title": "Environment"
             },
             {
                 "data": "AppName",
                 "title": "Application"
             },
             {
                 "data": "JenkinsBuild",
                 "title": "Jenkins Build"
             },
             {
                 "data": "DINumber",
                 "title": "DI Number"
             },
              {
                  "data": "TFSWorkItems",
                  "title": "TFS Work Items"
              },
               {
                   "data": "SpecialNotes",
                   "title": "Special Notes"
               },
                  {
                      "data": "DatabaseName",
                      "title": "Database title"
                  },
                 {
                     "data": "CodeLocation",
                     "title": "Code Location"
                 },
                 {
                     "data": "Changesets",
                     "title": "Changesets"
                 },
                 {
                     "data": "TFSWorkItemsDB",
                     "title": "TFS Work Items SQL"
                 },
                 {
                     "data": "DataScriptNames",
                     "title": "Data Script Names"
                 },
        ];
    }
    //code
    var Table = $('#data').DataTable({
        "scrollX": true,
        "responsive": true,
        "autoWidth": true,
        "lengthMenu": [[100, 250, 500, -1], [100, 250, 500, "ALL"]],
        "ajax": {
            "url": "/Request/GetRequests/?AppName=Test&Type=Code",
            "dataSrc": ""
        },
        "order": [[0, "desc"]],
        "columns": dataTableCols,
        "columnDefs": [
                    { targets: '_all', width: "3%" },
                    {
                        "targets": 1,
                        "data": null,
                        "defaultContent": "<button class='btn btn-mygrey btn-sm'>Copy Request</button>"
                    }],
        "oLanguage": {
            "sEmptyTable": "No requests have been made yet."
        }
    });

    $('#data tbody').on('click', 'button', function () {
        var rowNumber = Table.row($(this).parents('tr')).index();
        CopyDataFromHistory(rowNumber);
    });
    GlobalTable = Table;
    GlobalTable.columns.adjust().draw();
}
$(document).ready(function () {


});

function CopyDataFromHistory(rowNum) {
    var data = GlobalTable.row(rowNum).data();
    var currentForm = $('#FormSelectDropDown').val();
    if (currentForm == 'Deployment (App Only)') {
        CopyCodeData(data);
    } else if (currentForm == 'Deployment (SQL Only)') {
        CopyDBData(data);
        $('#TFSWorkItems').val(data['TFSWorkItems']);
    } else if (currentForm == 'Deployment (App + SQL)') {
        CopyCodeData(data);
        CopyDBData(data);
        $('#TFSWorkItemsDB').val(data['TFSWorkItemsDB']);
    }
    $('#CopySuccess').html('<div class="alert alert-success alert-dismissible" role="alert"><button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>Request fields copied successfully!</div>');

}

function CopyCodeData(data) {
    $('#JenkinsBuild').val(data['JenkinsBuild']);
    $('#DINumber').val(data['DINumber']);
    $('#TFSWorkItems').val(data['TFSWorkItems']);
    $('#SpecialNotes').val(data['SpecialNotes']);
}

function CopyDBData(data) {
    $('#CodeLocation').val(data['CodeLocation']);
    $('#Changesets').val(data['Changesets']);
    $('#DataScriptNames').val(data['DataScriptNames']);
}

function ShowRequestHistory(Application) {

    $('#RequestHistory').show();
    GlobalTable.columns.adjust().draw();
    $('.table').css("width", "100%");
    $('#RequestHistoryHeadingPrefix').html('');
    $('#RequestHistoryHeadingPrefix').html(Application);
    var ajaxUrl = '/Request/GetRequests/?'
    var currentForm = $('#FormSelectDropDown').val();
    var formType = '';
    if (currentForm == 'Deployment (App Only)') {
        formType = 'Code';
    } else if (currentForm == 'Deployment (SQL Only)') {
        formType = 'DB';
    } else if (currentForm == 'Deployment (App + SQL)') {
        formType = 'Full';
    }
    ajaxUrl = ajaxUrl + 'AppName=' + Application + '&Type=' + formType;

    GlobalTable.ajax.url(ajaxUrl).load();
    GlobalTable.draw();

}