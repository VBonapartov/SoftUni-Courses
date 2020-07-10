//"use strict";

// Angular
//const options = {
//    accessTokenFactory: () => {
//        return localStorage.getItem('token');
//    }
//};

var connection = new signalR.HubConnectionBuilder()
    //.withUrl("https://localhost:5011/notifications", options)
    .withUrl("http://localhost:5011/notifications")
    .build();

connection
    .start()
    .then(() => console.log('Connection started'))
    .catch ((err) => console.log('Error while starting connection ' + err));

connection.on('ReceiveNotification', (data) => {
    console.log(data);
    var t = JSON.parse(JSON.stringify(data));
    NewBook(t);
});

function NewBook(data) {
    Notification.create(

        // Title
        "New Book!",

        // Text
        "Title: " + data.title +"; Price: " + data.price,

        // Notificaiton icon
        "",

        // animate.css classname
        "bounceIn",

          // position
          // 1 = top left
          // 2 = top right
          // 3 = bottom left
          // 4 = bottom right
        2,

        // delay
        6
    );
};


//connection.on("ReceiveMessage",
//    function (user, message, myProjectId, myProjectVal) {

//        var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
//        var pollResultMsg = user + " voted for '" + myProjectVal + "'.";
            
//        var ulPoll = document.getElementById("messagesList");
//        var liPollResult = document.createElement("li");
//        liPollResult.textContent = pollResultMsg;

//        ulPoll.insertBefore(liPollResult, ulPoll.childNodes[0]);
//        document.getElementById(myProjectId + 'Block').innerHTML += chartBlock;
//    });