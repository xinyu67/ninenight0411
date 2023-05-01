//  用getItem抓login的資料存入login裡
var login = localStorage.getItem('login');
console.log(login);
//  如果login是null就跳轉到登入畫面(會是null是因為登出時清空了所有在localStorage裡的資料)
if (login === null) {
    window.location.href = "../front/login.html";
} else {



    // 抓現在網址
    var bb = window.location.href;
    // console.log(bb);
    // 從'='切割
    var ary1 = bb.split('=');
    // console.log(ary1[1]);

    //JSON 檔案網址
    const url = "https://localhost:7094/api/Order_B_/order_id?order_id=" + ary1[1];
    let data = [];
    // step 1 - 取得資料
    (function getData() {
        axios.get(url)
            .then(function(response) {
                // 檢查
                console.log(response.data);
                // 將取得資料帶入空陣列data中
                data = response.data;
                title(data);
            })
    })();

    function title(arr) {
        // 抓取欄位
        const p_title = document.querySelector('.order_all_content')
        let str = "";
        // 將資料存入
        arr.forEach(function(data) {
            if (data.order_pick === true) { //宅配->訂單可取貨4
                $ccc = "<div style='color:blue'>宅配</div>" + "" + data.order_address;
            } else if (data.order_pick === false) { //自取->訂單已出貨3
                $ccc = "<div style='color:green'>自取</div>" + "" + data.order_address;
            }
            if (data.order_pick === true) { //宅配->訂單可取貨4
                $cdd = "送貨地址：";
            } else if (data.order_pick === false) { //自取->訂單已出貨3
                $cdd = "取貨店家：";
            }
            str += `
  <tr align="center">
  <td id="num">${data.product_id}</td>
  <td id="name">${data.product_name}</td>
  <td id="phone">${data.cart_product_amount}</td>
  <td id="price">${data.product_price}</td>
  `
        })
        p_title.innerHTML = str;

        var totall = document.getElementById('totall')
        totall.innerHTML = `${data[0].order_price}`;

        // var cart_num = document.getElementById('cart_num')
        // cart_num.innerHTML = `${data[0].cart_id}`;

        var order_num = document.getElementById('order_num')
        order_num.innerHTML = `${data[0].order_id}`;
        console.log(`${data[0].order_id}`);

        var user_num = document.getElementById('user_num')
        user_num.innerHTML = `${data[0].user_account}`;

        var takedelivery_num = document.getElementById('takedelivery_num')
        takedelivery_num.innerHTML = `${data[0].order_address}`;

        var time_num = document.getElementById('time_num')
        time_num.innerHTML = `${data[0].order_date}`;

        var address = document.getElementById('address')
        address.innerHTML = $cdd;

        arr.forEach(function(data) {

        })

    }
}