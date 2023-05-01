//  用getItem抓login的資料存入login裡
var login = localStorage.getItem("login");
console.log(login);
//  如果login是null就跳轉到登入畫面(會是null是因為登出時清空了所有在localStorage裡的資料)
if (login === null) {
    window.location.href = "../front/login.html";
} else {
    var welcomeadmin = localStorage.getItem("admin");
    console.log(welcomeadmin);
    var welcome = document.getElementById("welcome");
    welcome.innerHTML = "<a>" + "管理員帳號：" + welcomeadmin + "　　</a>";

    //JSON 檔案網址
    const url = "https://localhost:7094/api/Product";
    let data = [];
    // step 1 - 取得資料
    (function getData() {
        axios.get(url).then(function(response) {
            // 檢查
            console.log(response.data);
            // 將取得資料帶入空陣列data中
            data = response.data;
            title(data);
        });
    })();

    function title(arr) {
        // 定義每頁顯示的商品數量
        const productsPerPage = 12;

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
        // function showPage(pageNumber) {
        //   const products = getProductsByPage(pageNumber);
        //   const productList = document.getElementById("product-content");
        //   productList.innerHTML = "";
        //   console.log(pageNumber);
        //   for (const data of products) {
        //     const productElement = document.createElement("div");
        //     productElement.classList.add("product");
        //     productElement.innerHTML = `
        //   <div class="pro-top"><img class="head" src="${data.product_img}"></div>
        //   <div class="pro-bottom">
        //       <div class="pro-title">${data.product_name}</div>
        //       <div class="pro-place-ml">
        //           <div class="left">產地：${data.place_name}</div>
        //           <div class="right">容量：${data.product_ml}ml</div>
        //       </div>
        //       <div class="price">
        //           <div class="p-text">
        //               <span id="t1">NT：$</span>
        //               <span id="t2">${data.product_price}</span>
        //               <span id="t3">/瓶</span>
        //           </div>
        //           <div class="button">
        //               <a href="./back-product-edit.html?product_id=${data.product_id}"><input type="button" class="edit-btn mouse" value="編輯"></a>
        //               <input type="button" id="delete${data.product_id}" class="stop-btn mouse" value="刪除" >
        //               <input type="hidden" id="hidden${data.product_id}" value="${data.product_name}">
        //           </div>
        //       </div>
        //   </div>
        //   `;
        //     productList.appendChild(productElement);
        //   }
        // }

        // 渲染指定頁碼的商品列表
        function showPage(pageNumber) {
            const products = getProductsByPage(pageNumber);
            const productList = document.getElementById("product-content");
            productList.innerHTML = "";
            console.log(pageNumber);
            for (const data of products) {
                const productElement = document.createElement("div");
                productElement.classList.add("product");
                productElement.innerHTML = `
        <div class="pro-top"><img class="head" src="${data.product_img}"></div>
        <div class="pro-bottom">
            <div class="pro-title">${data.product_name}</div>
            <div class="pro-place-ml">
                <div class="left">產地：${data.place_name}</div>
                <div class="right">容量：${data.product_ml}ml</div>
            </div>
            <div class="price">
                <div class="p-text">
                    <span id="t1">NT：$</span>
                    <span id="t2">${data.product_price}</span>
                    <span id="t3">/瓶</span>
                </div>
                <div class="button">
                    <a href="./back-product-edit.html?product_id=${data.product_id}"><input type="button" class="edit-btn mouse" value="編輯"></a>
                    <input type="button" id="delete${data.product_id}" class="stop-btn mouse" value="刪除" >
                    <input type="hidden" id="hidden${data.product_id}" value="${data.product_name}">
                </div>
            </div>
        </div>
        `;
                productList.appendChild(productElement);

                const button = productElement.querySelector(
                    `#delete${data.product_id}`
                );
                const aas = productElement.querySelector(`#hidden${data.product_id}`);
                console.log(button);

                function aa(e) {
                    if (
                        confirm('您即將刪除 "' + aas.value + '" 的資料,確認要刪除嗎？') ==
                        true
                    ) {
                        var bbb =
                            "https://localhost:7094/api/Product?product_id=" +
                            data.product_id;
                        console.log(bbb);

                        fetch(bbb, {
                                method: "DELETE",
                                headers: { "Content-Type": "application/json" },
                                body: JSON.stringify({ reason: "no longer needed" }),
                            })
                            .then((response) => {
                                if (response.ok) {
                                    console.log(response.ok);
                                    alert("刪除成功");
                                    location.reload();
                                }
                            })
                            .catch((error) => console.error("Error:", error));


                    } else {
                        // alert('您已取消刪除');
                    }
                }
                button.addEventListener("click", aa);
            }
        }

        // 將分頁按鈕添加到頁面上
        function renderPagination() {
            const paginationContainer = document.getElementById(
                "pagination-container"
            );
            const pagination = createPagination();
            paginationContainer.appendChild(pagination);
        }

        // 初始時展示第一頁商品列表和分頁按鈕
        function init() {
            renderPagination();
            showPage(1);
        }

        init();
    }

    //   arr.forEach(function (data) {
    //     var button = document.getElementById("delete" + `${data.product_id}`);
    //     var aas = document.getElementById("hidden" + `${data.product_id}`);
    //     console.log(button);

    //     function aa(e) {
    //       // confirm('確認要刪除嗎？');
    //       if (
    //         confirm('您即將刪除 "' + aas.value + '" 的資料,確認要刪除嗎？') == true
    //       ) {
    //         // console.log(aas.value);

    //         var bbb =
    //           "https://localhost:7094/api/Product?product_id=" +
    //           `${data.product_id}`;
    //         console.log(bbb);
    //         fetch(bbb, {
    //           method: "DELETE",
    //           headers: {
    //             "Content-Type": "application/json",
    //           },
    //           body: JSON.stringify({ reason: "no longer needed" }),
    //         })
    //           .then((response) => {
    //             if (response.ok) {
    //               console.log("Resource deleted successfully.");
    //             } else {
    //               console.error("Failed to delete resource:", response.status);
    //             }
    //           })
    //           .catch((error) => console.error("Error:", error));

    //         alert("刪除成功");
    //         // location.reload();//網頁重新整理
    //       } else {
    //         // alert('您已取消刪除');
    //       }
    //     }
    //     button.addEventListener("click", aa);
    //   });

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

//測試
// window.onload = function(){
//   localStorage.setItem('login', '123'); //用setItem新增login=123到localStorage
//   var login = localStorage.getItem('login'); //用getItem抓login的資料存入login裡
//   console.log(login);  //列印login的資料為123
//   localStorage.clear();  //清除所有localStorage的資料
//   }