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


var aass = document.getElementById('button')

function bb(){
  var title = document.getElementById('title')
  var bbb = title.value;
  var htmll = CKEDITOR.instances.editorDemo.getData();
  console.log("htmll",htmll);
  const myFile = document.getElementsByName('myfile')[0].files[0];
  console.log("myFile",myFile);

  if((htmll === "" ) || (bbb === "")){
	alert("欄位不得為空，請輸入");
	return false;
  }else if((htmll != "" ),(bbb != "" )){
const formData = new FormData();
  formData.append("story_img", myFile);
  formData.append('story_title', title.value);
  formData.append('story_content', htmll);
  
	
axios.post('https://localhost:7094/api/StoryControllers', formData, {
  headers: {
    'Content-Type': 'multipart/form-data'
  }
}).then(response => {
  console.log('Response:', response);
  window.location.href = "back-story.html";
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