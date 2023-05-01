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
        var user_id = localStorage.getItem("user_id");
        console.log(user);
        hello.style.display = "none";
        hello2.style.display = "flex";
        useer.innerHTML = user;

        var uu = window.location.href;
        var product_id = uu.split("=");
        console.log(product_id[1]);

        // JSON 檔案網址
        const url =
            `https://localhost:7094/api/Product/product_id?product_id=` + product_id[1];

        let data = [];
        /** 步驟一 取得資料**/
        (function getData() {
            axios.get(url).then(function(response) {
                // 檢查：
                console.log("url.search: " + url.search);
                console.log(response.data);
                // 將取得資料帶入空陣列 data 中
                data = response.data;
                product(data);
            });
        })();

        //產品總覽
        function product(arr) {
            //抓取欄位
            const p_ii = document.querySelector(".content-body");
            let str = "";
            //將資料存入
            arr.forEach(function(data) {
                str += ` 
        <div class="top">
        <div class="left">
            <div class="left-content">
                <div class="title">
                <input type="hidden" value="${data.product_id}">
                    <span id="title-c">${data.product_name}<br></span>
                    <span id="title-e">${data.product_eng}</span>
                </div>
                <div class="product-item">
                    <span>Item No.</span>
                    <span>&nbspT10330650</span>
                </div>
                <div class="product-content">
                    <div class="product-text">
                        <div class="t1">品牌<em id="eng">Brand</em></div>
                        <div class="t2">${data.brand_name} <em id="eng">${data.brand_eng}</em></div>
                    </div>
                    <div class="product-text">
                        <div class="t1">產地<em id="eng">Origin</em></div>
                        <div class="t2">${data.place_name} <em id="eng">${data.place_eng}</em></div>
                        <div class="price">
                            <span id="nt">NT：</span>
                            <span id="money">${data.product_price}</span>
                            <span>/</span>
                            <span id="unit">瓶</span>
                        </div>
                    </div>
                    <div class="product-text">
                        <div class="t1">容量<em id="eng">Volume</em></div>
                        <div class="t2"><em id="eng">${data.product_ml}ml</em></div>
                        <div class="buy">
                            <button id="add${data.product_id}"><span>加入購物車<div class="cart"><img src="./img/product/p0.png"></div></span></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="right"><img src="${data.product_img}"></div>
    </div>

    <div class="down">
        <div class="down-title">
            產品介紹
        </div>
        <div class="down-content">
            <div class="down-text">
            ${data.product_content}
            </div>
        </div>
        <div class="down-btn">
            <a href="./front-product.html">返回</a>
        </div>

    </div>
        `;
            });
            p_ii.innerHTML = str;

            const add = document.querySelectorAll(".buy button");

            add.forEach((button) => {
                button.addEventListener("click", () => {
                    // 從按鈕的ID中取商品ID
                    const productId = button.id.substring(3);
                    const apiUrl = `https://localhost:7094/api/Cart?user_id=${user_id}&product_id=${productId}`;

                    axios
                        .post(apiUrl)
                        .then((response) => {
                            console.log(response.data);
                            alert(response.data);
                            //alert(`${productId}`);
                            // ("商品已成功加入購物車");
                        })
                        .catch((error) => {
                            console.error(error);
                            //alert("加入購物車失敗");
                        });
                });
            });
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