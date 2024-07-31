$(document).ready(function () {
    $('#btnOpenChat').click(function () {
        $('#chatPanel').show();
        loadMessages();
    });

    $('#btnCloseChat').click(function () {
        $('#chatPanel').hide();
    });

    $('#btnSendMessage').click(function (e) {
        e.preventDefault();

        var message = $('#messageInput').val().trim();
        if (message === '') {
            alert('אנא הקלד הודעה');
            return;
        }

        $.ajax({
            type: 'POST',
            url: 'ClientHome.aspx/SendMessage',
            data: JSON.stringify({ message: message }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (response) {
                $('#chatMessages').append('<li><strong>אני:</strong> ' + message + '</li>');
                $('#messageInput').val('');
            },
            error: function (xhr, status, error) {
                console.error("Error sending message: ", error);
            }
        });
    });



    function loadMessages() {
        $.ajax({
            type: "POST",
            url: "ClientHome.aspx/GetMessages",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                try {
                    var messages = JSON.parse(response.d); // Ensure parsing response correctly
                    if (Array.isArray(messages)) {
                        $('#chatMessages').empty(); // Clear existing messages
                        messages.forEach(function (msg) {
                            var sender = msg.IsFromCustomer ? 'לקוח' : 'מנהל';
                            var messageHtml = '<li><strong>' + sender + ':</strong> ' + msg.MessageText + ' <small>(' + new Date(parseInt(msg.SentDate.substr(6))).toLocaleString() + ')</small></li>';
                            $('#chatMessages').append(messageHtml);
                        });
                    } else {
                        console.error("Unexpected response format: ", response.d);
                    }
                } catch (e) {
                    console.error("Error parsing JSON: ", e);
                }
            },
            error: function (xhr, status, error) {
                console.error("Error loading messages: ", error);
            }
        });
    }




    function sendMessage(message) {
        $.ajax({
            type: "POST",
            url: "ClientHome.aspx/SendMessage",
            data: JSON.stringify({ message: message }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                try {
                    var result = JSON.parse(response.d); // Ensure parsing response correctly
                    if (result.success) {
                        console.log(result.success);
                    } else {
                        console.error(result.error);
                    }
                } catch (e) {
                    console.error("Error parsing JSON: ", e);
                }
            },
            error: function (xhr, status, error) {
                console.error("Error sending message: ", error);
            }
        });
    }

});