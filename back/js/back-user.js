//  用getItem抓login的資料存入login裡
var login = localStorage.getItem("login");
console.log(login);
//  如果login是null就跳轉到登入畫面(會是null是因為登出時清空了所有在localStorage裡的資料)
if (login === null) {
  window.location.href = "../front/login.html";
} else {
  var welcomeadmin = localStorage.getItem("admin");
  var user_id = localStorage.getItem("user_id");
  console.log(welcomeadmin);
  var welcome = document.getElementById("welcome");
  welcome.innerHTML = "<a>" + "管理員帳號：" + welcomeadmin + "　　</a>";

  //JSON 檔案網址
  const url = "https://localhost:7094/api/User/B_All";
  let data = [];
  // step 1 - 取得資料
  (function getData() {
    axios.get(url).then(function (response) {
      // 檢查
      console.log(response.data);
      // 將取得資料帶入空陣列data中
      data = response.data;
      title(data);
    });
  })();

  function title(arr) {
    // 抓取欄位
    const p_title = document.querySelector(".uuser");
    let str = "";
    // 將資料存入
    arr.forEach(function (data) {
      if (data.user_level === false) {
        var $aaa = "管理者";
      } else if (data.user_level === true) {
        var $aaa = "會員";
      }
      if (data.user_level === false) {
        $bbb = `--`;
      } else if (data.user_level === true) {
        if (data.user_start === 1) {
          $bbb = `<input type="button" id="stop${data.user_id}" class="stop-btn mouse" value="停用">`;
        } else if (data.user_start === 2) {
          $bbb = `<input type='button' id="start${data.user_id}" class='start-btn mouse' value='啟用'>`;
        } else if (data.user_start === 3) {
          $bbb = `尚未驗證信箱`;
        }
      }
      str +=
        `
    <tr align="center">
    <td>${data.user_account}</td>
    <td>${data.user_name}</td>
    <td>` +
        $aaa +
        `</td>
    <td>` +
        $bbb +
        `</td>
    </tr>
    `;
    });
    p_title.innerHTML = str;

    arr.forEach(function (data) {
      if (data.user_level === false) {
        var $aaa = "管理者";
      } else if (data.user_level === true) {
        var $aaa = "會員";
      }

      if (document.getElementById("stop" + `${data.user_id}`) != null) {
        var stop1 = document.getElementById("stop" + `${data.user_id}`);

        function stop() {
          const formData = new FormData();
          // 添加文本字段
          formData.append("user_id", `${data.user_id}`);
          formData.append("user_start", 2);

          axios
            .put(
              "https://localhost:7094/api/User/B_PUT",
              formData,

              {
                headers: {
                  "Content-Type": "application/json",
                },
              }
            )
            .then((response) => {
              console.log(user_id);
              console.log("Response:", response);
              // window.location.href = "back-product.html";
              location.reload();
              alert(
                "停用成功!!\n\n" +
                  "停用帳號：" +
                  `${data.user_account}` +
                  "\n停用姓名：" +
                  `${data.user_name}` +
                  "\n停用權限：" +
                  $aaa
              );
            })
            .catch((error) => {
              console.error("Error:", error);
            });
        }
        stop1.addEventListener("click", stop);
      }

      if (document.getElementById("start" + `${data.user_id}`) != null) {
        var start1 = document.getElementById("start" + `${data.user_id}`);

        function start() {
          const formData = new FormData();
          // 添加文本字段
          formData.append("user_id", `${data.user_id}`);
          formData.append("user_start", 1);

          axios
            .put("https://localhost:7094/api/User/B_PUT", formData, {
              headers: {
                "Content-Type": "application/json",
              },
            })
            .then((response) => {
              console.log("Response:", response);
              // window.location.href = "back-product.html";
              location.reload();
              alert(
                "啟用成功!!\n\n" +
                  "啟用帳號：" +
                  `${data.user_account}` +
                  "\n啟用姓名：" +
                  `${data.user_name}` +
                  "\n啟用權限：" +
                  $aaa
              );
            })
            .catch((error) => {
              console.error("Error:", error);
            });
        }
        start1.addEventListener("click", start);
      }
    });
  }

  //  -----搜尋-----  -----搜尋-----  -----搜尋-----  -----搜尋-----  -----搜尋-----  -----搜尋-----  -----搜尋-----
  var search_btn = document.getElementById("search_btn");

  function search1() {
    var search = document.getElementById("search");
    console.log(search);
    //JSON 檔案網址
    const url1 = "https://localhost:7094/api/User/B_All?search=" + search.value;
    console.log(url1);
    let data1 = [];
    // step 1 - 取得資料
    (function getData() {
      axios.get(url1).then(function (response) {
        // 檢查
        console.log(response.data);
        // 將取得資料帶入空陣列data中
        data1 = response.data;
        var error = document.getElementById("error");
        var table = document.getElementById("table");
        if (data1.length === 0) {
          error.innerHTML = "查無此資料";
          document.getElementById("table").style.display = "none";
          return false;
        } else {
          error.innerHTML = "";
          document.getElementById("table").style.display = "";
          return title1(data1);
        }
        title1(data1);
      });
    })();

    function title1(arr1) {
      // 抓取欄位
      const p_title1 = document.querySelector(".uuser");
      let str1 = "";
      // 將資料存入
      arr1.forEach(function (data1) {
        if (data1.user_level === false) {
          var $aaa = "管理者";
        } else if (data1.user_level === true) {
          var $aaa = "會員";
        }

        if (data1.user_level === false) {
          $bbb = `--`;
        } else if (data1.user_level === true) {
          if (data1.user_start === 1) {
            $bbb = `<input type="button" id="stop1${data1.user_id}" class="stop-btn mouse" value="停用">`;
          } else if (data1.user_start === 2) {
            $bbb = `<input type='button' id="start1${data1.user_id}" class='start-btn mouse' value='啟用'>`;
          } else if (data1.user_start === 3) {
            $bbb = `尚未驗證信箱`;
          }
        }
        str1 +=
          `
    <tr align="center">
    <td>${data1.user_account}</td>
    <td>${data1.user_name}</td>
    <td>` +
          $aaa +
          `</td>
    <td>` +
          $bbb +
          `</td>
    </tr>
    `;
      });
      p_title1.innerHTML = str1;

      arr1.forEach(function (data1) {
        console.log("stop1" + `${data1.user_id}`);
        if (document.getElementById("stop1" + `${data1.user_id}`) != null) {
          var stop111 = document.getElementById("stop1" + `${data1.user_id}`);
          console.log(stop111);

          function stop11() {
            const formData = new FormData();
            // 添加文本字段
            formData.append("user_id", `${data1.user_id}`);
            formData.append("user_start", 2);

            axios
              .put("https://localhost:7094/api/User/B_PUT", formData, {
                headers: {
                  "Content-Type": "application/json",
                },
              })
              .then((response) => {
                console.log("Response:", response);
                // window.location.href = "back-product.html";
                location.reload();
                alert(
                  "停用成功!!\n\n" +
                    "停用帳號：" +
                    `${data1.user_account}` +
                    "\n停用姓名：" +
                    `${data1.user_name}` +
                    "\n停用權限：" +
                    $aaa
                );
              })
              .catch((error) => {
                console.error("Error:", error);
              });
          }
          stop111.addEventListener("click", stop11);
        }

        if (document.getElementById("start1" + `${data1.user_id}`) != null) {
          var start111 = document.getElementById("start1" + `${data1.user_id}`);
          console.log(start111);

          function start11() {
            const formData = new FormData();
            // 添加文本字段
            formData.append("user_id", `${data1.user_id}`);
            formData.append("user_start", 1);

            axios
              .put("https://localhost:7094/api/User/B_PUT", formData, {
                headers: {
                  "Content-Type": "application/json",
                },
              })
              .then((response) => {
                console.log("Response:", response);
                // window.location.href = "back-product.html";
                location.reload();
                alert(
                  "啟用成功!!\n\n" +
                    "啟用帳號：" +
                    `${data1.user_account}` +
                    "\n啟用姓名：" +
                    `${data1.user_name}` +
                    "\n啟用權限：" +
                    $aaa
                );
              })
              .catch((error) => {
                console.error("Error:", error);
              });
          }
          start111.addEventListener("click", start11);
        }
      });
    }
  }
  search_btn.addEventListener("click", search1);
  search.addEventListener("change", search1);
  search.addEventListener("blur", search1);
  search.addEventListener("keyup", search1);

  //登出
  var logout1 = document.getElementById("logout");

  function logout() {
    window.location.href = "../front/login.html";
    localStorage.clear();
  }
  logout1.addEventListener("click", logout);
}
