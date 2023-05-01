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

    function send() {
        const params = new URLSearchParams(window.location.search);
        const email = params.get("email"); // 獲取鍵為"name"的值
        const emaill = email.split("=")[0]; // 拆分鍵值對，取等號前的字串

        const pwd = document.querySelector(".user-textbox").value;
        const cpwd = document.querySelector(".check").value;
        if (pwd == "" || cpwd == "") {
            alert("欄位不可為空⛔");
        } else if (pwd !== cpwd) {
            alert("請輸入相同密碼⚠️");
        } else {
            axios
                .put(
                    "https://localhost:7094/api/ForgetPwd/upd_pwd?email=" +
                    emaill +
                    "&pwd=" +
                    pwd +
                    "&pwd_two=" +
                    cpwd
                )
                .then((response) => {
                    // Handle successful response
                    console.log(response.data);
                    alert("重設成功，請重新登入!!");
                    window.location.href = "./login.html";
                })
                .catch((error) => {
                    // Handle error
                    console.error(error);
                    // console.error(error.response.data);
                    // console.error(error.response.status);
                    // console.error(error.response.headers);
                });
        }
    }
    //登出
    var logout1 = document.getElementById("logout");

    function logout() {
        if (confirm('確認要登出嗎？') == true) {
            window.location.href = "../front/login.html";
            localStorage.clear();
        } else {

        }
    }
    logout1.addEventListener("click", logout);
}