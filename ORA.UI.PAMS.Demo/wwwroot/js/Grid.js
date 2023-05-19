//onEditorPreparing
function onEditorPreparing(e) {
    onEditorPreparingSelection(e);
    onEditorPreparingComment(e);
}

function onEditorPreparingComment(e) {
    let dataGrid = e.component;
    if (e.parentType === "dataRow") {

        if (e.dataField === "Comment") {
            e.editorName = "dxHtmlEditor"; //"dxTextArea";

            e.editorOptions.toolbar = {
                items: [
                    'bold', 'italic', 'strike', 'underline',
                    'link', 'image'
                ]
            };

            e.editorOptions.imageUpload = {
                tabs: ['file', 'url'],
                fileUploadMode: 'base64'
            };

            e.editorOptions.mediaResizing = {
                enabled: true
            };
        }
    }
}

function FormatHtml() {
    $(".Comment").each(function () {
        let html = $(this).html();
        if (html != null && (html.includes("&lt;") || html.includes("&gt;"))) {
            $(this).html(_.unescape(html));
        }
    });
}

//Selection
let selectAllCheckBox;
let checkBoxUpdating = false;

function onEditorPreparingSelection(e) {
    let dataGrid = e.component;
    if (e.command === "select") {
        if (e.parentType === "dataRow" && e.row) {
            if (!isSelectable(e.row.data))
                e.editorOptions.disabled = true;
        } else if (e.parentType === "headerRow") {
            e.editorOptions.onInitialized = function (e) {
                selectAllCheckBox = e.component;
            }
            e.editorOptions.value = isSelectAll(dataGrid);
            e.editorOptions.onValueChanged = function (e) {
                if (!e.event) {
                    if (e.previousValue && !checkBoxUpdating) {
                        e.component.option("value", e.previousValue);
                    }
                    return;
                }
                if (isSelectAll(dataGrid) === e.value) {
                    return;
                }

                e.value ? dataGrid.selectAll() : dataGrid.deselectAll();

                e.event.preventDefault();
            }
        }
    }
}

function isSelectable(item) {
    return item.IsActive;
}

function isSelectAll(dataGrid) {
    let items = [];

    dataGrid.getDataSource().store().load().done(function (data) {
        items = data;
    });

    let selectableItems = items.filter(isSelectable);
    let selectedRowKeys = dataGrid.option("selectedRowKeys");

    if (!selectedRowKeys.length) {
        return false;
    }
    return selectedRowKeys.length >= selectableItems.length ? true : undefined;
}

function onSelectionChanged(e) {
    let deselectRowKeys = [];

    e.selectedRowsData.forEach((item) => {
        if (!isSelectable(item))
            deselectRowKeys.push(e.component.keyOf(item));
    });

    if (deselectRowKeys.length) {
        e.component.deselectRows(deselectRowKeys);
    }

    checkBoxUpdating = true;
    selectAllCheckBox.option("value", isSelectAll(e.component));
    checkBoxUpdating = false;
}

function DisplayAllSelectedData() {
    const gridView = DevExpress.ui.dxDataGrid.getInstance(document.getElementById('grid1'));
    let selectedRows = gridView.getSelectedRowsData();
    let dialogContent = "";
    selectedRows.forEach(function (row) {
        dialogContent += row.Id + "- " + row.UserName + "<br/>";
    });
    DevExpress.ui.dialog.alert(dialogContent, "");
}

//Save / cancel
let isSaveClick = false;

function isEditVisible(e) {
    return !e.row.isEditing;
}
function onEditClick(e) {
    e.component.editRow(e.row.rowIndex);
    e.event.preventDefault();
}

function isSaveVisible(e) {
    return e.row.isEditing;
}
function onSaveClick(e) {
    isSaveClick = false;
    ShowConfirmPopup("Confirm Saving", "Are you sure you want to save changes?", "Save", () => {
        isSaveClick = true;
        e.component.saveEditData().then(() => {
            isSaveClick = false;
        });
    });
    e.event.preventDefault();
}

function isCancelVisible(e) {
    return e.row.isEditing;
}
function onCancelClick(e) {
    isSaveClick = false;
    ShowConfirmPopup("Confirm Cancellation", "Are you sure you want to cancel?", "Cancel", () => {  
        e.component.cancelEditData();
    });
    e.event.preventDefault();
}

//ConfirmPopup
function ConfirmCancel() {
    isSaveClick = false;
    $('#ConfirmPopup').modal('hide');    
}
function ShowConfirmPopup(title, content, type, callback) {
    $("#ConfirmTitle").text(title);
    $("#ConfirmContent").text(content);
    $('#ConfirmPopup').modal('show');

    $(".ConfirmOK").hide();
    $("#ConfirmOK-" + type).show();

    $("#ConfirmOK-" + type).click(() => {
        callback();
        $('#ConfirmPopup').modal('hide');
    });
}

//rowValidating
function rowValidating(e) {
    console.log(isSaveClick);

    if (isSaveClick) {
        let changeData = Object.assign(e.oldData, e.newData);

        const request = $.ajax({
            url: '/api/SpecialNote/ValidateRow',
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(changeData),
        });

        request.done((json) => {
            if (json.isValid === false) {
                e.isValid = false;
                e.errorText = `Error: ${json.data}`;
            }
        });

        e.promise = request;
    }
    //else
    //    e.promise = new Promise(r => r(true));
}

//function validateComment(e) {
//    if (!isSaveClick) return new Promise(r => r(true));

//    return $.ajax({
//        url: '/api/SpecialNote/ValidateComment',
//        type: "GET",
//        dataType: "json",
//        contentType: "application/json",
//        data: {Comment: e.data.Comment }
//    });
//}
