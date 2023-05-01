function send() {
    const params = new URLSearchParams(window.location.search);
    const email = params.get('email'); // 獲取鍵為"name"的值
    const emaill = email.split('=')[0]; // 拆分鍵值對，取等號前的字串


    const pwd = document.querySelector('.user-textbox').value;
    const cpwd = document.querySelector('.check').value;
    if (pwd == "" || cpwd == "") {
        alert('欄位不可為空⛔')
    } else if (pwd !== cpwd) {
        alert('請輸入相同密碼⚠️')
    } else {
        axios.put('https://localhost:7094/api/ForgetPwd/upd_pwd?email=' + emaill + '&pwd=' + pwd + '&pwd_two=' + cpwd)
            .then(response => {
                // Handle successful response
                console.log(response.data);
                alert('重設成功，請重新登入!!');
                window.location.href = './login.html';
            })
            .catch(error => {
                // Handle error
                console.error(error);
                // console.error(error.response.data);
                // console.error(error.response.status);
                // console.error(error.response.headers);
            });
    }
}