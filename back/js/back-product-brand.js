  // //  用getItem抓login的資料存入login裡
// var login = localStorage.getItem('login');
// console.log(login);
// //  如果login是null就跳轉到登入畫面(會是null是因為登出時清空了所有在localStorage裡的資料)
// if(login === null){
//     window.location.href = "../front/login.html";
// }else{
  
  
  
  
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
              <div id="show">${data.brand_eng}</div>
              </td>
              <td>
              <div class="buttonflex">
              <input type="button" id="update${data.brand_id}" class="update mouse" value="修改"></a>
              </div>
              </td>
          </tr>
      `
    })
    p_title.innerHTML = str;


    arr.forEach(function(data){
      var button = document.getElementById('update'+`${data.brand_id}`)
      console.log(button)
      function popup3(e) {
          var chi = window.prompt('編輯品牌(中文名)',`${data.brand_name}`);
          var eng = window.prompt('編輯品牌(英文名)',`${data.brand_eng}`);
          if (chi === null || chi === "" || eng === null || eng === "") {
              alert('請勿輸入空值!!')
          } else {
  
  
  var id = `${data.brand_id}`;
  console.log(chi);
  console.log(eng);
  console.log(id);
  
  const formData = new FormData();
  // 添加文本字段
  formData.append('brand_id', id);
  // formData.append('brand_name', chi);
  if(chi === `${data.brand_name}`){
    formData.append('brand_name', null);
  }else if(chi != `${data.brand_name}`){
    formData.append('brand_name', chi);
  }
  
  if(eng === `${data.brand_eng}`){
    formData.append('brand_eng', null);
  }else if(eng != `${data.brand_eng}`){
    formData.append('brand_eng', eng);
  }
  
  axios.put(url, formData, {
    headers: {
      'Content-Type': 'application/json'
    }
  }).then(response => {
    console.log('Response:', response);
    location.reload();
    alert('編輯品牌\n您編輯品牌為：'+ chi+eng)
  }).catch(error => {
    console.error('Error:', error);
  });
          }
      }
      button.addEventListener('click', popup3);
    })
  
  
  
  var button1 = document.getElementById('insert1')
    function popup4(e) {
        var chi1 = window.prompt('新增品牌(中文)',);
        var eng1 = window.prompt('新增品牌(英文)',);
        if (chi1 === null || chi1 === "" || eng1 === null || eng1 === "") {
            alert('請勿輸入空值!!')
        } else {
  
          console.log(chi1);
          console.log(eng1);
          
          const formData = new FormData();
          // 添加文本字段
          formData.append('brand_name', chi1);
          formData.append('brand_eng', eng1);
          
          axios.post(url, formData, {
            headers: {
              'Content-Type': 'application/json'
            }
          }).then(response => {
            console.log('Response:', response);
            location.reload();
            alert('新增品牌\n您新增品牌為：'+ chi1+eng1)
          }).catch(error => {
            console.error('Error:', error);
          });
                  }
              }
          button1.addEventListener('click', popup4);



  }



// }
