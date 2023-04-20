var uu = window.location.href;
var product_id = uu.split("=");
console.log(product_id[1]);

// JSON 檔案網址
const url = `https://localhost:7094/api/Product/product_id?product_id=` + product_id[1];

let data = [];
/** 步驟一 取得資料**/
(function getData() {
    axios.get(url)
        .then(function(response) {
            // 檢查：  
            console.log('url.search: ' + url.search);
            console.log(response.data);
            // 將取得資料帶入空陣列 data 中
            data = response.data;
            product(data);
        })
})();

//產品總覽
function product(arr) {
    //抓取欄位
    const p_ii = document.querySelector('.content-body')
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
                            <span id="money">75</span>
                            <span>/</span>
                            <span id="unit">瓶</span>
                        </div>
                    </div>
                    <div class="product-text">
                        <div class="t1">容量<em id="eng">Volume</em></div>
                        <div class="t2"><em id="eng">${data.product_ml}ml</em></div>
                        <div class="buy">
                            <button><span>加入購物車<div class="cart"><img src="./img/product/p0.png"></div></span></button>
                        </div>
                    </div>
                </div>
            </div>


        </div>
        <div class="right"><img src="./img/product/p1.png"></div>
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
        `
    })
    p_ii.innerHTML = str;
}