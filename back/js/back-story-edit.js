// //  用getItem抓login的資料存入login裡
// var login = localStorage.getItem('login');
// console.log(login);
// //  如果login是null就跳轉到登入畫面(會是null是因為登出時清空了所有在localStorage裡的資料)
// if(login === null){
//     window.location.href = "../front/login.html";
// }else{




var editor = CKEDITOR.replace('editorDemo');


// 抓現在網址
var bb=window.location.href;
// console.log(bb);
// 從'='切割
var ary1 = bb.split('=');
// console.log(ary1[1]);

//JSON 檔案網址
const url = "https://localhost:7094/api/StoryControllers/story_id?story_id="+ary1[1];
// console.log(url);
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
  var htmll = document.getElementById('editorDemo')
  var reset = document.getElementById('reset')

  document.getElementById("blah").src = data[0].story_img;
  document.getElementById('imgsrc1').value = data[0].story_img;
  title.value = data[0].story_title;
  htmll.value = data[0].story_content;
  // console.log(data[0].product_num);
//   console.log(ary1[1])

reset.addEventListener('click',function(){
  document.getElementById("blah").src = data[0].story_img;
  title.value = data[0].story_title;
  htmll.value = data[0].story_content;
});
}

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
  console.log(e.target.files[0].name);
	console.log(abc);
	document.getElementById("blah").src=abc;
  document.getElementById('imgsrc').value = e.target.files[0].name;
  console.log(e.target.files[0].name);
	}
	);
	}




var aas = document.getElementById('button')
function aa(e){
  
var title = document.getElementById('title')
// 本來圖片的檔名
var aaa = document.getElementById('imgsrc1').value;
// 上傳圖片時才會有的檔名
var ggg = document.getElementById('imgsrc').value;

const myFile = document.getElementsByName('myfile')[0].files[0];
var bbb = title.value;
var htmll = CKEDITOR.instances.editorDemo.getData();
console.log(htmll);
console.log(bbb);
if(ggg===""){
  iii=aaa;
}else if(ggg!=""){
  iii=ggg;
}
// console.log(iii);
// console.log(ggg);

if((htmll === "" ) || (bbb === "")){
  alert("欄位不得為空，請輸入");
  return false;
}else if((htmll != "" ),(bbb != "" )){
  const formData = new FormData();
// 添加文本字段
formData.append('story_id', ary1[1]);
formData.append('story_title', bbb);
if(ggg===""){
  iii=aaa;
  // document.getElementById("blah").src = "";
  formData.append('story_img', "");
}else if(ggg!=""){
  iii=ggg;
  formData.append('story_img', myFile);
}
formData.append('story_content', htmll);

axios.put('https://localhost:7094/api/StoryControllers', formData, {
  headers: {
    'Content-Type': 'multipart/form-data'
  }
}).then(response => {
  console.log('Response:', response);
  // location.reload();
  window.location.href = "back-story.html";
  alert("編輯成功");
}).catch(error => {
  console.error('Error:', error);
});
}


}
aas.addEventListener('click', aa);




// }