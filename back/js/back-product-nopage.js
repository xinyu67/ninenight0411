//JSON 檔案網址
const url = "https://localhost:7094/api/Product";
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
  // 抓取欄位
  const p_title = document.querySelector('.product-content')
  let str = "";
  // 將資料存入
  arr.forEach(function(data){
    str += `
    <div class="pro">
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
    </div>
    `
    console.log(`${data.product_img}`);
  })
  p_title.innerHTML = str;
  

arr.forEach(function(data){
var button = document.getElementById('delete'+`${data.product_id}`)
var aas = document.getElementById('hidden'+`${data.product_id}`)
function aa(e){
  // confirm('確認要刪除嗎？');
  if (confirm('您即將刪除 "'+aas.value+'" 的資料,確認要刪除嗎？') == true) {
      // console.log(aas.value);

    var bbb = 'https://localhost:7094/api/Product?product_id='+`${data.product_id}`;
    console.log(bbb);
    fetch(bbb, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ reason: 'no longer needed' })
    })
    .then(response => {
      if (response.ok) {
        console.log('Resource deleted successfully.');
      } else {
        console.error('Failed to delete resource:', response.status);
      }
    })
    .catch(error => console.error('Error:', error));

    alert('刪除成功');
    location.reload();//網頁重新整理
  } else {
    // alert('您已取消刪除');
  }
}
button.addEventListener('click', aa);
})



}

