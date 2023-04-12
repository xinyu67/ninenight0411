var ampm = document.getElementById("ampm");
var atime = document.getElementById("amtime");
var ptime = document.getElementById("pmtime");
atime.style.display = "none";
ptime.style.display = "none";

ampm.addEventListener('change', function() {
    if (ampm.value === "AM") {
        ptime.value = "";
        atime.style.display = "flex";
        ptime.style.display = "none";
    } else if (ampm.value === "PM") {
        atime.value = "";
        atime.style.display = "none";
        ptime.style.display = "flex";
    } else {
        atime.value = "";
        ptime.value = "";
        atime.style.display = "none";
        ptime.style.display = "none";
    }
})