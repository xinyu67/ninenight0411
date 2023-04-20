// JSON Product_all檔案網址
const url = "https://localhost:7094/api/Product";

let data = [];
/** 步驟一 取得資料**/
(function getData() {
    axios.get(url)
        .then(function(response) {
            // 檢查：  
            console.log(response.data);
            // 將取得資料帶入空陣列 data 中
            data = response.data;
            title(data);
        })
})();

//產品總覽
function title(arr) {
    //抓取欄位
    const p_title = document.querySelector('.menu-list')
    let str = "";
    //將資料存入
    arr.forEach(function(data) {
        str += ` 
            <div class="menu-product">
            <div class="p-2">
                <div class="product-img">
                    <a href="./front-product-introduction.html"><img src="${data.product_img}" alt="" class="img-auto" /></a>
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
                                <button><span>加入購物車<div class="cart"><img src="./img/product/p0.png"></div></span></button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        `
    })
    p_title.innerHTML = str;

}

// JSON 篩選檔案網址
const selectb_url = "https://localhost:7094/api/Brand";

let data1 = [];
/** 步驟一 取得資料**/
(function getData() {
    axios.get(selectb_url)
        .then(function(response) {
            // 檢查：  
            console.log(response.data);
            // 將取得資料帶入空陣列 data 中
            data1 = response.data;
            select_b(data1);
        })
})();

//品牌下拉
function select_b(add) {
    //抓取欄位

    const s_brand = document.querySelector('.dropdown-container1')
    let str = "";
    //將資料存入
    add.forEach(function(data1) {
        str += ` 
            
        `
    })

    s_brand.innerHTML = str;
}


// JSON 篩選檔案網址
const select_url = "https://localhost:7094/api/Place";

let data2 = [];
/** 步驟一 取得資料**/
(function getData() {
    axios.get(select_url)
        .then(function(response) {
            // 檢查：  
            console.log(response.data);
            // 將取得資料帶入空陣列 data 中
            data2 = response.data;
            select_p(data2);
        })
})();

//產地下拉
function select_p(add) {
    //抓取欄位

    const s_place = document.querySelector('.dropdown-container2')
    let str = "";
    //將資料存入
    add.forEach(function(data2) {
        str += ` 
        
        `
    })

    s_place.innerHTML = str;
}