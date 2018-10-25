
$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "AppraisalObjectives.aspx/GetObjectives",
        async: false,
       // data: JSON.stringify({ '_employeeOffDay': _object }), // Check this call.
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var obj = JSON.parse(response.d);
            var str = "", selctedval="";
          
            for (var i = 0; i < obj.datatb.length; i++) {
                switch (obj.datatb[i].ObjectiveType) {
                    case "Rating5":
                        selctedval = "Rating 1-5";
                        break;
                    case "Rating10":
                        selctedval = "Rating 1-10";
                        break;
                    case "YESorNO":
                        selctedval = "YES or NO";
                        break;
                    case "Percentage":
                        selctedval = "Percentage";
                        break;
                    case "PFGVE":
                        selctedval = "PFGVE";
                        break;
                    default:

                }
                str += "<tr><td>" + obj.datatb[i].CategoryName + "</td><td> " + obj.datatb[i].ObjectiveName + " </td><td> " + selctedval + " </td><td hidden='true' style='display:none;'>" + obj.datatb[i].Id + "</td>";
                str += "<td class='text-center'><a class='edit' href='javascript:;'><i class='fa fa-pencil'></i>";
                str += "</a></td><td class='text-center'><a class='delete' href='javascript:;'> ";
                str += "<i class='fa fa-trash'></i></a> </td></tr>";
                
            }
            $(tbvalues).html(str);
            
            

        },
        failure: function (response) {
            alert(response.d);
        }


    });



});
