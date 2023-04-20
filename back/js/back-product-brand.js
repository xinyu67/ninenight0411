// window.onload = function(){

//     var hidden1 = document.getElementById('hidden1')
//     var insert1button = document.getElementById('insert1')
//     var okbutton = document.getElementById('ok')
//     var reset = document.getElementById('reset')
  
//         hidden1.style.display = "none";
//         // hiddentext.style.display = "none";
        
//         insert1button.addEventListener("click",function(){
//             hidden1.style.display = "table-row";
//         });
        
//         // update.addEventListener("click",function(){
//         //   hiddentext.style.display = "flex";
//         // });
  
//         reset.addEventListener("click",function(){
//             if(hidden1.style.display = "table-row"){
//                 hidden1.style.display = "none";
//             }else if(hidden1.style.display = "table-row"){
//                 hidden1.style.display = "none";
//             }
//         });
  
//         okbutton.addEventListener("click",function(){
//             if(hidden1.style.display == "table-row", place.value != ""){
//                 alert(place.value);
//             }
//         });
  
//     }
  
  
  //JSON 檔案網址
  const url = "https://localhost:7094/api/brand";
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
    const p_title = document.querySelector('.place1')
    let str = "";
    // 將資料存入
    arr.forEach(function(data){
      str += `
          <tr align="center">
              <td>
              <div id="show">${data.brand_name}</div>
              </td>
              <td>
              <div class="buttonflex">
              <input type="button" id="update${data.brand_id}" class="update mouse" value="修改"></a>
              <input type="button" id="delete" class="delete mouse" value="刪除"></a>
              </div>
              </td>
          </tr>
      `
    })
    p_title.innerHTML = str;

    arr.forEach(function(data){
        var button = document.getElementById('update'+`${data.brand_id}`)
        var show = document.getElementById('show')
        console.log(`${data.brand_name}`)
        function popup3(e) {
            var guest = window.prompt('編輯品牌',`${data.brand_name}`);
            if (guest == null || "") {
                alert('您已取消編輯')
            } else {
                alert('您編輯品牌為：'+ guest)
                show.innerHTML =  guest 
            }
        }
        button.addEventListener('click', popup3);
      })
  }
  
  var button1 = document.getElementById('insert1')
  var show = document.getElementById('show')
  function popup3(e) {
      var guest = window.prompt('新增品牌',);
      if (guest == null || "") {
          alert('您已取消新增')
      } else {
          alert('您新增品牌為：'+ guest)
          location.reload();
      }
  }
  button1.addEventListener('click', popup3);   