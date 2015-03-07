var http = require("http");
var util = require('util');
var url = require('url');
var path = require('path');
var EventEmitter = require('events').EventEmitter;
var qs = require('querystring');

var secretKey = 'a69849e8-b44a-4d23-903a-c09dd46e6deb';
var messageBus = new EventEmitter();
messageBus.setMaxListeners(10000);
var Messages = [];
var hasNewMessage = false;

http.createServer(function (req, response) {
    try {
        if (req.method == 'GET') {
            var url_parts = url.parse(req.url, true);
            
            var action = url_parts.query.action;
            if (action == "hear") {
                var addMessageListener = function (res) {
                    var userId = url_parts.query.id;
                    var match = url_parts.query.match;
                    messageBus.once('message', function (data) {
                        if (data.match != null && data.user != null) {
                            if (data.user == userId && data.match == match) {
                                WreiteResponse(req, res, data);
                            }
                        } else {
                            if (data.match != null) {
                                if (data.match == match) {
                                    WreiteResponse(req, res, data);
                                }
                            } else if (data.user != null && data.user == userId) {
                                WreiteResponse(req , res, data);
                            } else if (data.match == null && data.user == null) {
                                WreiteResponse(req , res, data);
                            }
                        }
                    });
                };
                addMessageListener(response);
                console.log('has new request: ' + EventEmitter.listenerCount(messageBus, 'message'));

            } else if (action == "push") {
                OnPushMessage(req, response);
            } else {
                response.writeHead(200);
                response.end();
            }
        }
        if (req.method == "POST") {
            if (path.dirname(req.url) == "/push" || req.url.indexOf('/push') > -1) {
                var url_parts = url.parse(req.url, true);
                if (url_parts.query.token==secretKey) {
                    var body = '';
                    req.on('data', function (chunk) {
                        body += chunk;
                    });
                    req.on('end', function () {
                        var postData = qs.parse(body);
                        console.log("push:" + body.toString());
                        messageBus.emit('message', JSON.parse(body));
                        
                        response.writeHead(200, { "Content-Type": "text/plain" });
                        response.write("ok");
                        response.end();
                
                    });
                }
            }
        
        }
        
        req.on('error', function (e) {
            console.log('problem with request: ' + e.message);
        });
        
        setTimeout(function () {
            response.writeHead(200, { "Content-Type": "text/plain", 'Access-Control-Allow-Origin': req.headers.origin, 'Access-Control-Allow-Methods': 'GET', 'Access-Control-Allow-Headers': 'Content-Type' });
            response.end();
        }, 60000);
    } catch(e) {
        console.log(e.message);
    } 
    

}).listen(8888);

function WreiteResponse(req, res, data) {
    var responseText = JSON.stringify(data);
    console.log('WreiteResponse:'+ responseText);
    res.writeHead(200, { "Content-Type": "text/plain",'Access-Control-Allow-Origin': req.headers.origin, 'Access-Control-Allow-Methods': 'GET','Access-Control-Allow-Headers': 'Content-Type'});
    res.write(JSON.stringify(data));
    res.end();
}

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
    
    messageBus.emit('message', url_parts.query.m);
    response.writeHead(200, { "Content-Type": "text/plain" });
    response.write("ok");
    response.end();
    
    console.log('pull: '+ request.url);
}