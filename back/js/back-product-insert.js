var editor = CKEDITOR.replace('editorDemo');


window.onload = function(){
	let input = document.querySelector('#img-ing')
	input.addEventListener('change', (e) => {
	function getObjectURL(file) {
		var url = null;
		if (window.createObjcectURL != undefined) {
		url = window.createObjectURL(file);
		} else if (window.URL != undefined) {
		url = window.URL.createObjectURL(file);
		}else if (window.webkituRL != undefined) {
		url = window.webkituRL.createObjectURL(file);
		}
		return url;
	}
	var abc=getObjectURL(e.target.files[0]);
	console.log(abc);
	document.getElementById("blah").src=abc;
	}
	);


    var insertplace = document.getElementById('insertplace')
    var insertbutton = document.getElementById('insertbutton')
    var placeselect = document.getElementById('place-select')

        insertplace.style.display = "none";

        placeselect.addEventListener("change",function(){
            if(placeselect.value === "change"){
                placeselect.style.display = "none";
                insertplace.style.display = "block";
            }
        });

        insertbutton.addEventListener("click",function(){
                placeselect.style.display = "block";
                insertplace.style.display = "none";
                placeselect.value = "";
        });

	}