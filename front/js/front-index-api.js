// JSON 檔案網址
const url = "https://localhost:7094/api/Index/LatestNews";
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
    const news_all = document.querySelector('.news-content')
    let str = "";
    //將資料存入
    arr.forEach(function(data) {
        str += `
        <div class="news-list">
                    <span>${data.new_startdate}</span>
                    <span>-</span>
                    <span>${data.new_enddate}</span>
                    <a href="#">${data.new_title}</a>
        </div>
    `
    })
    news_all.innerHTML = str;
}


// JSON 檔案網址
const p_url = "https://localhost:7094/api/Index/LikeProduct";
let p_data = [];
/** 步驟一 取得資料**/
(function getData() {
    axios.get(p_url)
        .then(function(response) {
            // 檢查：  
            console.log(response.data);
            // 將取得資料帶入空陣列 data 中
            p_data = response.data;
            product(p_data);
        })
})();

//產品總覽
function product(add) {
    //抓取欄位
    const product_all = document.querySelector('.products-content')
    let str = "";
    //將資料存入
    add.forEach(function(p_data) {
        str += `
        <div class="products-list">
        <div class="products-content-title">
            ${p_data.product_name}
        </div>
        <div class="products-img">
            <a href="#"><img src="./img/product/p1.png"></a>
        </div>
    </div>
    `
    })
    product_all.innerHTML = str;
}