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
                <div class="story-top-left"><img src="./img/story.jpg"></div>
                <div class="story-top-right">
                    <div class="title"><a>${data.story_title}</a></div>
                    <div class="storycontent"><a>${data.story_content}</a></div>
                </div>
            </div>
            <div class="story-bottom">
                <a href="./back-story-edit.html"><input type="button" class="edit-btn mouse" value="編輯"></a>
                <a href="#"><input type="button" class="delete-btn mouse" value="刪除"></a>
            </div>
        </div>
    `
  })
  p_title.innerHTML = str;
}
