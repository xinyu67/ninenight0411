//  用getItem抓login的資料存入login裡
var login = localStorage.getItem("login");
console.log(login);
//  如果login是null就跳轉到登入畫面(會是null是因為登出時清空了所有在localStorage裡的資料)
if (login === null) {
  window.location.href = "../front/login.html";
  //   var hello = document.getElementById("login1");
  //   var hello2 = document.getElementById("login2");
  //   hello.style.display = "flex";
  //   hello2.style.display = "none";
} else {
  var hello = document.getElementById("login1");
  var hello2 = document.getElementById("login2");
  if ((user != "") & (login != null)) {
    var useer = document.getElementById("useer");
    var user = localStorage.getItem("user");
    var user_id = localStorage.getItem("user_id");
    console.log(user);
    hello.style.display = "none";
    hello2.style.display = "flex";
    useer.innerHTML = user;
  }

  // JSON 檔案網址
  const url = "https://localhost:7094/api/User/F_All?user_id=" + user_id;
  let data = [];
  /** 步驟一 取得資料**/
  (function getData() {
    axios.get(url).then(function (response) {
      // 檢查：
      console.log(response.data);
      // 將取得資料帶入空陣列 data 中
      data = response.data;
      order(data);
    });
  })();

  //最新消息總覽
  function order(arr) {
    //抓取欄位
    const order_all = document.querySelector(".login-content");
    let str = "";

    //將資料存入
    arr.forEach(function (data) {
      const user_birthday = `${data.user_birthday}`
        .split(" ")[0]
        .replace(/\//g, "-"); // 取出日期部分
      const [year, month, day] = user_birthday.split("-");
      const formattedDate = `${year}-${month.padStart(2, "0")}-${day.padStart(
        2,
        "0"
      )}`;

      str += `
        <div class="login-user ">帳號：<input type="text" class="readonly" value="${
          data.user_account
        }" readonly></div>
        <div class="login-user">姓名：<input type="text" class="user-textbox" value="${
          data.user_name
        }" required></div>
        <div class="sex-bir">
            <div class="radio-user">性別：
                <div class="aaa">
                    <input type="radio" name="sex" style="ZOOM:1.5" id="sex1" class="readonly" value='0' ${
                      data.user_gender === 0 ? "checked" : ""
                    }><label for="sex">男　</label>
                    <input type="radio" name="sex" style="ZOOM:1.5" id="sex1" class="readonly" value='1' ${
                      data.user_gender === 1 ? "checked" : ""
                    }><label for="sex1">女</label>
                </div>
            </div>
            <div class="bir-user">生日：
                <input type="date" id="start" name="trip-start" min="1900-01-01" max="2023-12-31" class="readonly" value='${formattedDate}' readonly> </div>
            </div>
        <div class="login-user">信箱：<input type="text" class="readonly" value="${
          data.user_email
        }" readonly></div>
        <div class="login-user">電話：<input type="text" class="readonly" value="${
          data.user_phone
        }" readonly></div>
        <div class="login-user">地址：<input type="text" class="address user-textbox" value="${
          data.user_address
        }" required></div>
        <div class="buttons"><input type="button" onclick="fix()" class="down mouse" value="確認修改"></div>
        `;
    });
    order_all.innerHTML = str;
  }

  function fix() {
    const name = document.querySelector(".user-textbox").value; //姓名
    const sex = document.querySelector('input[name="sex"]:checked').value;
    const address = document.querySelector(".address").value;
    fetch(`https://localhost:7094/api/User/F_Put`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        //Authorization: `Bearer ${user_id}`,
      },
      body: JSON.stringify({
        user_id: user_id,
        user_name: name,
        user_gender: sex,
        user_address: address,
      }),
    })
      .then((response) => {
        return response.json();
      })
      .then((data) => {
        console.log(data);
        document.querySelector(
          'input[name="sex"][value="' + sex + '"]'
        ).checked = true;
        alert("修改成功!!");
        location.reload();
      })
      .catch((error) => {
        console.error(error);
      });
  }
  //登出
  var logout1 = document.getElementById("logout");

  function logout() {
    window.location.href = "../front/login.html";
    localStorage.clear();
  }
  logout1.addEventListener("click", logout);
}
