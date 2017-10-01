// record start time
var startTime;

function display() {
    // later record end time
    var endTime = new Date();

    // time difference in ms
    var timeDiff = endTime - startTime;

    // strip the miliseconds
    timeDiff /= 1000;

    // get seconds
    var seconds = Math.round(timeDiff % 60);

    // remove seconds from the date
    timeDiff = Math.floor(timeDiff / 60);

    // get minutes
    var minutes = Math.round(timeDiff % 60);

    // remove minutes from the date
    timeDiff = Math.floor(timeDiff / 60);

    // get hours
    var hours = Math.round(timeDiff % 24);

    // remove hours from the date
    timeDiff = Math.floor(timeDiff / 24);

    // the rest of timeDiff is number of days
    var days = timeDiff;

   // if (days != 0) {
   //     hours = hours + (days * 24);
   // }

    $(".time").text(hours.zeroPad() + ":" + minutes.zeroPad() + ":" + seconds.zeroPad());
    setTimeout(display, 1000);
}

$(document).ready(function () {
    // startTime = new Date();
    test();
    setTimeout(display, 1000);

});

function test() {
    var node = document.getElementById('dato');
    startDato = node.textContent;
    //    alert(startDato);
    var testDate = new Date();
    testDate.setDate(startDato.substr(0, 2));
   // alert(startDato.substr(0, 2));
    testDate.setMonth(startDato.substr(3, 2) - 1);
    testDate.setYear(startDato.substr(6, 4));
    testDate.setHours(startDato.substr(11, 2));
    testDate.setMinutes(startDato.substr(14, 2));
    testDate.setSeconds(startDato.substr(17, 2));
 //   alert(testDate);
    startTime = testDate;
    var testerino = startDato.substr(6, 4);
    //  alert(testerino);
}

// For å få finare klokke
Number.prototype.zeroPad = function () {
    return ('0' + this).slice(-2);
};
