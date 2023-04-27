// //  用getItem抓login的資料存入login裡
// var login = localStorage.getItem('login');
// console.log(login);
// //  如果login是null就跳轉到登入畫面(會是null是因為登出時清空了所有在localStorage裡的資料)
// if(login === null){
//     window.location.href = "../front/login.html";
// }else{




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
  var address = document.getElementById('address')
  var tel = document.getElementById('tel')
  var email = document.getElementById('email')
  var time = document.getElementById('time')
  const myFile = document.getElementsByName('myfile')[0].files[0];
  console.log("myFile",myFile);
  var bbb = title.value;
  var ccc = address.value;
  var ddd = tel.value;
  var eee = email.value;
  var hhh = time.value;

  if((hhh === "" ) || (bbb === "") || (ccc === "") || (ddd === "") || (eee === "") || (myFile === undefined)){
    alert("欄位不得為空，請輸入");
    return false;
  }else if((hhh != "" ),(bbb != ""),(ccc != ""),(ddd != ""),(eee != ""),(myFile != undefined)){
const formData = new FormData();
  formData.append("store_img", myFile);
  formData.append('store_name', title.value);
  formData.append('store_address', address.value);
  formData.append('store_email', email.value);
  formData.append('store_phone', tel.value);
  formData.append('store_time', time.value);
  
	
axios.post('https://localhost:7094/api/Store', formData, {
  headers: {
    'Content-Type': 'multipart/form-data'
  }
}).then(response => {
  console.log('Response:', response);
  window.location.href = "back-store.html";
  // location.reload();
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
}
aas.addEventListener('click', aa);




// }