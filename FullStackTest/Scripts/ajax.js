$(document).ready(function () {
    $("#btnAdd").click(function () {
        var firstName = $("#firstName").val();
        var lastName = $("#lastName").val();
        var email = $("#email").val();

        if (firstName == "" && lastName == "" && email == "") {
            alert("At least one column must contain a value!");
            return false;
        }

        $.ajax({
            type: "POST",
            url: '/Customers/Create',
            data: JSON.stringify({ FirstName: firstName, LastName: lastName, Email: email }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result == "Invalid email!") {
                    alert(result);
                    return false;
                }

                $("#custTBody").prepend("<tr><td>" +
                    "<input class='disabled form-firstName form-control' type='text' name='firstName' value='" + firstName + "' disabled/></td><td>" +
                    "<input class='disabled form-lastName form-control' type='text' name='lastName' value='" + lastName + "' disabled/></td><td>" +
                    "<input class='disabled form-email form-control' type='email' name='email' value='" + email + "' disabled/></td><td>" +
                    "<button class='btn-edit btn btn-default' type='button' value='" + result.CustomerID + "'>Edit" + "</button>" +
                    "<button class='btn-save btn btn-default' type='button' value='" + result.CustomerID + "'>Save" + "</button> " +
                    "<button class='btn-delete btn btn-default' type='button' value='" + result.CustomerID + "'>Delete" + "</button> ");
                $("#firstName").val("");
                $("#lastName").val("");
                $("#email").val("");
                alert("Insertion complete!");
            },
            error: function (result) {
                alert("Error adding customer!");
            }
        });
        return false;
    });

    $("#custTBody").on("click", "button.btn-edit", function () {
        $(this).closest('tr').find('.disabled').prop("disabled", false);
        $(this).closest('tr').find('.btn-edit').hide();
        $(this).closest('tr').find('.btn-save').show();
    });

    $("#custTBody").on("click", "button.btn-save", function () {
        var customerID = $(this).val();
        var firstName = $(this).closest('tr').find('.disabled.form-firstName').val();
        var lastName = $(this).closest('tr').find('.disabled.form-lastName').val();
        var email = $(this).closest('tr').find('.disabled.form-email').val();

        $(this).closest('tr').find('.disabled').prop("disabled", true);
        $(this).closest('tr').find('.btn-edit').show();
        $(this).closest('tr').find('.btn-save').hide();

        $.ajax({
            type: "POST",
            url: '/Customers/Edit',
            data: JSON.stringify({ CustomerID: customerID, FirstName: firstName, LastName: lastName, Email: email }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                alert(result)
            },
            error: function (result) {
                alert(result);
            }
        });
        return false;
    });

    $("#custTBody").on("click", "button.btn-delete", function () {
        var customerID = $(this).val();
        var trElement = $(this).closest('tr');

        $.ajax({
            type: "POST",
            url: '/Customers/Delete',
            data: JSON.stringify({ CustomerID: customerID }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                alert(result);
                trElement.remove();
            },
            error: function (result) {
                alert(result);
            }
        });
        return false;
    });
});