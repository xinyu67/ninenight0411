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
        console.log(user);
        hello.style.display = "none";
        hello2.style.display = "flex";
        useer.innerHTML = user;
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
}

// JSON 檔案網址
const url = "https://localhost:7094/api/Store";
let data = [];
/** 步驟一 取得資料**/
(function getData() {
    axios.get(url).then(function(response) {
        // 檢查：
        console.log(response.data);
        // 將取得資料帶入空陣列 data 中
        data = response.data;
        store(data);
    });
})();

//門市據點總覽
function store(arr) {
    //抓取欄位
    const store_all = document.querySelector(".content-body");
    let str = "";
    //將資料存入
    arr.forEach(function(data) {
        str += `
        <div class="left">
        <div class="l-img">
            <img src=" ${data.store_img}">
        </div>
        <div class="l-text">
            <div class="title">
                ${data.store_name}
            </div>
            <div class="down">
                <div class="down-content"><i class="fa-solid fa-location-dot"></i>${data.store_address}</div>
                <div class="down-content"><i class="fa-solid fa-phone"></i>${data.store_phone}</div>
                <div class="down-content"><i class="fa-solid fa-envelope"></i>${data.store_email}</div>
                <div class="down-content"><i class="fa-solid fa-clock"></i>${data.store_time}</div>
            </div>
        </div>

    </div>
        
    
    
    
    `;
    });
    store_all.innerHTML = str;
}