// JSON 檔案網址
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
                    <a href="./front-product-introduction.html"><img src="./img/product/p1.png${data.product_img}" alt="" class="img-auto" /></a>
                </div>
                <div class="menu-content">
                    <div class="menu-content-text">
                        <div class="menu-title"><a href="./front-product-introduction.html">${data.product_name}</a></div>
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