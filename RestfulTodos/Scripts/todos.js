var Todos;

    var GetTodos = function () {
        //GET/READ 
        return $.ajax('/api/Todos/GetTodos', {
            contentType: 'application/json; charset=utf-8',
            success: function (todos) {
                var tbodyEl = $('tbody');
                tbodyEl.html('');
                $.each(todos, function (i, todo) {
                    var checked = '';
                    if (todo.isCompleted)
                        checked = 'checked';
                    else
                        checked = '';
                    tbodyEl.append('\
                        <tr>\
                        	<td><input id="completed" type="checkbox" value="' + todo.id + '" class="myCheckbox" ' + checked + '/></td>\
                            <td class="id">' + todo.id + '</td>\
                            <td><label class="name">'+todo.text+'</label></td>\
                            <td>\
                                <button class="btn btn-warning" onclick="return getbyID(' + todo.id + ')">Edit</button>\
                                <button id="deleteBtn" class="btn btn-danger">Delete</button>\
                            </td>\
                        </tr>\
                    ');
                });
            }
        });
    };


var AddTodo = function(event) {
    event.preventDefault();

    var createInput = $('#create-input');

    return $.ajax({
        url: '/api/Todos/AddTodo',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ text: createInput.val(), isCompleted: false }),
        success: function(response) {
            console.log(response);
            createInput.val('');
            GetTodos();
        }
    });
};

//Function for getting the Data Based upon Employee ID
function getbyID(todoId) {
    $('#Name').css('border-color', 'lightgrey');
    $.ajax({
        url: "/api/Todos/GetTodo/" + todoId,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#TodoId').val(result.id);
            $('#Name').val(result.text);
            $("#Completed").val(result.isCompleted);
            $('#myModal').modal('show');

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

var EditTodo = function (todoId, newText, isCompleted) {
    
    
    return $.ajax({
        url: '/api/Todos/UpdateTodo/' + todoId,
        type: 'PUT',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ id: todoId, text: newText, isCompleted: isCompleted}),
        success: function (result) {
            GetTodos();
            console.log(result);
            $('#myModal').modal('hide');
            $('#TodoId').val("");
            $('#Name').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
};

var updateStatus = function (id, text, isCompleted) {
   
    return $.ajax({
        url: '/api/Todos/UpdateTodo/' + id,
        type: 'PUT',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ id: id, text: text, isCompleted: isCompleted }),
        success: function (response) {
            console.log(response);
            GetTodos();
        }
    });
}

// DELETE
var RemoveTodo = function(id) {

    swal({
            title: "Are you sure?",
            text: "You will not be able to recover this todo!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: false
        },
        function(isConfirm) {
            if (isConfirm) {
                swal({
                        title: 'Deleted!',
                        text: 'Todo is Deleted!',
                        type: 'success'
                    },
                    function() {
                        deleteTodo(id);
                    });

            } else {
                swal("Cancelled", "Your todo is safe :)", "error");
            }
        });


};

var deleteTodo = function(id) {
    return $.ajax({
        url: '/api/Todos/DeleteTodo/' + id,
        type: 'DELETE',
        contentType: 'application/json; charset=utf-8',
        success: function(response) {
            console.log(response);
            GetTodos();
        }
    });
};

var WireEvents = function () {
    
    $('#btnUpdate').on('click', function () {
        var todoId = $('#TodoId').val();
        var newText = $('#Name').val();
        var isCompleted = $("#Completed").val();

        EditTodo(todoId, newText, isCompleted);
    });
    $('table').on('click', '#deleteBtn', function () {
        var rowEl = $(this).closest('tr');
        var id = rowEl.find('.id').text();

        RemoveTodo(id);
    });

    $('table').on('change', '.myCheckbox', function () {
        var rowEl = $(this).closest('tr');
        var id = rowEl.find('.id').text();
        var text = rowEl.find('.name').text();
        var isCompleted = rowEl.find('.myCheckbox');

        if (isCompleted.prop('checked')) 
            isCompleted = true;
         else 
            isCompleted = false;
        

        updateStatus(id, text, isCompleted);
    });

    $('#create-form').on('submit', function (event) {
        AddTodo(event);
    });

};

$(document).ready(function () {
    GetTodos();
    WireEvents();
});
