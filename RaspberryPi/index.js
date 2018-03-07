var app = require('express')();
var http = require('http').Server(app);
var io = require('socket.io')(http);
var keypress = require('keypress');

app.get('/', function(req, res){
	res.send("Hello world!");

});

http.listen(3000, function(){
	console.log('listening on *:3000');
});

io.on('connection', function(socket){
	console.log('a user connected');

	keypress(process.stdin);

	process.stdin.on('keypress', function(ch, key){
		console.log('got "keypress"', key);
		if(key && key.name == 'up'){
			socket.emit('pos', '0,0,1');
		}

		if(key && key.name == 'down'){
			socket.emit('pos', '0,0,-1');
		}
		
		if(key && key.name == 'left'){
			socket.emit('pos', '-1,0,0');
		}

		if(key && key.name == 'right'){
			socket.emit('pos', '1, 0, 0');
		}
	});

	process.stdin.setRawMode(true);
	process.stdin.resume();
});
