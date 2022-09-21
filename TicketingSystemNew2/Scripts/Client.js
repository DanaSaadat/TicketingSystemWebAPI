$(document).ready(function () {
    loadData();

    $('#UserName').change(function () {

        if ($(this).val() != "" || $(this).val() != null)
        {
            $(this).css('border-color', 'lightgrey');
            $(this).next('div').remove();

           
        }
        else {
            $('#UserName').css('border-color', 'Red');
            $('#UserName').after('<div class="red">UserName is Required</div > ');
        }

    });


    $('#Password').change(function () {

        if ($(this).val() != "" || $(this).val() != null) {
            $(this).css('border-color', 'lightgrey');
            $(this).next('div').remove();


        }
        else {
            $('#UserName').css('border-color', 'Red');
            $('#UserName').after('<div class="red">Password is Required</div > ');
        }
    });

});
function loadData()
{

    $.ajax({
        url: "/Client/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            var html = '';
            $.each(result, function (key, item) {

                html += '<tr>';
                html += '<td>' + item.UserID + '</td>';
                html += '<td>' + item.UserName + '</td>';
                //html += '<td>' + item.Password + '</td>';
                html += '<td>' + item.FirstName + '</td>';
                html += '<td>' + item.LastName + '</td>';
                html += '<td>' + item.Email + '</td>';
                html += '<td>' + item.Mobile + '</td>';

                html += '<td><a href="#" class="btn btn-primary" onclick="return getbyID(' + item.UserID + ')">Edit</a>  <a class="btn btn-danger" href="#" onclick="Delele(' + item.UserID + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


function clearTextBox()
{
    $("#myModalLabel").text("Add Client");

   /* filldrop();*/
    $('#UserID').val("");
    $('#UserName').val("");
    $('#Password').val("");
    $('#FirstName').val("");
    $('#LastName').val("");
    $('#Email').val("");
    $('#Mobile').val("");
    //$("#PermissionUser").empty();
    $("#PermissionUser option").prop("selected", false);
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Name').css('border-color', 'lightgrey');
    $('#UserName').next('div').remove();
    $('#Password').next('div').remove();
}

function Add() {
    debugger;
    var res = validate();
    if (res == false) {
        return false;
    }
        var optionsVal = [];
        //$("#Permission option:selected").each(function () {
        $("#PermissionUser option:selected").each(function () {
            optionsVal.push($(this).val());
        });
   

        var empObj = {
            UserID: $('#UserID').val(),
            UserName: $('#UserName').val(),
            Password: $('#Password').val(),
            FirstName: $('#FirstName').val(),
            LastName: $('#LastName').val(),
            Email: $('#Email').val(),
            Mobile: $('#Mobile').val(),
            //Permission: $('#Permission').val(),
            Permission: optionsVal

        };

        $.ajax({
            url: "/Client/Add",
            data: JSON.stringify(empObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                debugger;
                if (result.IsSuccess == true)
                {


                    //alert("save successfully");
                    swal("save successfully", {
                        icon: "success"
                    });
                    loadData();
                    $('#myModal').modal('hide');
                  
                }
                else {
                    alert(result.Error);
                    $('#myModal').modal('hide');

                }
            },
            error: function (errormessage) {

                alert(errormessage.responseText);
            }
        });
    }
      

function validate() {

    debugger;
    var isValid1 = true;

    if ($('#UserName').val().trim() == "") {
        $('#UserName').after('<div class="red">UserName is Required</div>');

        $('#UserName').css('border-color', 'Red');
        isValid1 = false;
    }
    else {
        $('#UserName').css('border-color', 'lightgrey');
        $('#UserName').after('<div class="red"> </div>');
    }



    if ($('#Password').val().trim() == "")
    {
        $('#Password').after('<div class="red">Password is Required</div>');
        $('#Password').css('border-color', 'Red');
        isValid1 = false;
    }
    else
    {
        $('#Password').css('border-color', 'lightgrey');
    }

    return isValid1;
}


function filldrop() {

    $.ajax({
        type: "POST",
        url: "/Client/getPermission" ,
        success: function (data) {
            var s = '<option selected="true" value="0"> Select permission</option>';

            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].ID + '">' + data[i].Name + '</option>';
            }
            $("#Permission").html(s);
        }
    });
}


function getbyID(Id) {

    $("#myModalLabel").text("Edit Client");
    //$("#PermissionUser").trigger("change");

    $('#UserName').next('div').remove();
    $('#Password').next('div').remove();
    //var res = validate();
    //if (res == false) {
    //    return false;
    //}


    filldrop2(Id);

    $.ajax({
        url: "/Client/getbyID/" + Id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result)
        {
            $('#UserID').val(result.UserID);
            $('#UserName').val(result.UserName);
            $('#Password').val(result.Password);
            $('#FirstName').val(result.FirstName);
            $('#LastName').val(result.LastName);
            $('#Email').val(result.Email);
            $('#Mobile').val(result.Mobile);
          
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();

            $('#UserName').css('border-color', 'lightgrey');
            //$('#UserName').after('<div class="hidden"> </div>');
            $('#UserName').next('div').remove();
            $('#Password').next('div').remove();

            $('#Password').css('border-color', 'lightgrey');
            //if ($('#myModal').is(':visible')) {

            //}
            //else {
            //    location.reload();
            //}
            

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
function filldrop2(Id) {

    /*unselect();*/

    $.ajax({
        type: "POST",
        url: "/Client/getPermissionUser/" + Id,
        success: function (data)
        {

            $("#PermissionUser").val(data);

          
            $.each(data, function (i, e) {
                /*alert(e.ID);*/
                $("#PermissionUser option[value='" + e.ID + "']").prop("selected", true);

            //    $("#PermissionUser option[value='" + e.ID + "']").val(e.ID).trigger('change');
            });

            //var options = '';

            //for (var i = 0; i < data.length; i++) {
            //    options += '<option value="' + data[i].ID + '">' + data[i].Name + '</option>';

            //}

           
            //$('#PermissionUser').html(options);




           
        }
    });
}


function unselect()
{
    $.each($("#PermissionUser option:selected"), function () {
        $(this).prop('selected', false); 
    });
}

function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var optionsVal = [];
    //$("#Permission option:selected").each(function () {
    $("#PermissionUser option:selected").each(function () {
        optionsVal.push($(this).val());
    });


    var empObj = {
        UserID: $('#UserID').val(),
        UserName: $('#UserName').val(),
        Password: $('#Password').val(),
        FirstName: $('#FirstName').val(),
        LastName: $('#LastName').val(),
        Email: $('#Email').val(),
        Mobile: $('#Mobile').val(),
        Permission: optionsVal

    };
    $.ajax({
        url: "/Client/Update",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            //alert("Updated successfully");


            swal("Updated successfully", {
                icon: "success"
            });
            $('#myModal').modal('hide');
            //$('#id').val("");
            //$('#Name').val("");
            //$('#ManagerID').val("");

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function Delele(ID) {

    debugger;
    //var ans = confirm("Are you sure you want to delete this Record?" + ID);
    var ans = confirm(ID);
    //if (ans) {
    //    $.ajax({
    //        url: "/Client/Delete2/" + ID,
    //        type: "POST",
    //        contentType: "application/json;charset=UTF-8",
    //        dataType: "json",
    //        success: function (result) {

    //            alert("deleted successfully");
    //            loadData();
    //        },
    //        error: function (errormessage) {
    //            alert(errormessage.responseText);
    //        }
    //    });
   // }
}


function confirm(ID)
{
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this Record!" + ID,
        icon: "warning",
      /*  buttons: true,*/
        buttons: ["No, cancel please!", "Yes!"],
        showCancelButton: true,
        //confirmButtonText: "Yes, archive it!",
        //cancelButtonText: "No, cancel please!",
        dangerMode: true,
        closeOnConfirm: false,
        closeOnCancel: false
    }).then(function (isConfirm) {

      


        if (isConfirm) {

            $.ajax({
                url: "/Client/Delete2/" + ID,
                type: "POST",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                success: function (result) {
                    swal("Your record  has been deleted!", {
                        icon: "success"
                    });
                   // alert("deleted successfully");
                    loadData();
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            });


            //swal("Poof! Your imaginary file has been deleted!", {
            //    icon: "success"
            //});
        } else {
          /*  swal("Your Record is safe!");*/
        }
    });

    return false;
}