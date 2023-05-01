var login = localStorage.getItem("login");
console.log(login);
//  如果login是null就跳轉到登入畫面(會是null是因為登出時清空了所有在localStorage裡的資料)
if (login === null) {
    window.location.href = "../front/login.html";
    //   var hello = document.getElementById("login1");
    //   var hello2 = document.getElementById("login2");
    //   hello.style.display = "flex";
    //   hello2.style.display = "none";
} else {
    var hello = document.getElementById("login1");
    var hello2 = document.getElementById("login2");
    if ((user != "") & (login != null)) {
        var useer = document.getElementById("useer");
        var user = localStorage.getItem("user");
        var user_id = localStorage.getItem("user_id");
        console.log(user_id);
        hello.style.display = "none";
        hello2.style.display = "flex";
        useer.innerHTML = user;
    }

    // JSON 檔案網址
    const url = "https://localhost:7094/api/Order?user_id=" + user_id;
    console.log(url);
    let data = [];
    /** 步驟一 取得資料**/
    (function getData() {
        axios.get(url).then(function(response) {
            // 檢查：
            console.log(response.data);
            // 將取得資料帶入空陣列 data 中
            data = response.data;
            order(data);
        });
    })();

    //訂購總覽
    function order(arr) {
        //抓取欄位
        const order_all = document.querySelector(".order_content");
        let str = "";

        //將資料存入
        if (data.length === 0) {
            str +=
                '<tr><td colspan="8">目前無任何訂單，請前往<a href="./front-product.html" style="color:#AD5A5A;text-decoration: underline;">產品區</a>購物🛍️</td></tr>';
            order_all.innerHTML = str;
        }
        arr.forEach(function(data) {
            //判斷訂單狀態
            let states = "";
            var state = "";
            states = `${data.order_state}`;
            if (states == "0") {
                state = "訂單未確認";
            } else if (states == "1") {
                state = "賣家取消此訂單";
            } else if (states == "2") {
                state = "訂單已確認";
            } else if (states == "3") {
                state = "訂單可取貨";
            } else if (states == "4") {
                state = "訂單已出貨";
            } else if (states == "5") {
                state = "訂單已完成";
            }

            str += `
        <tr>
        <td>${data.order_id}</td>
        <td><span>${data.order_num}</span> &nbsp;/<span>瓶</span></td>
        <td>$${data.order_price}</td>
        
        <td>${state}</td>
        <td>
            <li><a href="./front-myorder-introduction.html?order_id=${data.order_id}"><i class="fa-solid fa-file-lines"></i></a></li>
        </td>
    </tr>
    `;
        });
        order_all.innerHTML = str;
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