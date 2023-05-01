//  用getItem抓login的資料存入login裡
var login = localStorage.getItem('login');
console.log(login);
//  如果login是null就跳轉到登入畫面(會是null是因為登出時清空了所有在localStorage裡的資料)
if(login === null){
    window.location.href = "../front/login.html";
}else{

  var welcomeadmin = localStorage.getItem('admin');
  console.log(welcomeadmin);
  var welcome = document.getElementById("welcome");
  welcome.innerHTML = "<a>"+"管理員帳號："+welcomeadmin+"　　</a>";


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
// 抓取欄位
const p_title = document.querySelector('.order_content')
let str = "";
// 將資料存入
arr.forEach(function(data){
  if(data.order_state === 0){$bb="訂單未確認"}
    else if(data.order_state === 1){$bb="訂單取消"}
    else if(data.order_state === 2){$bb="訂單確認"}
    else if(data.order_state === 3){$bb="可取貨"}
    else if(data.order_state === 4){$bb="已出貨"}
    else if(data.order_state === 5){$bb="已完成"};

    if(data.order_state === 0){//未確認
      $cc=`<input type="button" id="${data.order_id}:2" class="ok-btn mouse" value="確認">
           <input type="button" id="${data.order_id}:1" class="cancel-btn mouse" value="取消">`
    }
    else if(data.order_state === 1){//取消
      $cc=`<input type="button" id="delete${data.order_id}" class="delete-btn mouse" value="刪除">`
    }
    else if(data.order_state === 2){//確認
      if(data.order_pick === true){//宅配->訂單已出貨4
        $ddd = `<input type="button" id="${data.order_id}:4" class="out-btn mouse" value="已出貨">`;
      }else if(data.order_pick === false){//自取->訂單可取貨3
        $ddd = `<input type="button" id="${data.order_id}:3" class="out-btn mouse" value="可取貨">`;
      }
      $cc=$ddd+`<input type="button" id="${data.order_id}:1" class="cancel-btn mouse" value="取消">`
    }
    else if(data.order_state === 3){//可取貨
      $cc=`<input type="button" id="${data.order_id}:5" class="fin-btn mouse" value="完成">`
    }
    else if(data.order_state === 4){//已出貨
      $cc=`<input type="button" id="${data.order_id}:5" class="fin-btn mouse" value="完成">`
    }
    else if(data.order_state === 5){//已完成
      $cc=`<input type="button" id="delete${data.order_id}" class="delete-btn mouse" value="刪除">`
    };

  if(data.order_pick === true){//宅配->訂單可取貨4
    $ccc = "<div style='color:blue'><宅配></div>"+""+data.order_address;
    }else if(data.order_pick === false){//自取->訂單已出貨3
    $ccc = "<div style='color:green'><自取></div>"+""+data.order_address;
    }
  str += `
  <tr align="center">
  <td id="num">${data.order_id}</td>
  <td id="name">${data.order_name}</td>
  <td id="phone">${data.order_phone}</td>
  <td id="price">${data.order_price}</td>
  <td id="time">${data.order_picktime}</td>
  <td id="take">`+$ccc+`</td>
  <td id="state">`+$bb+`</td>
  <td id="more">`+$cc+`</td>
  <td id="detail"><a href="./back-order-all.html?order_id=${data.order_id}"><input type="button" class="detail-btn mouse" value="詳細資料"></a></td>
  `
})
p_title.innerHTML = str;



arr.forEach(function(data){


if((document.getElementById(`${data.order_id}`+':1')) != null){
  var cel_btn = document.getElementById(`${data.order_id}`+':1');

  function cancel(){
    var cel = document.getElementById(`${data.order_id}`+':1').id;
    // 從':'切割
    var ary1 = cel.split(':');
    console.log(ary1[0]);
    console.log(ary1[1]);

    const formData = new FormData();
// 添加文本字段
formData.append('order_id', ary1[0]);
formData.append('order_state', ary1[1]);

axios.put('https://localhost:7094/api/Order_B_', formData, {
  headers: {
    'Content-Type': 'multipart/form-data'
  }
}).then(response => {
  console.log('Response:', response);
  location.reload();
  alert("已將狀態更改為 '訂單取消'");
}).catch(error => {
  console.error('Error:', error);
});

  }
  cel_btn.addEventListener('click', cancel);
  console.log(cel_btn);
} 

if((document.getElementById(`${data.order_id}`+':2')) != null){
  var ok_btn = document.getElementById(`${data.order_id}`+':2');

  function ok(){
    var ok = document.getElementById(`${data.order_id}`+':2').id;
    // 從':'切割
    var ary2 = ok.split(':');
    console.log(ary2[0]);
    console.log(ary2[1]);

    const formData = new FormData();
// 添加文本字段
formData.append('order_id', ary2[0]);
formData.append('order_state', ary2[1]);

axios.put('https://localhost:7094/api/Order_B_', formData, {
  headers: {
    'Content-Type': 'multipart/form-data'
  }
}).then(response => {
  console.log('Response:', response);
  location.reload();
  alert("已將狀態更改為 '訂單已確認'");
}).catch(error => {
  console.error('Error:', error);
});

  }
  ok_btn.addEventListener('click', ok);
  console.log(ok_btn);
} 

if((document.getElementById(`${data.order_id}`+':3')) != null){
  var take_btn = document.getElementById(`${data.order_id}`+':3');

  function take(){
    var take = document.getElementById(`${data.order_id}`+':3').id;
    // 從':'切割
    var ary3 = take.split(':');
    console.log(ary3[0]);
    console.log(ary3[1]);

    const formData = new FormData();
// 添加文本字段
formData.append('order_id', ary3[0]);
formData.append('order_state', ary3[1]);

axios.put('https://localhost:7094/api/Order_B_', formData, {
  headers: {
    'Content-Type': 'multipart/form-data'
  }
}).then(response => {
  console.log('Response:', response);
  location.reload();
  alert("已將狀態更改為 '訂單可取貨'");
}).catch(error => {
  console.error('Error:', error);
});

  }
  take_btn.addEventListener('click', take);

  console.log(take_btn);
} 

if((document.getElementById(`${data.order_id}`+':4')) != null){
  var out_btn = document.getElementById(`${data.order_id}`+':4');

  function out(){
    var out = document.getElementById(`${data.order_id}`+':4').id;
    // 從':'切割
    var ary4 = out.split(':');
    console.log(ary4[0]);
    console.log(ary4[1]);

    const formData = new FormData();
// 添加文本字段
formData.append('order_id', ary4[0]);
formData.append('order_state', ary4[1]);

axios.put('https://localhost:7094/api/Order_B_', formData, {
  headers: {
    'Content-Type': 'multipart/form-data'
  }
}).then(response => {
  console.log('Response:', response);
  location.reload();
  alert("已將狀態更改為 '訂單已出貨'");
}).catch(error => {
  console.error('Error:', error);
});

  }
  out_btn.addEventListener('click', out);

  console.log(out_btn);
} 

if((document.getElementById(`${data.order_id}`+':5')) != null){
  var fin_btn = document.getElementById(`${data.order_id}`+':5');

  function fin(){
    var fin = document.getElementById(`${data.order_id}`+':5').id;
    // 從':'切割
    var ary5 = fin.split(':');
    console.log(ary5[0]);
    console.log(ary5[1]);

    const formData = new FormData();
// 添加文本字段
formData.append('order_id', ary5[0]);
formData.append('order_state', ary5[1]);

axios.put('https://localhost:7094/api/Order_B_', formData, {
  headers: {
    'Content-Type': 'multipart/form-data'
  }
}).then(response => {
  console.log('Response:', response);
  location.reload();
  alert("已將狀態更改為 '訂單已完成'");
}).catch(error => {
  console.error('Error:', error);
});

  }
  fin_btn.addEventListener('click', fin);

  console.log(fin_btn);
}


if((document.getElementById('delete'+`${data.order_id}`)) != null){
var button1 = document.getElementById('delete'+`${data.order_id}`)
function aaaa(){
  // confirm('確認要刪除嗎？');
  if (confirm("您即將刪除:\n\n訂單編號："+`${data.order_id}`+"\n取貨姓名："+`${data.order_name}`+"\n連絡電話："+`${data.order_phone}`+"\n訂單金額："+`${data.order_price}`+"\n取貨/送貨時間："+`${data.order_picktime}`+"\n\n確認要刪除嗎？？") == true) {
      // console.log(aas.value);

    var bbb = 'https://localhost:7094/api/Order_B_?order_id='+`${data.order_id}`;
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
    alert('您已取消刪除');
  }
}
button1.addEventListener('click', aaaa);
}



})

}
  // )}




