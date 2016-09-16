$(document).ready(function () {
    $('#data').DataTable({
        responsive: true,
        "lengthMenu": [[-1, 100, 250, 500], ["All", 100, 250, 500]],
        "ajax": {
            "url": "/Deployments/GetDeploymentStatusData/",
            "dataSrc": "",
        },

        dom: 'Bfrtip',

        "columns": [

             {
                 data: 'ProjectName',
                 render: function (data, type, full, meta) {
                     return '<a  href="../Applications/Index/?p=' + data + '">' + data + '</a>';
                 }
             },
             {
                 data: 'DevReleaseDate',
                 "render": function (data, type, row) {
                     if (data == "12/31/1999 7:00:00 PM") {
                         return "";
                     } else {
                         return row["DevRelease"] + "<hr id=\"bar\"/>" + data;
                     }
                 }, "orderable": false,

                 //Switch to true when dev env is added to octopus
                 visible: false
             },
             //{ data: 'DevRelease', "visible": false },
             {
                 data: 'QAReleaseDate',
                 "render": function (data, type, row) {
                     if (data == "12/31/1999 7:00:00 PM") {
                         return "";
                     } else {
                         return row["QARelease"] + "<hr id=\"bar\"/>" + data;
                     }
                 }, "orderable": false
             },
             //{ data: 'QARelease', "visible": false },
              {
                  data: 'QTSReleaseDate',
                  "render": function (data, type, row) {
                      if (data == "12/31/1999 7:00:00 PM") {
                          return "";
                      } else {
                          return row["QTSRelease"] + "<hr id=\"bar\"/>" + data;
                      }
                  }, "orderable": false
              },
             { data: 'QTSRelease', "visible": false },
              {
                  data: 'BetaReleaseDate',
                  "render": function (data, type, row) {
                      if (data == "12/31/1999 7:00:00 PM") {
                          return "";
                      } else {
                          return row["BetaRelease"] + "<hr id=\"bar\"/>" + data;
                      }
                  }, "orderable": false
              },
             //{ data: 'BetaRelease', "visible": false },
              {
                  data: 'ProductionReleaseDate',
                  "render": function (data, type, row) {
                      if (data == "12/31/1999 7:00:00 PM") {
                          return "";
                      } else {
                          return row["ProductionRelease"] + "<hr id=\"bar\"/>" + data;
                      }
                  }, "orderable": false
              },
             { data: 'ProductionRelease', "visible": false }

        ],
        "buttons": [
          'pageLength',
          {
              extend: 'collection',
              text: 'Column Toggle',
              buttons: ['columnsToggle']
          },
          {
              extend: 'collection',
              text: 'Export',
              buttons: ['copy', 'excel', 'pdf']
          },
           //{
           //    text: 'QTS and Prod are Different',
           //    action: function (e, dt, node, config) {
           //        config.counter++;
           //        if (config.state == 0) {
           //            this.text('All Applications');
           //            //dt.ajax.url('/Deployments/GetDeploymentStatusData/').load();
           //            config.state = 1;
           //        } else {
           //            this.text('QTS and Prod are Different')
           //            //dt.ajax.url('/Deployments/GetDeploymentStatusDiffs/').load();
           //            var prodVal = dt.column(7).data();
           //            dt.column(4).data().filter(function (value, index) {
           //            })
           //            config.state = 0;
           //        }
           //    },
           //    counter: 1,
           //    state: 0
           //},

        ],
    });
});