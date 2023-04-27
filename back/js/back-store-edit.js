// //  用getItem抓login的資料存入login裡
// var login = localStorage.getItem('login');
// console.log(login);
// //  如果login是null就跳轉到登入畫面(會是null是因為登出時清空了所有在localStorage裡的資料)
// if(login === null){
//     window.location.href = "../front/login.html";
// }else{




// 抓現在網址
var bb=window.location.href;
// console.log(bb);
// 從'='切割
var ary1 = bb.split('=');
// console.log(ary1[1]);

//JSON 檔案網址
const url = "https://localhost:7094/api/Store/store_id?store_id="+ary1[1];
console.log(url);
let data = [];
// step 1 - 取得資料
(function getData(){
  axios.get(url)
    .then(function(response){
      // 檢查
      console.log(response.data);
      // 將取得資料帶入空陣列data中
      data = response.data;
      title(data);
    })
})();
function title(arr) {
    var title = document.getElementById('title')
    var address = document.getElementById('address')
    var tel = document.getElementById('tel')
    var email = document.getElementById('email')
    var time = document.getElementById('time')
    var reset = document.getElementById('reset')
  
    document.getElementById("blah").src = data[0].store_img;
    document.getElementById('imgsrc1').value = data[0].store_img;
    title.value = data[0].store_name;
    address.value = data[0].store_address;
    tel.value = data[0].store_phone;
    email.value = data[0].store_email;
    time.value = data[0].store_time;
    console.log(data[0].store_img);
  //   console.log(ary1[1])
  
  reset.addEventListener('click',function(){
    document.getElementById("blah").src = data[0].store_img;
    title.value = data[0].store_name;
    address.value = data[0].store_address;
    tel.value = data[0].store_phone;
    email.value = data[0].store_email;
    time.value = data[0].store_time;
  });
  }
  



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
  console.log(e.target.files[0].name);
	console.log(abc);
	document.getElementById("blah").src=abc;
  document.getElementById('imgsrc').value = e.target.files[0].name;
  console.log(e.target.files[0].name);
}
);



var aas = document.getElementById('button')
function aa(e){
  var title = document.getElementById('title')
  var address = document.getElementById('address')
  var tel = document.getElementById('tel')
  var email = document.getElementById('email')
  var time = document.getElementById('time')
// 本來圖片的檔名
var aaa = document.getElementById('imgsrc1').value;
// 上傳圖片時才會有的檔名
var ggg = document.getElementById('imgsrc').value;

const myFile = document.getElementsByName('myfile')[0].files[0];
var bbb = title.value;
var ccc = address.value;
var ddd = tel.value;
var eee = email.value;
var hhh = time.value;
if(ggg===""){
  iii=aaa;
}else if(ggg!=""){
  iii=ggg;
}
console.log(iii);
console.log(ggg);


if((hhh === "" ) || (bbb === "") || (ccc === "") || (ddd === "") || (eee === "")){
  alert("欄位不得為空，請輸入");
  return false;
}else if((hhh != "" ),(bbb != ""),(ccc != ""),(ddd != ""),(eee != "")){
const formData = new FormData();
// 添加文本字段
formData.append('store_id', ary1[1]);
formData.append('store_name', bbb);
formData.append('store_address', ccc);
formData.append('store_phone', ddd);
formData.append('store_email', eee);
formData.append('store_time', hhh);
if(ggg===""){
  iii=aaa;
  // document.getElementById("blah").src = "";
  formData.append('store_img', "");
}else if(ggg!=""){
  iii=ggg;
  formData.append('store_img', myFile);
}



axios.put('https://localhost:7094/api/Store', formData, {
  headers: {
    'Content-Type': 'multipart/form-data'
  }
}).then(response => {
  console.log('Response:', response);
  window.location.href = "back-store.html";
  // location.reload();
  alert("編輯成功");
}).catch(error => {
  console.error('Error:', error);
});
}



}
aas.addEventListener('click', aa);




// }