//    搜尋列表    搜尋列表    搜尋列表    搜尋列表    搜尋列表    搜尋列表    搜尋列表    //
var search = document.getElementById('search');
var search_btn = document.getElementById('search_btn');

function search1(){

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
      var error = document.getElementById('error')
      var table = document.getElementById('table')
      if(data1.length === 0){
        error.innerHTML="查無此資料";
        document.getElementById("table").style.display = "none";
      return false;
      }else{
        error.innerHTML="";
        document.getElementById("table").style.display = "";
        return title1(data1);;
      }
      title1(data1);
    })
})();

function title1(arr1) {
  // 抓取欄位
  const p_title1 = document.querySelector('.order_content')
  let str1 = "";
  // 將資料存入
  arr1.forEach(function(data1){
    if(data1.order_state === 0){$bb="訂單未確認"}
      else if(data1.order_state === 1){$bb="訂單取消"}
      else if(data1.order_state === 2){$bb="訂單確認"}
      else if(data1.order_state === 3){$bb="可取貨"}
      else if(data1.order_state === 4){$bb="已出貨"}
      else if(data1.order_state === 5){$bb="已完成"};
  
      if(data1.order_state === 0){//未確認
        $cc=`<input type="button" id="${data1.order_id}:2" class="ok-btn mouse" value="確認">
             <input type="button" id="${data1.order_id}:1" class="cancel-btn mouse" value="取消">`
      }
      else if(data1.order_state === 1){//取消
        $cc=`<input type="button" id="delete${data1.order_id}" class="delete-btn mouse" value="刪除">`
      }
      else if(data1.order_state === 2){//確認
        if(data1.order_pick === true){//宅配->訂單可取貨4
          $ddd = `<input type="button" id="${data1.order_id}:4" class="out-btn mouse" value="出貨">`;
        }else if(data1.order_pick === false){//自取->訂單已出貨3
          $ddd = `<input type="button" id="${data1.order_id}:3" class="out-btn mouse" value="出貨">`;
        }
        $cc=$ddd+`<input type="button" id="${data1.order_id}:1" class="cancel-btn mouse" value="取消">`
      }
      else if(data1.order_state === 3){//可取貨
        $cc=`<input type="button" id="${data1.order_id}:5" class="fin-btn mouse" value="完成">`
      }
      else if(data1.order_state === 4){//已出貨
        $cc=`<input type="button" id="${data1.order_id}:5" class="fin-btn mouse" value="完成">`
      }
      else if(data1.order_state === 5){//已完成
        $cc=`<input type="button" id="delete${data1.order_id}" class="delete-btn mouse" value="刪除">`
      };
  
    if(data1.order_pick === true){//宅配->訂單可取貨4
      $ccc = "<div style='color:blue'>宅配</div>"+""+data1.order_address;
      }else if(data1.order_pick === false){//自取->訂單已出貨3
      $ccc = "<div style='color:green'>自取</div>"+""+data1.order_address;
      }
    str1 += `
    <tr align="center">
    <td id="num">${data1.order_id}</td>
    <td id="name">${data1.order_name}</td>
    <td id="phone">${data1.order_phone}</td>
    <td id="price">${data1.order_price}</td>
    <td id="time">${data1.order_picktime}</td>
    <td id="take">`+$ccc+`</td>
    <td id="state">`+$bb+`</td>
    <td id="more">`+$cc+`</td>
    <td id="detail"><a href="./back-order-all.html?order_id=${data1.order_id}"><input type="button" class="detail-btn mouse" value="詳細資料"></a></td>
    `
  })
  p_title1.innerHTML = str1;
  
  
  
  arr1.forEach(function(data1){
  
  
  if((document.getElementById(`${data1.order_id}`+':1')) != null){
    var cel_btn = document.getElementById(`${data1.order_id}`+':1');
  
    function cancel(){
      var cel = document.getElementById(`${data1.order_id}`+':1').id;
      // 從':'切割
      var ary1 = cel.split(':');
      console.log(ary1[0]);
      console.log(ary1[1]);
  
      const formData = new FormData();
  // 添加文本字段
  formData.append('order_id', ary1[0]);
  formData.append('order_state', ary1[1]);
  
  axios.put('https://localhost:7094/api/Order_B_', formData, {
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  }).then(response => {
    console.log('Response:', response);
    location.reload();
    alert("已將狀態更改為 '訂單取消'");
  }).catch(error => {
    console.error('Error:', error);
  });
  
    }
    cel_btn.addEventListener('click', cancel);
    console.log(cel_btn);
  } 
  
  if((document.getElementById(`${data1.order_id}`+':2')) != null){
    var ok_btn = document.getElementById(`${data1.order_id}`+':2');
  
    function ok(){
      var ok = document.getElementById(`${data1.order_id}`+':2').id;
      // 從':'切割
      var ary2 = ok.split(':');
      console.log(ary2[0]);
      console.log(ary2[1]);
  
      const formData = new FormData();
  // 添加文本字段
  formData.append('order_id', ary2[0]);
  formData.append('order_state', ary2[1]);
  
  axios.put('https://localhost:7094/api/Order_B_', formData, {
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  }).then(response => {
    console.log('Response:', response);
    location.reload();
    alert("已將狀態更改為 '訂單已確認'");
  }).catch(error => {
    console.error('Error:', error);
  });
  
    }
    ok_btn.addEventListener('click', ok);
    console.log(ok_btn);
  } 
  
  if((document.getElementById(`${data1.order_id}`+':3')) != null){
    var take_btn = document.getElementById(`${data1.order_id}`+':3');
  
    function take(){
      var take = document.getElementById(`${data1.order_id}`+':3').id;
      // 從':'切割
      var ary3 = take.split(':');
      console.log(ary3[0]);
      console.log(ary3[1]);
  
      const formData = new FormData();
  // 添加文本字段
  formData.append('order_id', ary3[0]);
  formData.append('order_state', ary3[1]);
  
  axios.put('https://localhost:7094/api/Order_B_', formData, {
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  }).then(response => {
    console.log('Response:', response);
    location.reload();
    alert("已將狀態更改為 '訂單取消'");
  }).catch(error => {
    console.error('Error:', error);
  });
  
    }
    take_btn.addEventListener('click', take);
  
    console.log(take_btn);
  } 
  
  if((document.getElementById(`${data1.order_id}`+':4')) != null){
    var out_btn = document.getElementById(`${data1.order_id}`+':4');
  
    function out(){
      var out = document.getElementById(`${data1.order_id}`+':4').id;
      // 從':'切割
      var ary4 = out.split(':');
      console.log(ary4[0]);
      console.log(ary4[1]);
  
      const formData = new FormData();
  // 添加文本字段
  formData.append('order_id', ary4[0]);
  formData.append('order_state', ary4[1]);
  
  axios.put('https://localhost:7094/api/Order_B_', formData, {
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  }).then(response => {
    console.log('Response:', response);
    location.reload();
    alert("已將狀態更改為 '訂單可取貨/已出貨'");
  }).catch(error => {
    console.error('Error:', error);
  });
  
    }
    out_btn.addEventListener('click', out);
  
    console.log(out_btn);
  } 
  
  if((document.getElementById(`${data1.order_id}`+':5')) != null){
    var fin_btn = document.getElementById(`${data1.order_id}`+':5');
  
    function fin(){
      var fin = document.getElementById(`${data1.order_id}`+':5').id;
      // 從':'切割
      var ary5 = fin.split(':');
      console.log(ary5[0]);
      console.log(ary5[1]);
  
      const formData = new FormData();
  // 添加文本字段
  formData.append('order_id', ary5[0]);
  formData.append('order_state', ary5[1]);
  
  axios.put('https://localhost:7094/api/Order_B_', formData, {
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  }).then(response => {
    console.log('Response:', response);
    location.reload();
    alert("已將狀態更改為 '訂單已完成'");
  }).catch(error => {
    console.error('Error:', error);
  });
  
    }
    fin_btn.addEventListener('click', fin);
  
    console.log(fin_btn);
  }
  
  
  if((document.getElementById('delete'+`${data1.order_id}`)) != null){
  var button1 = document.getElementById('delete'+`${data1.order_id}`)
  function aaaa(){
    // confirm('確認要刪除嗎？');
    if (confirm("您即將刪除:\n\n訂單編號："+`${data1.order_id}`+"\n取貨姓名："+`${data1.order_name}`+"\n連絡電話："+`${data1.order_phone}`+"\n訂單金額："+`${data1.order_price}`+"\n取貨/送貨時間："+`${data1.order_picktime}`+"\n\n確認要刪除嗎？？") == true) {
        // console.log(aas.value);
  
      var bbb = 'https://localhost:7094/api/Order_B_?order_id='+`${data1.order_id}`;
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
      alert('您已取消刪除');
    }
  }
  button1.addEventListener('click', aaaa);
  }
  
  
  
  })
  
  }
}
search_btn.addEventListener('click', search1);
search.addEventListener('change', search1);
search.addEventListener('blur', search1);
search.addEventListener('keyup', search1);





//登出
var logout1 = document.getElementById('logout')
function logout(){
window.location.href = "../front/login.html";
localStorage.clear();
}
logout1.addEventListener('click', logout);



}