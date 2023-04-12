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
}