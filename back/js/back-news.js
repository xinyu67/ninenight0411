//JSON 檔案網址
const url = "https://localhost:7094/api/New";
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
    <div class="news-content">
            <div class="news-top">
                <div class="news-top-left"><img src="./img/product.jpg"></div>
                <div class="news-top-right">
                    <div class="title"><a>${data.new_title}</a></div>
                    <div class="newscontent"><a>${data.new_content}</a></div>
                    <div class="news-time">${data.new_startdate} - ${data.new_enddate}</div>
                </div>
                
            </div>
            <div class="news-bottom">
                <a href="./back-news-edit.html?new_id=${data.new_id}"><input type="button" class="edit-btn mouse" value="編輯"></a>
                <a href="#"><input type="button" class="delete-btn mouse" value="刪除"></a>
            </div>
        </div>
    `
  })
  p_title.innerHTML = str;
}
