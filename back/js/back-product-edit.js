var editor = CKEDITOR.replace('editorDemo');

// 抓現在網址
var bb=window.location.href;
// console.log(bb);
// 從'='切割
var ary1 = bb.split('=');
// console.log(ary1[1]);

//JSON 檔案網址
const url = "https://localhost:7094/api/Product/product_id?product_id="+ary1[1];
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
  var chinese = document.getElementById('chinese')
  var place = document.getElementById('place')
  var brand = document.getElementById('brand')
  var english = document.getElementById('english')
  var ml = document.getElementById('ml')
  var price = document.getElementById('price')
  var htmll = document.getElementById('htmll')
  var reset = document.getElementById('reset')

  chinese.value = data[0].product_name;
  place.value = data[0].place_name;
  brand.value = data[0].brand_name;
  english.value = data[0].product_eng;
  ml.value = data[0].product_ml;
  price.value = data[0].product_price;
  htmll.value = data[0].product_content;
//   console.log(data[0].new_content)
//   console.log(ary1[1])

reset.addEventListener('click',function(){
  chinese.value = data[0].product_name;
  place.value = data[0].place_name;
  brand.value = data[0].brand_name;
  english.value = data[0].product_eng;
  ml.value = data[0].product_ml;
  price.value = data[0].product_price;
  htmll.value = data[0].product_content;
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
	console.log(abc);
	document.getElementById("blah").src=abc;
	}
	);
	}