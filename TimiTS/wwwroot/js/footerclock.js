var myVar = setInterval(function () {
    myTimer();
}, 1000);

function myTimer() {
    var d = new Date();
    var time = d.getHours();
    if (time>6) {
        document.getElementById("clock").innerHTML = "Morgen - " + d.toLocaleTimeString() + " " + d.getDate();
    }
    if (time>9) {
        document.getElementById("clock").innerHTML = "Formiddag - " + d.toLocaleTimeString() + " " + d.getDate();
    }
    if (time>=12) {
        document.getElementById("clock").innerHTML = "Middag - " + d.toLocaleTimeString() + " " + d.getDate();
    }
    if (time>12) {
        document.getElementById("clock").innerHTML = "Ettermiddag - " + d.toLocaleTimeString() + " " + d.getDate();
    }
    if (time>18) {
        document.getElementById("clock").innerHTML = "Kveld - " + d.toLocaleTimeString() + " " + d.getDate();
    }
    
}