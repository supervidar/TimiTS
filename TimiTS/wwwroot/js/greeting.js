var myVar = setInterval(function () {
    myTimer();
}, 1000);

function myTimer() {    
    switch (new Date().getHours()) {        
        case 9:
            document.getElementById("greeting").innerHTML = "God formiddag";
            break;
        case 12:
            document.getElementById("greeting").innerHTML = "God ettermiddag";
            break;
        case 18:
            document.getElementById("greeting").innerHTML = "God kveld";
            break;
        case 24:
            document.getElementById("greeting").innerHTML = "God morgen!";
            break;
        
    }
    //if (time<9) {
    //    document.getElementById("greeting").innerHTML = "God morgen!";
    //}
    //if (9<time<12) {
    //    document.getElementById("greeting").innerHTML = "God formiddag!";
    //}
    //if (12<time<18) {
    //    document.getElementById("greeting").innerHTML = "God ettermiddag!";
    //}
    //if (18<time<24) {
    //    document.getElementById("greeting").innerHTML = "God kveld!";
    //}
   
}