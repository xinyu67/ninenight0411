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
    <div class="pro-top"><img class="head" src="./img/product/p1.png"></div>
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
                <input type="button" class="stop-btn mouse" value="刪除">
            </div>
        </div>
    </div>
    </div>
    `
    
  })
  p_title.innerHTML = str;
  console.log(data[0].product_name);
}
