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
    logout1.addEventListener("click", logout);
}

// JSON 檔案網址
const url = "https://localhost:7094/api/StoryControllers";
let data = [];
/** 步驟一 取得資料**/
(function getData() {
    axios.get(url).then(function(response) {
        // 檢查：
        console.log(response.data);
        // 將取得資料帶入空陣列 data 中
        data = response.data;
        story(data);
    });
})();

//品牌故事總覽
function story(arr) {
    //抓取欄位
    const story_all = document.querySelector(".content-body");
    let str = "";
    //將資料存入
    arr.forEach(function(data) {
        str += `
        <div class="content-in">
        <div class="content-img">
            <img src="${data.story_img}">
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
    `;
    });
    story_all.innerHTML = str;
}