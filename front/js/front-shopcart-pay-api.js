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
        console.log(user);
        hello.style.display = "none";
        hello2.style.display = "flex";
        useer.innerHTML = user;
    }
    //購物車總覽--檔案網址
    const url = "https://localhost:7094/api/Cart?user_id=" + user_id;

    let data = [];
    /** 步驟一 取得資料**/
    (function getData() {
        axios.get(url)
            .then(function(response) {
                // 檢查：console.log(response.data);

                // 將取得資料帶入空陣列 data 中
                data = response.data;
                cart(data);
            })
    })();

    //購物車總覽
    function cart(arr) {
        //抓取欄位
        const cart_all = document.querySelector('.product_all')
        let str = "";
        //將資料存入
        arr[0].product_list.forEach(function(data) {
            console.log(arr)
            str += `
        <tr>
            <td>
                <div class="table-img"><img src="${data.product_img}"></div>
            </td>
            <td>${data.product_name}&nbsp;${data.product_ml}ml</td>
            <td>${data.product_price}</td>
            <td><input id="count_${data.cart_product_id}" type="text" value="${data.cart_product_amount}" size="1" style="text-align:center; background-color:#d2d0d0;" readonly></td>
            <td>$${data.money}</td>
        </tr>
    `;
        })

        arr.forEach(function(data) {
            cart_all.innerHTML = str;
            const total = document.querySelector(".total-money")
            total.innerHTML = `$&nbsp;${data.total}`;
        })

        arr.forEach(function(data) {
            const nextLink = document.getElementById("submit1");

            // const store = document.getElementById("store");
            // fetch("https://localhost:7094/api/Store")
            //     .then(response => response.json())
            //     .then(data => {
            //         data.forEach(storeName => {
            //             const option = document.createElement("option");
            //             option.text = storeName;
            //             option.value = storeName;
            //             store.add(option);
            //         });
            //     })
            //     .catch(error => console.error(error));


            const store = document.getElementById("store");
            fetch("https://localhost:7094/api/Store")
                .then(response => response.json())
                .then(data => {
                    data.forEach(stores => {
                        const option = document.createElement("option");
                        option.text = stores.store_name;
                        option.value = stores.store_name;
                        store.add(option);
                    });
                })
                .catch(error => console.error(error));

            const cc = document.getElementById("cc");
            store.addEventListener('change', function() {
                cc.value = store.value;
            });

            const ampm = document.getElementById("ampm");
            const ampma = document.getElementById("ampma");
            ampm.addEventListener('change', function() {
                ampma.value = ampm.value;
            })

            nextLink.addEventListener('click', function() {

                const cartid = data.cart_id; //購物車id
                const total = data.total; //總額
                const person = document.getElementById("take_person").value; //訂購人
                const phone = document.getElementById("take_phone").value; //訂購人電話
                const tt = document.querySelector('input[name="take"]:checked');
                if (!person || !phone || !tt) {
                    alert("欄位不得為空!!");
                }

                //取貨方式
                var take = document.querySelector('input[name="take"]:checked').value;
                const add = document.getElementById("add").value;


                if (take == 0) {
                    var type = "false";
                    var cstore = cc.value;
                } else if (take == 1) {
                    var type = "true";
                    var cstore = add;
                }

                //取貨日期
                const date = document.getElementById("take_time").value;

                //取貨時間
                if (ampma.value === "AM") {
                    time = document.getElementById("amtime").value;
                } else if (ampma.value === "PM") {
                    time = document.getElementById("pmtime").value;
                }

                if (!cstore || !phone || !date || !ampma.value || !time) {
                    alert("欄位不得為空!!");
                    return;
                }


                const url = `https://localhost:7094/api/Order`;
                const newdata = new FormData();
                newdata.append("cart_id", cartid);
                newdata.append("order_price", total);
                newdata.append("order_name", person);
                newdata.append("order_pick", type);
                newdata.append("order_address", cstore);
                newdata.append("order_phone", phone);
                newdata.append("order_picktime", date + '  ' + ampma.value + '  ' + time);
                console.log(newdata);

                axios.post(url, newdata, {
                        headers: {
                            'Content-Type': 'multipart/form-data'
                        }
                    }).then(response => {
                        alert("下單成功😊");
                        console.log(response);
                        window.location.href = './front-myorder.html';
                        //location.reload();

                    })
                    .catch(error => {
                        console.log(error);
                    })
            });
        })
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