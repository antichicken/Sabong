var http = require("http");
var util = require('util');
var url = require('url');
var Messages = [];
var hasNewMessage = false;

//bet acceptted;
//ratio change
http.createServer(function (req, response) {
    if (req.method == 'GET') {
        var url_parts = url.parse(req.url, true);

        var action = url_parts.query.action;
        if (action == "hear") {
            console.log('has new request');
            CheckForNewMessage(req, response);
        } else if (action == "push") {
            OnPushMessage(req, response);
        } else {
            console.log('nothing');
            response.writeHead(200, { "Content-Type": "text/plain" });
            response.write("fuck you");
            response.end();
        }
    }
    
    req.on('error', function (e) {
        console.log('problem with request: ' + e.message);
    });
}).listen(8888);

function CheckForNewMessage(request, response) {
    if (Messages.length > 0) {
        console.log('write message');
        response.writeHead(200, { "Content-Type": "text/plain" });
        response.write(Messages[0]);
        response.end();
    } else {
        setTimeout(function () {
            CheckForNewMessage(request, response);
        }, 1000);
    }
}

function OnPushMessage(request, response) {
    var url_parts = url.parse(request.url, true);
    
    hasNewMessage = true;
    Messages.push(url_parts.query.m);
    console.log('has new message');
    response.writeHead(200, { "Content-Type": "text/plain" });
    response.write("ok");
    response.end();
}