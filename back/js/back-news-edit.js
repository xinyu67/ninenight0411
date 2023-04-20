var editor = CKEDITOR.replace('editorDemo');

// 抓現在網址
var bb=window.location.href;
// console.log(bb);
// 從'='切割
var ary1 = bb.split('=');
// console.log(ary1[1]);



//JSON 檔案網址
const url = "https://localhost:7094/api/New/new_id?new_id="+ary1[1];
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
  var startdate = document.getElementById('startdate')
  var enddate = document.getElementById('enddate')
  var htmll = document.getElementById('htmll')
  var title = document.getElementById('title')
  startdate.value = data[0].new_startdate;
  enddate.value = data[0].new_enddate;
  htmll.value = data[0].new_content;
  title.value = data[0].new_title;
//   console.log(data[0].new_content)
//   console.log(ary1[1])
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
	console.log(abc);
	document.getElementById("blah").src=abc;
	}
	);
	}