// //  用getItem抓login的資料存入login裡
// var login = localStorage.getItem('login');
// console.log(login);
// //  如果login是null就跳轉到登入畫面(會是null是因為登出時清空了所有在localStorage裡的資料)
// if(login === null){
//     window.location.href = "../front/login.html";
// }else{




//JSON 檔案網址
const url = "https://localhost:7094/api/Order_B_";
let data = [];
// step 1 - 取得資料
(function getData(){
  axios.get(url)
    .then(function(response){
      // 檢查
      console.log(response.data);
      // 將取得資料帶入空陣列data中
      data = response.data;
      title(data);
    })
})();
function title(arr) {
    var num = document.getElementById('num')
    var name = document.getElementById('name')
    var phone = document.getElementById('phone')
    var price = document.getElementById('price')
    var time = document.getElementById('time')
    var take = document.getElementById('take')
    var state = document.getElementById('state')
    var remoreset = document.getElementById('more')
    var detail = document.getElementById('detail')
  
    if(data[0].order_state === 0){$bb="訂單未確認"}
    else if(data[0].order_state === 1){$bb="訂單取消"}
    else if(data[0].order_state === 2){$bb="訂單確認"}
    else if(data[0].order_state === 3){$bb="可取貨"}
    else if(data[0].order_state === 4){$bb="已出貨"}
    else if(data[0].order_state === 5){$bb="已完成"};

    if(data[0].order_state === 0){$cc=`<input type="button" class="fin-btn mouse" value="完成">`}
    else if(data[0].order_state === 1){$cc=`<input type="button" class="ok-btn mouse" value="確認">
    <input type="button" class="cancel-btn mouse" value="取消">`}
    else if(data[0].order_state === 2){$cc=`<input type="button" class="cancel-btn mouse" value="取消">
    <input type="button" class="out-btn mouse" value="出貨">`}
    else if(data[0].order_state === 3){$cc=`<input type="button" class="fin-btn mouse" value="完成">`}
    else if(data[0].order_state === 4){$cc=`<input type="button" class="delete-btn mouse" value="刪除">`}
    else if(data[0].order_state === 5){$cc=`<input type="button" class="delete-btn mouse" value="刪除">`};

    num.innerHTML = data[0].order_id;
    name.innerHTML = data[0].order_name;
    phone.innerHTML = data[0].order_phone;
    price.innerHTML = data[0].order_price;
    time.innerHTML = data[0].order_picktime;
    take.innerHTML = data[0].order_pick+","+data[0].order_address;
    state.innerHTML = $bb;
    remoreset.innerHTML = $cc;
    detail.innerHTML = `<a href="./back-order-all.html?order_id=${data.order_id}"><input type="button" class="detail-btn mouse" value="詳細資料"></a>`;
}


//    搜尋列表    搜尋列表    搜尋列表    搜尋列表    搜尋列表    搜尋列表    搜尋列表    //
var search = document.getElementById('search');
var search_btn = document.getElementById('search_btn');

function search1(){
if(search.value === "" || search.value === null){
    alert("搜尋列不得為空");
    return false;
}
//JSON 檔案網址
const url1 = "https://localhost:7094/api/Order_B_?search="+search.value;
console.log(url1);
let data1 = [];
// step 1 - 取得資料
(function getData(){
  axios.get(url1)
    .then(function(response){
      // 檢查
      console.log(response.data);
      // 將取得資料帶入空陣列data中
      data1 = response.data;
      title1(data1);

    })
})();
// function title1(arr) {
// console.log(data1.length);
// var error = document.getElementById('error')
// var table = document.getElementById('table')
// if(data1[0].order_id === ""){
//     error.innerHTML="查無此資料";
//     table.innerHTML="";
// }
// }

function title1(arr1) {
    var num = document.getElementById('num')
    var name = document.getElementById('name')
    var phone = document.getElementById('phone')
    var price = document.getElementById('price')
    var time = document.getElementById('time')
    var take = document.getElementById('take')
    var state = document.getElementById('state')
    var remoreset = document.getElementById('more')
    var detail = document.getElementById('detail')
  
    if(data1[0].order_state === 0){$bb="訂單未確認"}
    else if(data1[0].order_state === 1){$bb="訂單取消"}
    else if(data1[0].order_state === 2){$bb="訂單確認"}
    else if(data1[0].order_state === 3){$bb="可取貨"}
    else if(data1[0].order_state === 4){$bb="已出貨"}
    else if(data1[0].order_state === 5){$bb="已完成"};

    if(data1[0].order_state === 0){$cc=`<input type="button" class="fin-btn mouse" value="完成">`}
    else if(data1[0].order_state === 1){$cc=`<input type="button" class="ok-btn mouse" value="確認">
    <input type="button" class="cancel-btn mouse" value="取消">`}
    else if(data1[0].order_state === 2){$cc=`<input type="button" class="cancel-btn mouse" value="取消">
    <input type="button" class="out-btn mouse" value="出貨">`}
    else if(data1[0].order_state === 3){$cc=`<input type="button" class="fin-btn mouse" value="完成">`}
    else if(data1[0].order_state === 4){$cc=`<input type="button" class="delete-btn mouse" value="刪除">`}
    else if(data1[0].order_state === 5){$cc=`<input type="button" class="delete-btn mouse" value="刪除">`};

    num.innerHTML = data1[0].order_id;
    name.innerHTML = data1[0].order_name;
    phone.innerHTML = data1[0].order_phone;
    price.innerHTML = data1[0].order_price;
    time.innerHTML = data1[0].order_picktime;
    take.innerHTML = data1[0].order_pick+","+data[0].order_address;
    state.innerHTML = $bb;
    remoreset.innerHTML = $cc;
    detail.innerHTML = `<a href="./back-order-all.html?order_id=${data.order_id}"><input type="button" class="detail-btn mouse" value="詳細資料"></a>`;
}
}
search_btn.addEventListener('click', search1);


// }