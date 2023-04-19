// JSON 檔案網址
const url = "https://localhost:7094/api/Product";
const productsList = document.querySelector(".showList");
let data = [];

/** 步驟一 取得資料**/
function getData() {
    axios.get(url)
        .then(function(response) {
            // 檢查： 
            console.log(response.data);
            // 將取得資料帶入空陣列 data 中
            data = response.data;

            // 執行渲染 (請留意非同步)
            renderData(data);
        })
}
getData();


/** 步驟二 渲染**/
function renderData(arr) {
    // 宣告空字串，可存入資料
    let str = "";

    //請透過 data 陣列跑 forEach ，並至少帶入第一個參數
    arr.forEach(function(item) {
            str += `
        <tr>
        <td>${item.product_name}</td>
        <td>${item.product_eng}</td>
        <td>${item.product_price}</td>
        <td>${item.product_content}</td>
        </tr>`
        })
        // 檢查：console.log(str);
    productsList.innerHTML = str;
}