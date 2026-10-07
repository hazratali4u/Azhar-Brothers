$("document").ready(function () {
/*
    setTimeout(function () {
        var id = '#dialog';
        //Get the screen height and width
        var maskHeight = $(document).height();
        var maskWidth = $(window).width();
        //Set heigth and width to mask to fill up the whole screen
        $('#mask').css({ 'width': maskWidth, 'height': maskHeight });
        //transition effect
        $('#mask').fadeIn(500);
        $('#mask').fadeTo("slow", 0.9);
        //Get the window height and width
        var winH = $(window).height();
        var winW = $(window).width();
        //Set the popup window to center
        $(id).css('top', winH / 2 - $(id).height() / 2);
        $(id).css('left', winW / 2 - $(id).width() / 2);
        //transition effect
        $(id).fadeIn(2000);
        //if close button is clicked
        $('.window .close').click(function (e) {
            //Cancel the link behavior
            e.preventDefault();
            $('#mask').hide();
            $('.window').hide();
        });
        //if mask is clicked
        $('#mask').click(function () {
            $(this).hide();
            $('.window').hide();
        });        
    }, 10);
    */
});
function pageLoad() {
    GetSessionData();
    LoadCustomerData();
    //LoadSKUDetail();
    LoadPendingOrder();

    $('#drpDocumentNo').change(function () {
        var selectedVal = $('#drpDocumentNo option:selected').attr('value');
        //alert(selectedVal);
    });

    $("[id*=btnSave]").click(function () {
        
        AddRecord();
    });

    $("select").searchable();
}

function GetSessionData() {
    jQuery.ajax({
        type: "GET",
        url: "HttpHandlers/OrderEntryHandler.ashx",
        data: "MethodName=GetSessionData",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            UserSessionData(data);
        }
    });
}

function UserSessionData(data) {
    $(jQuery.parseJSON(JSON.stringify(data))).each(function () {        
        $("#txtprincipal").val(this.Route);
        $("#txtDeliveryMan").val(this.SaleMan);
        if(this.OrderNo == '1')
        {
            //alert('OrderNo is 1')
        }
        else
        {
            //$('#ChbDiscount').show();
        }
        $("#ctl00_ctl00_mainCopy_cphPage_lblOrderDate").text(this.OrderDate);        
    });
}

function LoadCustomerData() {
    jQuery.ajax({
        type: "GET",
        url: "HttpHandlers/OrderEntryHandler.ashx",
        data: "MethodName=LoadCustomerData",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            PopulatedrpCustomer(data);
        }
    });
}
function PopulatedrpCustomer(data) {
    var drpCustomer = $("[id*=drpCustomer]");
    $(jQuery.parseJSON(JSON.stringify(data))).each(function () {        
        drpCustomer.append($("<option></option>").val(this['CUSTOMER_ID']).html(this['CUSTOMER_DETAIL2']));
    });
}

function LoadSKUDetail() {
    jQuery.ajax({
        type: "GET",
        url: "HttpHandlers/OrderEntryHandler.ashx",
        data: "MethodName=LoadSKUDetail",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            PopulateddlSKuCde(data);
        }
    });
}
function PopulateddlSKuCde(data) {
    var ddlSKuCde = $("[id*=ddlSKuCde]");
    $(jQuery.parseJSON(JSON.stringify(data))).each(function () {        
        ddlSKuCde.append($("<option></option>").val(this['Sku_ID']).html(this['SkuPriceDetail2']));
    });
}

function LoadPendingOrder() {
    jQuery.ajax({
        type: "GET",
        url: "HttpHandlers/OrderEntryHandler.ashx",
        data: "MethodName=LoadPendingOrder",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            PopulatedrpDocumentNo(data);
        }
    });
}
function PopulatedrpDocumentNo(data) {
    var drpDocumentNo = $("[id*=drpDocumentNo]");
    drpDocumentNo.empty().append('<option selected="selected" value="0">New</option>');
    $(jQuery.parseJSON(JSON.stringify(data))).each(function () {
        var SALE_INVOICE_ID = this.SALE_INVOICE_ID;        
        drpDocumentNo.append($("<option></option>").val(this['SALE_INVOICE_ID']).html(this['SALE_INVOICE_ID2']));        
    });

    $('#mask').trigger('click');
}
function AddRecord() {
    var row = $("[id*=GrdPurchase] tr:last-child").clone(true);
    $("[id*=GrdPurchase] tr").not($("[id*=GrdPurchase] tr:first-child")).remove();
    $("td", row).eq(0).text("1");
    $("td", row).eq(1).text("1");
    $("[id*=GrdPurchase]").append(row);
    row = $("[id*=GrdPurchase] tr:last-child").clone(true);
    alert($("td", row).eq(1).text());
}