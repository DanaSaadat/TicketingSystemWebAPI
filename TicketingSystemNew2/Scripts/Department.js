
    $(document).ready(function () {
        loadData();
       

      
    });


function loadData1()
{
    debugger;
    $.ajax({
        url: "/Department/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            var html = '';
            $.each(result, function (key, item)
            {

                html += '<tr>';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.Name + '</td>';

                html += '<td><a href="#" class="btn btn-primary" onclick="return getbyID(' + item.ID + ')">Edit</a>  <a class="btn btn-danger" href="#" onclick="Delele(' + item.ID + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
           
            //$("#myTable1").DataTable(html);
            $("#myTable1").DataTable();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
            
        }
    });
}

function loadData() {
    debugger;
    $.ajax({
        url: "/Department/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            var html = '';
            $.each(result.cc, function (key, item) {

                html += '<tr>';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.Name + '</td>';

                html += '<td><a href="#" class="btn btn-primary" onclick="return getbyID(' + item.ID + ')">Edit</a>  <a class="btn btn-danger" href="#" onclick="Delele(' + item.ID + ')">Delete</a></td>';
                html += '</tr>';
            });

            $('.tbody').html(html);

            //$("#myTable1").DataTable(html);
            $("#myTable1").DataTable();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);

        }
    });
}

function clearTextBox() {

    $('#testt').addClass('hidden');
    $('#id').val("");
    $('#Name').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    /* $('#ManagerID').hide();*/
    $('#Name').css('border-color', 'lightgrey');

}


function Add() {

    var res = validate();
    if (res == false) {
        return false;
    }
   

    //var res1 = checkadd();
    //debugger;

    //alert(res1);
    //if (res1 == true) {
    //    return false;
    //}
/*    if (res1 == false) {*/
        // if (checkadd()) {
        //alert("ddanaa");

        var empObj = {
            ID: $('#id').val(),
            Name: $('#Name').val(),
        };
    //var ddd = false;
        $.ajax({
            url: "/Department/Add",
            data: JSON.stringify(empObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
              /*  ddd = result;*/
                debugger;
                if (result.IsSuccess == true) {


                    alert("save successfully");
                    loadData();
                    $('#myModal').modal('hide');
                    $('#id').val("");
                    $('#Name').val("");
                }
                else {
                    alert(result.Error);
                   /* alert("sdsds");*/
                    $('#myModal').modal('hide');
                //    throw result.Error;
                }
            },
            error: function (errormessage)
            {
                //alert(errormessage);
               
                alert(errormessage.responseText);
                //alert("dana saadat");
              
            }
        });
    }
/*}*/

function checkadd() {


    var empObj = {
        ID: $('#id').val(),
        Name: $('#Name').val(),
    };

    $.ajax({
        type: "POST",
        url: "/Department/checkDepartmentName",
        data: JSON.stringify(empObj),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            /* alert(result);*/
            if (result) {
                return true;

                alert("department name already exists");
                $('#myModal').modal('hide');
                loadData();
            }
            else {
                return false;

            }

        },
        error: function (error) { return false; alert(error); }

    });
}
function validate() {
    var isValid1 = true;

    if ($('#Name').val().trim() == "") {
        $('#Name').css('border-color', 'Red');
        isValid1 = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }


    return isValid1;
}


function getbyID(Id) {
    debugger;
    $('#testt').removeClass('hidden');
    filldrop(Id);
    //$('#ManagerID  option[value="0"]').prop("selected", true);


    //$('select[name="ManagerID"]')
    //    .children('option[value ="0"]') //--> will get option value "0" element 
    //    .text('Please select a Manager');
    //$.ajax({
    //    type: "POST",
    //    url: "/Department/getManager/" + Id,

    //    success: function (data) {
    //        var s = '<option value="-1">Please Select a Manager</option>';
    //        for (var i = 0; i < data.length; i++) {
    //            s += '<option value="' + data[i].UserID + '">' + data[i].UserName + '</option>';
    //        }
    //        $("#ManagerID").html(s);
    //    }
    //});  

    $('#Name').css('border-color', 'lightgrey');


    $.ajax({
        url: "/Department/getbyID/" + Id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#id').val(result.ID);
            $('#Name').val(result.Name);
            $('#ManagerID').val(result.ManagerID);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}


function filldrop(Id) {

    $.ajax({
        type: "POST",
        url: "/Department/getManager/" + Id,
        success: function (data) {
            var s = '<option selected="true" value="0"> Select Manager</option>';
            //var s = '<option  value="0" > Select Manager</option>';

            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].UserID + '">' + data[i].UserName + '</option>';
            }
            $("#ManagerID").html(s);
        }
    });
}




function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        ID: $('#id').val(),
        Name: $('#Name').val(),
        ManagerID: $('#ManagerID').val()

    };
    $.ajax({
        url: "/Department/Update",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            alert("Updated successfully");
            $('#myModal').modal('hide');
            $('#id').val("");
            $('#Name').val("");
            $('#ManagerID').val("");

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Record?" + ID);
    if (ans) {
        $.ajax({
            url: "/Department/Delete2/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {

                alert("deleted successfully");
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}