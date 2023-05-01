const brandSelect = document.querySelector('.custom-select0');
const placeSelect = document.querySelector('.custom-select1');
const mlSelect = document.querySelector('.custom-select2');
const productList = document.querySelector('.menu-list');

brandSelect.addEventListener('change', fetchData);
placeSelect.addEventListener('change', fetchData);
mlSelect.addEventListener('change', fetchData);

function fetchData() {
    const brand = brandSelect.value;
    const place = placeSelect.value;
    const ml = mlSelect.value;

    let url = `https://localhost:7094/api/Product?search_brand=${brand}&search_place=${place}&search_ml=${ml}`;

    fetch(url)
        .then(response => response.json())
        .then(data => {
            if (data.length > 0) {
                let html = '';
                data.forEach(data => {
                    html += ` 
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
            `;
                });
                // 找到 productList 的定義，然後更新它的 innerHTML
                productList.innerHTML = html;
            }
        })
        .catch(error => console.log(error));
}