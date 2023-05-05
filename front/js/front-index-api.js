//  用getItem抓login的資料存入login裡
var login = localStorage.getItem("login");
console.log(login);
//  如果login是null就跳轉到登入畫面(會是null是因為登出時清空了所有在localStorage裡的資料)
if (login === null) {
    var hello = document.getElementById("login1");
    var hello2 = document.getElementById("login2");
    hello.style.display = "flex";
    hello2.style.display = "none";
} else {
    var hello = document.getElementById("login1");
    var hello2 = document.getElementById("login2");
    if ((user != "") & (login != null)) {
        var useer = document.getElementById("useer");
        var user = localStorage.getItem("user");
        var admin = localStorage.getItem("admin");
        console.log(user);
        console.log(admin);
        hello.style.display = "none";
        hello2.style.display = "flex";
        if (user != null) {
            useer.innerHTML = user;
        } else if (admin != null) {
            useer.innerHTML = admin;
        }

    }
    //登出
    var logout1 = document.getElementById("logout");

    function logout() {
        if (confirm('確認要登出嗎？') == true) {
            window.location.href = "../front/login.html";
            localStorage.clear();
        } else {

        }
    }
    logout1.addEventListener("click", logout);
}

// JSON 檔案網址
const url = "https://localhost:7094/api/Index/LatestNews";
let data = [];
/** 步驟一 取得資料**/
(function getData() {
    axios.get(url).then(function(response) {
        // 檢查：
        console.log(response.data);
        // 將取得資料帶入空陣列 data 中
        data = response.data;
        news(data);
    });
})();

//總覽
function news(arr) {
    //抓取欄位
    const news_all = document.querySelector(".news-content");
    let str = "";
    //將資料存入
    arr.forEach(function(data) {
        str += `
        <div class="news-list">
                    <span>${data.new_startdate}</span>
                    <span>-</span>
                    <span>${data.new_enddate}</span>
                    <a href="./front-news.html">${data.new_title}</a>
        </div>
    `;
    });
    news_all.innerHTML = str;
}

// JSON 檔案網址
const p_url = "https://localhost:7094/api/Index/LikeProduct";
let p_data = [];
/** 步驟一 取得資料**/
(function getData() {
    axios.get(p_url).then(function(response) {
        // 檢查：
        console.log(response.data);
        // 將取得資料帶入空陣列 data 中
        p_data = response.data;
        product(p_data);
    });
})();

//產品總覽
function product(add) {
    //抓取欄位
    const product_all = document.querySelector(".products-content");
    let str = "";
    //將資料存入
    add.forEach(function(p_data) {
        str += `
        <div class="products-list">
        <div class="products-content-title">
            ${p_data.product_name}
        </div>
        <div class="products-img">
            <a href="./front-product.html"><img src="${p_data.product_img}"></a>
        </div>
    </div>
    `;
    });
    product_all.innerHTML = str;
}