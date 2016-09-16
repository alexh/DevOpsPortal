$('#FormSelectDropDown').change(function () {
    var loadingHtmlString = '<span id="loading-container"> <img id="loading" src="/images/icons/ApertureScience.gif"/> </span> <h4 align="center"> Loading...</h4>';

    $('#partialPlaceHolder').html(loadingHtmlString);
    /* little fade in effect */
    $('#partialPlaceHolder').fadeIn(500);

    /* Get the selected value of dropdownlist */
    var selectedString = $(this).val();
    var selectedID = this.selectedIndex;
    var urlString = '/Form/';

    switch (selectedID) {
        case 1:
            urlString = urlString + 'CodeDeployRequest'
            break;
        case 2:
            urlString = urlString + 'DbDeployRequest'
            break;
        case 3:
            urlString = urlString + 'FullRequest'
            break;
        case 4:
            urlString = urlString + 'General'
            break;
        default:
            urlString = '#';
            break;
    }
    if (urlString != '#') {
        urlString = urlString + '/';
    }

    /* Request the partial view with .get request. */
    $.get(urlString, function (data) {
        /* data is the pure html returned from action method, load it to your page */
        $('#partialPlaceHolder').html(data);
        /* little fade in effect */
        $('#partialPlaceHolder').fadeIn('fast');

        CreateDatePicker();
    }
    );
    if (GlobalTable != '') {
        $('#RequestHistory').hide(300);
        GlobalTable.destroy();
    }
    GenerateRequestHistory(selectedString);
});