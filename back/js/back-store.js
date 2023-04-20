//JSON 檔案網址
const url = "https://localhost:7094/api/Store";
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
    <div class="stronghold-content">
    <div class="stronghold-top">
                <div class="stronghold-top-left"><img src="./img/store1.jpg"></div>
                <div class="stronghold-top-right">
                    <div class="title"><a>${data.store_name}</a></div>
                    <div class="address"><a>地址：${data.store_address}</a></div>
                    <div class="tel"><a>電話：${data.store_phone}</a></div>
                    <div class="email"><a>信箱：ninenight2023@nutc.edu.tw</a></div>
                    <div class="time"><a>營業時間：${data.store_time}</a></div>
                </div>
            </div>
            <div class="stronghold-bottom">
                <a href="./back-store-edit.html"><input type="button" class="edit-btn mouse" value="編輯"></a>
                <a href="#"><input type="button" class="delete-btn mouse" value="刪除"></a>
            </div>
            </div>
    `
  })
  p_title.innerHTML = str;
}
