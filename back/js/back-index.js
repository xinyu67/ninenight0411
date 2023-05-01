//  用getItem抓login的資料存入login裡
var login = localStorage.getItem('login');
console.log(login);
//  如果login是null就跳轉到登入畫面(會是null是因為登出時清空了所有在localStorage裡的資料)
if(login === null){
    window.location.href = "../front/login.html";
}else{

var welcomeadmin = localStorage.getItem('admin');
console.log(welcomeadmin);
var welcome = document.getElementById("welcome");
welcome.innerHTML = "<a>"+"管理員帳號："+welcomeadmin+"　　</a>";

//JSON 檔案網址
const url = "https://localhost:7094/api/Index/LikeProduct";
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
  // 抓取欄位
  const p_title = document.querySelector('.pro_bg_content')
  let str = "";
  // 將資料存入
  arr.forEach(function(data){
    str += `
    <div class="pro_content">
        <div class="pro_content_top"><a class="noline">${data.product_name}</a></div>
        <div class="pro_content_center"><a target="_blank"><img src="${data.product_img}"></a></div>
      </div>
    `
    console.log(`${data.product_img}`);
  })
  p_title.innerHTML = str;
}


//登出
var logout1 = document.getElementById('logout')
function logout(){
window.location.href = "../front/login.html";
localStorage.clear();
}
logout1.addEventListener('click', logout);


}