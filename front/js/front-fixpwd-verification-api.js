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

    function sendEmail() {
        const email = document.querySelector(".user-textbox").value; // 取得會員信箱
        // 檢查信箱格式是否正確
        function validateEmail(email) {
            const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            return regex.test(email);
        }

        if (email === "") {
            alert("會員信箱為必填");
        } else if (!validateEmail(email)) {
            alert("信箱格式錯誤請重新輸入!!");
        } else {
            // 使用 axios 發送 GET 請求
            axios
                .get("https://localhost:7094/api/ForgetPwd/send_email", {
                    params: {
                        user_email: email,
                    },
                })
                .then((response) => {
                    console.log(response.data);
                    alert(response.data);
                })
                .catch((error) => {
                    console.error(error);
                    // TODO: 處理錯誤
                });
        }
    }

    function next() {
        const email = document.querySelector(".user-textbox").value; // 取得會員信箱
        const vcode = document.querySelector(".verification-textbox").value;
        // 信箱格式
        function validateEmail(email) {
            const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            return regex.test(email);
        }

        if (email === "") {
            alert("會員信箱為必填!!");
        } else if (!validateEmail(email)) {
            alert("信箱格式錯誤請重新輸入!!");
        } else if (vcode === "") {
            alert("驗證碼不得為空!!");
        } else {
            // 使用 axios 發送 GET 請求
            axios
                .get("https://localhost:7094/api/ForgetPwd/verify_cord", {
                    params: {
                        user_email: email,
                        code: vcode,
                    },
                })
                .then((response) => {
                    console.log(response.data);
                    if (response.data == "0") {
                        alert("驗證失敗，請確認驗證碼🥹");
                    } else {
                        alert("驗證成功😊!!");
                        window.location.href =
                            "./front-reset-pwd.html?email=" + encodeURIComponent(`${email}`);
                    }
                })
                .catch((error) => {
                    console.error(error);
                    // TODO: 處理錯誤
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