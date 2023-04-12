window.onload = function() {
    var count = document.getElementById("count");
    var inc = document.getElementById("inc");
    var dec = document.getElementById("dec");
    inc.onclick = function() {
        count.value = parseInt(count.value) + 1;
    };
    dec.onclick = function() {
        count.value = parseInt(count.value) - 1;
    };
};