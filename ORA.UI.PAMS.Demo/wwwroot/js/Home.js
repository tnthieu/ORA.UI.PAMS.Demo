window.addEventListener("resize", onTabChanged);

function onTabChanged(e) {
    let tab = 1;

    if (e != null && e.component != null)
        tab = e.component.option("selectedIndex") + 1;

    ResizeTabPanel(tab);
}

function ResizeTabPanel(tab) {
    let width = $("#grid-container" + tab + " .dx-datagrid-table").width();
    if (width < $(window).width())
        width = $(window).width();

    $("#tabpanel-container").width(width);
}

function LoadGrid() {
    let postBackURL = "/Home/Grid";

    $.ajax({
        url: postBackURL,
        method: 'GET'
    })
        .done(function (data) {
            $("#grid-container2").html(data);
            return false;
        })
        .fail(function () {
            alert('Postback failed.');
        })
}