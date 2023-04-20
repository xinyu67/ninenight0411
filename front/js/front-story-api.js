// JSON 檔案網址
const url = "https://localhost:7094/api/StoryControllers";
let data = [];
/** 步驟一 取得資料**/
(function getData() {
    axios.get(url)
        .then(function(response) {
            // 檢查：  
            console.log(response.data);
            // 將取得資料帶入空陣列 data 中
            data = response.data;
            story(data);
        })
})();

//產品總覽
function story(arr) {
    //抓取欄位
    const story_all = document.querySelector('.content-body')
    let str = "";
    //將資料存入
    arr.forEach(function(data) {
        str += `
        <div class="content-body">
        <div class="content-in">
        <div class="content-img">
            <img src="./img/aboutus.png">
        </div>
        <div class="content-text">
            <div class="content-title">
            ${data.story_title}
            </div>
            <div class="content-title-in">
                ${data.story_content}
            </div>
        </div>
    </div>
    </div>
    `
    })
    story_all.innerHTML = str;
}