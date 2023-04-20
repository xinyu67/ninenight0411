// JSON 檔案網址
const url = "https://localhost:7094/api/Store";
let data = [];
/** 步驟一 取得資料**/
(function getData() {
    axios.get(url)
        .then(function(response) {
            // 檢查：  
            console.log(response.data);
            // 將取得資料帶入空陣列 data 中
            data = response.data;
            store(data);
        })
})();

//產品總覽
function store(arr) {
    //抓取欄位
    const store_all = document.querySelector('.content-body')
    let str = "";
    //將資料存入
    arr.forEach(function(data) {
        str += `
        <div class="left">
        <div class="l-img">
            <img src="./img/store1.jpg">
        </div>
        <div class="l-text">
            <div class="title">
                ${data.store_name}
            </div>
            <div class="down">
                <div class="down-content"><i class="fa-solid fa-location-dot"></i>${data.store_address}</div>
                <div class="down-content"><i class="fa-solid fa-phone"></i>${data.store_phone}</div>
                <div class="down-content"><i class="fa-solid fa-envelope"></i>${data.store_email}</div>
                <div class="down-content"><i class="fa-solid fa-clock"></i>${data.store_time}</div>
            </div>
        </div>

    </div>
        
    
    
    
    `
    })
    store_all.innerHTML = str;
}