//  用getItem抓login的資料存入login裡
var login = localStorage.getItem('login');
console.log(login);
//  如果login是null就跳轉到登入畫面(會是null是因為登出時清空了所有在localStorage裡的資料)
if (login === null) {
    window.location.href = "../front/login.html";
} else {

    var welcomeadmin = localStorage.getItem('admin');
    console.log(welcomeadmin);
    var welcome = document.getElementById("welcome");
    welcome.innerHTML = "<a>" + "管理員帳號：" + welcomeadmin + "　　</a>";


    //JSON 檔案網址
    const url = "https://localhost:7094/api/Store";
    let data = [];
    // step 1 - 取得資料
    (function getData() {
        axios.get(url)
            .then(function(response) {
                // 檢查
                console.log(response.data);
                // 將取得資料帶入空陣列data中
                data = response.data;
                title(data);
            })
    })();

    function title(arr) {
        // 抓取欄位
        const p_title = document.querySelector('.content')
        let str = "";
        // 將資料存入
        arr.forEach(function(data) {
            str += `
    <div class="stronghold-content">
    <div class="stronghold-top">
                <div class="stronghold-top-left"><img src="${data.store_img}"></div>
                <div class="stronghold-top-right">
                    <div class="title"><a>${data.store_name}</a></div>
                    <div class="address"><a>地址：${data.store_address}</a></div>
                    <div class="tel"><a>電話：${data.store_phone}</a></div>
                    <div class="email"><a>信箱：${data.store_email}</a></div>
                    <div class="time"><a>營業時間：${data.store_time}</a></div>
                </div>
            </div>
            <div class="stronghold-bottom">
                <a href="./back-store-edit.html?store_id=${data.store_id}"><input type="button" class="edit-btn mouse" value="編輯"></a>
                <input type="button" id="delete${data.store_id}" class="delete-btn mouse" value="刪除">
                <input type="hidden" id="hidden${data.store_id}" value="${data.store_name}">
            </div>
            </div>
    `
        })
        p_title.innerHTML = str;


        arr.forEach(function(data) {
            var button = document.getElementById('delete' + `${data.store_id}`)
            var aas = document.getElementById('hidden' + `${data.store_id}`)

            function aa(e) {
                // confirm('確認要刪除嗎？');
                if (confirm('您即將刪除 "' + aas.value + '" 的資料,確認要刪除嗎？') == true) {
                    // console.log(aas.value);

                    var bbb = 'https://localhost:7094/api/Store?store_id=' + `${data.store_id}`;
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
                    location.reload(); //網頁重新整理
                } else {
                    // alert('您已取消刪除');
                }
            }
            button.addEventListener('click', aa);
        })

    }



    //登出
    var logout1 = document.getElementById('logout')

    function logout() {
        if (confirm('確認要登出嗎？') == true) {
            window.location.href = "../front/login.html";
            localStorage.clear();
        } else {

        }
    }
    logout1.addEventListener('click', logout);

}