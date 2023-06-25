var connection = new SignalRHubConnectionBuilder().WithUrl("/ChatHub");
//الاسم اللي بيتكتب ف ال startup 

connection.on("RecieveMessage", function (fromUser, message) {
    var msg = fromUser + ":" + message;
    var li = document.createElement("li");
    li.textContent = msg;

    $("#list").prepend(li);
});

connection.start();

$("#btnsend").on("click", function () {
    var fromUser = $("#txtuser").val();
    var Message = $("#msguser").val();
    connection.invoke("SendMsg", fromUser, Message);
});