var uu = window.location.href;
var order_id = uu.split("=");
console.log(order_id[1]);

// JSON 檔案網址
const url = `https://localhost:7094/api/Order/order_id?order_id=` + order_id[1];

let data = [];
/** 步驟一 取得資料**/
(function getData() {
    axios.get(url)
        .then(function(response) {
            // 檢查：  
            //console.log('url.search: ' + url.search);
            //console.log(response.data);
            // 將取得資料帶入空陣列 data 中
            data = response.data;
            order(data);
        })
})();

//訂單詳細總覽

function order(arr) {
    const order_pp = document.querySelector(".order_person")
    let ss = "";
    //將資料存入
    arr.slice(0, 1).forEach(function(data) {
        //抓取欄位

        //判斷宅配自取方式
        let type = "";
        var pike = "";
        type = `${data.order_pick}`;
        if (type === 'true') {
            pike = "宅配";
        } else if (type === 'false') {
            pike = "自取";
        }
        ss += `
        <tr>
        <td style="background-color:#DECECE">${data.order_name}</td>
        <td style="background-color:#DECECE">${data.order_phone}</td>
        <td style="background-color:#DECECE">${pike}</td>
        <td style="background-color:#DECECE">${data.order_address}</td>
        <td style="background-color:#DECECE">${data.order_picktime}</td>
    </tr>
    `
    })
    order_pp.innerHTML = ss;
    const order_ii = document.querySelector(".order_body")
    let str = "";
    //將資料存入
    arr.forEach(function(data) {
        console.log(`${order_id[1]}`);
        //抓取欄位
        str += `
        <tr>
        <td>
        <div class="table-img"><img src="${data.product_img}"></div>
        </td>
        <td>${data.product_name}&nbsp;${data.product_ml}ml</td>
        <td>$${data.product_price}</td>
        <td>${data.cart_product_amount}</td>
        <td>$${data.money}</td>
    </tr>
        `
    })


    arr.forEach(function(data) {

        order_ii.innerHTML = str;
        const total = document.querySelector(".total-money")
        total.innerHTML = `$&nbsp;${data.order_price}`;
    })
}

//產品總覽
// function order(arr) {
//     const order_pp = document.querySelector(".order_all")
//     let str = "";
//     //將資料存入
//     arr.forEach(function(data) {
//         //抓取欄位

//         //判斷宅配自取方式
//         let type = "";
//         var pike = "";
//         type = `${data.order_pick}`;
//         if (type === 'true') {
//             pike = "宅配";
//         } else if (type === 'false') {
//             pike = "自取";
//         }
//         str += `
//         <tr>
//         <th>訂購人姓名</th>
//         <th>訂購人電話</th>
//         <th>取貨方式</th>
//         <th>取貨地址</th>
//         <th>預計取貨時間</th>
//     </tr>
//     <tbody class="order_person">
//     <tr>
//         <td>123</td>
//         <td>123</td>
//         <td>${pike}</td>
//         <td>${data.order_address}</td>
//         <td>${data.order_order_picktime}</td>
//     </tr>
//     </tbody>

//     <th colspan="6" style="background-color:rgba(255, 250, 244, 1)" ;></th>

//     <tr>
//         <th>產品圖片</th>
//         <th>產品名稱</th>
//         <th>產品價格</th>
//         <th>產品數量</th>
//         <th>金額</th>
//     </tr>
//     <tbody class="order_body">
//         <tr>
//             <td><div class="table-img"><img src="${data.product_img}"></div></td>
//             <td>${data.product_name}&nbsp;${data.product_ml}</td>
//             <td>$${data.product_price}</td>
//             <td>${data.cart_product_amount}</td>
//             <td>${data.money}</td>
//         </tr>
//     </tbody>
//     `
//     })


//     arr.forEach(function(data) {
//         order_pp.innerHTML = str;
//         const total = document.querySelector(".total-money")
//         total.innerHTML = `$&nbsp;${data.order_price}`;
//     })
// }