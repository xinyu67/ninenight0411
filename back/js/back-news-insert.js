// //  用getItem抓login的資料存入login裡
// var login = localStorage.getItem('login');
// console.log(login);
// //  如果login是null就跳轉到登入畫面(會是null是因為登出時清空了所有在localStorage裡的資料)
// if(login === null){
//     window.location.href = "../front/login.html";
// }else{




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
	var abc=getObjectURL(e.target.files[0]);
	const myFile = document.getElementsByName('myfile')[0].files[0];
	document.getElementById("imged").value = myFile;
  	console.log(myFile);
	}
	);

	

	}

	var today = new Date();
	var dd = today.getDate();
	var mm = today.getMonth() + 1; // 一月是 0，所以需要加上 1
	var yyyy = today.getFullYear();
  
	if (dd < 10) {
	  dd = '0' + dd;
	}
  
	if (mm < 10) {
	  mm = '0' + mm;
	}
  
	var minDate = yyyy + '-' + mm + '-' + dd;
	var start = document.getElementById('start');
	document.getElementById("start").setAttribute("min", minDate);

	function aaaa(){
	document.getElementById("end").setAttribute("min", start.value);
	}
	start.addEventListener('change', aaaa);

	


var aass = document.getElementById('button')
function bb(){
  var title = document.getElementById('title')
  var htmll = CKEDITOR.instances.editorDemo.getData();
  console.log("htmll",htmll);
  var start = document.getElementById('start')
  var end = document.getElementById('end')
  const myFile = document.getElementsByName('myfile')[0].files[0];
  console.log("myFile",myFile);
  var bbb = title.value;
var ccc = start.value;
var ddd = end.value;


  if((htmll === "" ) || (bbb === "") || (ccc === "") || (ddd === "")){
	alert("欄位不得為空，請輸入");
	return false;
  }else if((htmll != "" ),(bbb != "" ),(ccc != ""),(ddd != "")){
const formData = new FormData();
  formData.append("new_img", myFile);
  formData.append('new_title', title.value);
  formData.append('new_startdate', start.value);
  formData.append('new_enddate', end.value);
  formData.append('new_content', htmll);
  
	
axios.post('https://localhost:7094/api/New', formData, {
  headers: {
    'Content-Type': 'multipart/form-data'
  }
}).then(response => {
  console.log('Response:', response);
  window.location.href = "back-news.html";
//   location.reload();
  alert("新增成功");
}).catch(error => {
  console.error('Error:', error);
});
  }


}
aass.addEventListener('click', bb);




var aas = document.getElementById('reset')

function aa(e){
	document.getElementById("blah").src=" ";
	CKEDITOR.instances.editorDemo.setData("");
}
aas.addEventListener('click', aa);




// }