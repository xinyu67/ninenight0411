function send() {
    const mail = document.querySelector(".mail").value;
    const userid = document.querySelector(".userid").value;
    const userpwd = document.querySelector(".userpwd").value;
    const usercpwd = document.querySelector(".usercpwd").value;
    const username = document.querySelector(".username").value;
    const sex = document.querySelector('input[name="sex"]:checked').value;
    if (sex == "true") {
        var sexx = Boolean("true");
    } else if (sex == "false") {
        var sexx = Boolean("");
    }
    const birthday = document.querySelector(".bd").value;
    const userphone = document.querySelector(".userphone").value;
    const useradd = document.querySelector(".useradd").value;
    //email格式
    function validateEmail(mail) {
        const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return regex.test(mail);
    }


    function getAge(birthday) {
        //年齡驗證
        const today = new Date(); //取目前日期
        const birthdate = new Date(birthday);
        //獲得目前年份及輸入年份，計算年齡差
        let age = today.getFullYear() - birthdate.getFullYear();
        //如果目前月份小於輸入月份，或目前月份等於出生月份但目前日期小於出生日期，年龄就- 1
        const month = today.getMonth() - birthdate.getMonth();
        if (month < 0 || (month === 0 && today.getDate() < birthdate.getDate())) {
            age = age - 1;
        }
        return age;
    }

    if (mail == "" || userid == "" || userpwd == "" || usercpwd == "" || username == "" || birthday == "" || userphone == "" || useradd == "") {
        alert("⛔欄位不可為空!!")
    } else if (!validateEmail(mail)) {
        alert('信箱格式錯誤請重新輸入!!');
    } else if (userpwd !== usercpwd) {
        alert("⚠️請輸入相同密碼")
    } else if (getAge(birthday) < 18) {
        alert("🔞未滿18歲不可註冊");
    } else {


        const data = {
            user_account: userid,
            user_pwd: userpwd,
            user_name: username,
            user_gender: sexx,
            user_birthday: birthday,
            user_email: mail,
            user_phone: userphone,
            user_address: useradd
        };
        axios.post('https://localhost:7094/api/User/Register', data)
            .then(response => {
                console.log(response);
                alert("成功!! 前往驗證>>>");
                window.location.href = "./signup-verification.html?Account=" + userid;
            })
            .catch(error => {
                console.error(error);
                alert(error.response.data);
            });
    }
}