function sendEmail() {

    const email = document.querySelector(".user-textbox").value; // 取得會員信箱
    // 檢查信箱格式是否正確
    function validateEmail(email) {
        const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return regex.test(email);
    }

    if (email === "") {
        alert('會員信箱為必填');
    } else if (!validateEmail(email)) {
        alert('信箱格式錯誤請重新輸入!!');
    } else {
        // 使用 axios 發送 GET 請求
        axios.get('https://localhost:7094/api/ForgetPwd/send_email', {
                params: {
                    user_email: email
                }
            })
            .then(response => {
                console.log(response.data);
                alert(response.data);
            })
            .catch(error => {
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

    if (email === "" || vcode === "") {
        alert('會員信箱為必填');
    } else if (!validateEmail(email)) {
        alert('信箱格式錯誤請重新輸入!!');
    } else {
        // 使用 axios 發送 GET 請求
        axios.get('https://localhost:7094/api/ForgetPwd/verify_cord', {
                params: {
                    user_email: email,
                    code: vcode
                }
            })
            .then(response => {
                console.log(response.data);
                if (response.data == '0') {
                    alert('驗證失敗，請確認驗證碼🥹');
                } else {
                    alert('驗證成功😊!!')
                    window.location.href = './forget-reset.html?email=' + encodeURIComponent(`${email}`);
                }
            })
            .catch(error => {
                console.error(error);
                // TODO: 處理錯誤
            });
    }
}