// JSON 檔案網址
const url = "https://localhost:7094/api/New";
let data = [];
/** 步驟一 取得資料**/
(function getData() {
    axios.get(url)
        .then(function(response) {
            // 檢查：  
            console.log(response.data);
            // 將取得資料帶入空陣列 data 中
            data = response.data;
            news(data);
        })
})();

//產品總覽
function news(arr) {
    //抓取欄位
    const news_all = document.querySelector('.content-body')
    let str = "";
    //將資料存入
    arr.forEach(function(data) {
        str += `
        <div class="content-in">
            <div class="content-img">
                <img src="./img/product.jpg">
            </div>

            <div class="content-text">
                <div class="content-title">${data.new_title}</div>
                    
                <div class="content-title-in">
                    <p id="p2">${data.new_content}</p>
                    
                    <div class="content-title-in2">
                        <p></p>
                        <div class="content-title-in3">
                            <p>${data.new_startdate}</p>
                            <p>－</p>
                            <p>${data.new_enddate}</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `
    })
    news_all.innerHTML = str;
}