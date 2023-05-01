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
const url = "https://localhost:7094/api/StoryControllers";
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
  const p_title = document.querySelector('.content')
  let str = "";
  // 將資料存入
  arr.forEach(function(data){
    str += `
    <div class="story-content">
            <div class="story-top">
                <div class="story-top-left"><img src="${data.story_img}"></div>
                <div class="story-top-right">
                    <div class="title"><a>${data.story_title}</a></div>
                    <div class="storycontent"><a>${data.story_content}</a></div>
                </div>
            </div>
            <div class="story-bottom">
                <a href="./back-story-edit.html?story_id=${data.story_id}"><input type="button" class="edit-btn mouse" value="編輯"></a>
                <input type="button" id="delete${data.story_id}" class="delete-btn mouse" value="刪除">
                <input type="hidden" id="hidden${data.story_id}" value="${data.story_title}">
            </div>
        </div>
    `
  })
  p_title.innerHTML = str;


  arr.forEach(function(data){
    var button = document.getElementById('delete'+`${data.story_id}`)
    var aas = document.getElementById('hidden'+`${data.story_id}`)
    function aa(e){
      // confirm('確認要刪除嗎？');
      if (confirm('您即將刪除 "'+aas.value+'" 的資料,確認要刪除嗎？') == true) {
          // console.log(aas.value);
    
        var bbb = 'https://localhost:7094/api/StoryControllers?story_id='+`${data.story_id}`;
        console.log(bbb);
        fetch(bbb, {
          method: 'DELETE',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({ reason: 'no longer needed' })
        })
        .then(response => {
          if (response.ok) {
            console.log('Resource deleted successfully.');
          } else {
            console.error('Failed to delete resource:', response.status);
          }
        })
        .catch(error => console.error('Error:', error));
    
        alert('刪除成功');
        location.reload();//網頁重新整理
      } else {
          // alert('您已取消刪除');
      }
    }
    button.addEventListener('click', aa);
    })


}




//登出
var logout1 = document.getElementById('logout')
function logout(){
window.location.href = "../front/login.html";
localStorage.clear();
}
logout1.addEventListener('click', logout);

}