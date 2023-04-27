// //  用getItem抓login的資料存入login裡
// var login = localStorage.getItem('login');
// console.log(login);
// //  如果login是null就跳轉到登入畫面(會是null是因為登出時清空了所有在localStorage裡的資料)
// if(login === null){
//     window.location.href = "../front/login.html";
// }else{




//JSON 檔案網址
const url = "https://localhost:7094/api/New";
let data = [];
fetch(url)  //判斷api有沒有資料
  .then(response => {
    if (response.ok) {  //如果api有資料就執行以下程式碼顯示資料


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
    <div class="news-content">
            <div class="news-top">
                <div class="news-top-left"><img src="${data.new_img}"></div>
                <div class="news-top-right">
                    <div class="title"><a>${data.new_title}</a></div>
                    <div class="newscontent"><a>${data.new_content}</a></div>
                    <div class="news-time">活動期間：${data.new_startdate}  -  ${data.new_enddate}</div>
                </div>
                
            </div>
            <div class="news-bottom">
                <a href="./back-news-edit.html?new_id=${data.new_id}"><input type="button" class="edit-btn mouse" value="編輯"></a>
                <input type="button" id="delete${data.new_id}" class="delete-btn mouse" value="刪除">
                <input type="hidden" id="hidden${data.new_id}" value="${data.new_title}">
            </div>
        </div>
    `
  })
  p_title.innerHTML = str;

  arr.forEach(function(data){
    var button = document.getElementById('delete'+`${data.new_id}`)
    var aas = document.getElementById('hidden'+`${data.new_id}`)
    function aa(e){
      // confirm('確認要刪除嗎？');
      if (confirm('您即將刪除 "'+aas.value+'" 的資料,確認要刪除嗎？') == true) {
          // console.log(aas.value);
    
        var bbb = 'https://localhost:7094/api/New?new_id='+`${data.new_id}`;
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







    } else if(response.status === 404){  //如果api沒有資料就執行以下程式碼顯示"目前還沒有最新資訊!!"
      console.log(response.status);
      const p_title = document.querySelector('.error')
      p_title.innerHTML = "目前還沒有最新資訊!!";
    }
  })



// }