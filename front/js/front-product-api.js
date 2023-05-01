var login = localStorage.getItem("login");
console.log(login);
//  如果login是null就跳轉到登入畫面(會是null是因為登出時清空了所有在localStorage裡的資料)
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
        console.log(user_id);
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

//產品總覽--檔案網址
const url = "https://localhost:7094/api/Product";

let data = [];
/** 步驟一 取得資料**/
(function getData() {
    axios.get(url).then(function(response) {
        // 檢查：
        console.log(response.data);
        // 將取得資料帶入空陣列 data 中
        data = response.data;
        title(data);
    });
})();

//產品總覽+分頁
function title() {
    // 定義每頁顯示的商品數量
    const productsPerPage = 9;

    // 計算總頁數
    function getTotalPages() {
        return Math.ceil(data.length / productsPerPage);
    }

    // 創建分頁按鈕
    function createPagination() {
        const totalPages = getTotalPages();
        const pagination = document.createElement("ul");
        pagination.classList.add("pagination");
        for (let i = 1; i <= totalPages; i++) {
            const pageButton = document.createElement("li");
            pageButton.textContent = i;
            const p_title = document.getElementById("thispage");
            if (i == 1) pageButton.classList.add("li-selected");
            p_title.innerHTML = "第 1 頁";

            pageButton.addEventListener("click", () => {
                const selectedPage = document.querySelector(".li-selected");
                selectedPage.classList.remove("li-selected");
                const p_title = document.getElementById("thispage");
                p_title.innerHTML = "第 " + i + " 頁";
                showPage(i);
                pageButton.classList.add("li-selected");
            });
            pagination.appendChild(pageButton);
        }
        return pagination;
    }

    // 獲取指定頁碼的商品數據
    function getProductsByPage(pageNumber) {
        const startIndex = (pageNumber - 1) * productsPerPage;
        const endIndex = startIndex + productsPerPage;
        return data.slice(startIndex, endIndex);
    }

    // 渲染指定頁碼的商品列表
    function showPage(pageNumber) {
        const products = getProductsByPage(pageNumber);
        const productList = document.querySelector(".menu-list");
        productList.innerHTML = "";
        console.log(pageNumber);
        for (const data of products) {
            const productElement = document.createElement("div");
            productElement.classList.add("product");
            productElement.innerHTML = ` 
            <div class="menu-product">
            <div class="p-2">
                <div class="product-img">
                <a  href="./front-product-introduction.html?product_id=${data.product_id}"><img src="${data.product_img}" alt="" class="img-auto" /></a>
                </div>
                <div class="menu-content">
                    <div class="menu-content-text">
                    <input type="hidden" value="${data.product_id}">
                        <div class="menu-title"><a  href="./front-product-introduction.html?product_id=${data.product_id}">${data.product_name}</a></div>
                        <div class="m-content">
                            <div class="left">產地：${data.place_name}</div>
                            <div class="right">容量：${data.product_ml}ml</div>
                        </div>
                        <div class="m-line"></div>
                        <div class="price">
                            <div class="p-text">
                                <span id="t1">NT：$</span>
                                <span id="tt1">${data.product_price}</span>
                                <span id="t2">/</span>
                                <span id="t3">瓶</span>
                            </div>

                            <div class="buy">
                                <button id="add${data.product_id}"><span>加入購物車<div class="cart"><img src="./img/product/p0.png"></div></span></button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        `;
            productList.appendChild(productElement);
        }
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
                        // ("商品已成功加入購物車");
                    })
                    .catch((error) => {
                        console.error(error);
                        alert("加入購物車失敗，請確認是否已登入!!");
                    });
            });
        });
    }

    // 將分頁按鈕添加到頁面上
    function renderPagination() {
        const paginationContainer = document.getElementById("page");
        const pagination = createPagination();
        paginationContainer.appendChild(pagination);
    }

    // 初始時展示第一頁商品列表和分頁按鈕
    function init() {
        renderPagination();
        showPage(1);
    }

    init();

    //品牌篩選--檔案網址
    const selectb_url = "https://localhost:7094/api/Brand";

    let data1 = [];
    /** 步驟一 取得資料**/
    (function getData() {
        axios.get(selectb_url).then(function(response) {
            // 檢查：
            console.log(response.data);
            // 將取得資料帶入空陣列 data 中
            data1 = response.data;
            select_b(data1);
        });
    })();

    //品牌下拉
    function select_b(add) {
        //抓取欄位
        const s_brand = document.querySelector(".custom-select0");
        let str = "";
        no = `<option value=""><span class="c">全部品牌</span> <span class="e">Brand</span></option>`;
        //將資料存入
        add.forEach(function(data1) {
            str += ` 
        <option value="${data1.brand_id}">${data1.brand_name}</option>
        `;
        });

        s_brand.innerHTML = no + str;
    }

    //產地篩選--檔案網址
    const selectp_url = "https://localhost:7094/api/Place";

    let data2 = [];
    /** 步驟一 取得資料**/
    (function getData() {
        axios.get(selectp_url).then(function(response) {
            // 檢查：
            console.log(response.data);
            // 將取得資料帶入空陣列 data 中
            data2 = response.data;
            select_p(data2);
        });
    })();

    //產地下拉
    function select_p(add) {
        //抓取欄位

        const s_place = document.querySelector(".custom-select1");
        let str = "";
        no = `<option value=""><span class="c">全部產地</span> <span class="e">Origin</span></option>`;
        //將資料存入
        add.forEach(function(data2) {
            str += ` 
            <option value="${data2.place_id}">${data2.place_name}</option>
        `;
        });

        s_place.innerHTML = no + str;
    }
}