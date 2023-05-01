function gogo() {
    const urlParams = new URLSearchParams(window.location.search);
    const userid = urlParams.get('Account');
    const check = document.querySelector(".verification-textbox").value;
    if (check == "") {
        alert("⚠️驗證碼不可為空");
    } else {

        axios.post('https://localhost:7094/api/User/email/validate?Account=' + userid + "&AuthCode=" + check)
            .then(response => {
                console.log(response);
                alert("註冊成功，將帶您前往登入頁🥳");
                window.location.href = "./login.html";
            })
            .catch(error => {
                console.error(error);
            });
    }
}