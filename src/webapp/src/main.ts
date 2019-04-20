var text = "TeamZ is coming..."
var count = 0;

var node = document.getElementById("app")

var interval = setInterval(() => {

    if (Math.random() < 0.5) {
        return;
    }
    var char = text[count++]

    var textnode = document.createTextNode(char ? char : char + " ")
    var wbr = document.createElement("wbr")
    node.appendChild(textnode)
    node.appendChild(wbr)

}, 200);

