// //  用getItem抓login的資料存入login裡
// var login = localStorage.getItem('login');
// console.log(login);
// //  如果login是null就跳轉到登入畫面(會是null是因為登出時清空了所有在localStorage裡的資料)
// if(login === null){
//     window.location.href = "../front/login.html";
// }else{




//JSON 檔案網址
const url = "https://localhost:7094/api/User";
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
  const p_title = document.querySelector('.uuser')
  let str = "";
  // 將資料存入
  arr.forEach(function(data){
    str += `
    <tr align="center">
    <td>${data.user_account}</td>
    <td>${data.user_name}</td>
    if(${data.user_level} === False){<td>管理者</td>}else if(${data.user_level} === Ture){<td>會員</td>}
    if(${data.user_level} === False){<td>-</td>}else if(${data.user_level} === Ture){
        if(${data.isdel} === False){
            <td>
                <input type="button" class="stop-btn mouse" value="停用">
            </td>
        }else if(${data.isdel} === Ture){
            <td>
            <input type="button" class="start-btn mouse" value="啟用">
            </td>
        }
    }
    </tr>
    `
    
  })
  p_title.innerHTML = str;
  console.log(data[0].user_level);
}




// }