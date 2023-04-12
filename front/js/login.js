window.onload = function(){
    var hello = document.getElementById("login1");
    var hello2 = document.getElementById("login2");
    var user = document.getElementById("user55");
    var user1 = document.getElementById("user");
    var login = document.getElementById("login");

    login.addEventListener("click",function(){
        if(user.value === "123"){
            hello2.style.display = "flex";
            hello.style.display = "none";
        }else{
            hello.style.display = "flex";
            hello2.style.display = "none";
        }
    });

    
    }