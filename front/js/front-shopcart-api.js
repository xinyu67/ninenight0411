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
  //購物車總覽--檔案網址
  const url = "https://localhost:7094/api/Cart?user_id=" + user_id;

  let data = [];
  /** 步驟一 取得資料**/
  (function getData() {
    axios.get(url).then(function (response) {
      // 檢查：console.log(response.data);

      // 將取得資料帶入空陣列 data 中
      data = response.data;
      cart(data);
    });
  })();

  //購物車總覽
  function cart(arr) {
    //抓取欄位
    const cart_all = document.querySelector(".product_all");
    let str = "";
    //將資料存入
    // arr[0].product_list.forEach(function(data) {
    //     console.log(arr)
    //     str += `
    //     <tr>
    //     <td>
    //         <div class="table-img"><img src="${data.product_img}"></div>
    //     </td>
    //     <td>${data.product_name}&nbsp;${data.product_ml}ml</td>
    //     <td id="price">${data.product_price}</td>
    //     <td><input id="count_${data.cart_product_id}" type="text" value="${data.cart_product_amount}" size="1" style="text-align:center;"></td>
    //     <td  id="money">$${data.money}</td>
    //     <td><li><a href="#" class="dd_${data.cart_product_id}"><i class="fa-solid fa-circle-minus"></i></a></li></td>
    //     </tr>
    // `
    // })

    if (data.length === 0) {
      const total = document.querySelector(".total-content");
      total.style.display = "none";
      const next = document.querySelector(".next");
      next.style.display = "none";
      str +=
        '<tr><td colspan="6">目前無商品，請前往<a href="./front-product.html" style="color:#AD5A5A;text-decoration: underline;">產品區</a>下單🛍️</td></tr>';
      cart_all.innerHTML = str;
    } else {
      arr[0].product_list.forEach(function (data) {
        str += `
            <tr>
            <td>
                <div class="table-img"><img src="${data.product_img}"></div>
            </td>
            <td>${data.product_name}&nbsp;${data.product_ml}ml</td>
            <td id="price">${data.product_price}</td>
            <td><input id="count_${data.cart_product_id}" type="number" min="1" value="${data.cart_product_amount}" size="1" style="text-align:center;width:15%;"></td>
            <td  id="money">$${data.money}</td>
            <td><li><a href="#" class="dd_${data.cart_product_id}"><i class="fa-solid fa-circle-minus"></i></a></li></td>
            </tr>
        `;
      });

      arr.forEach(function (data) {
        cart_all.innerHTML = str;
        const total = document.querySelector(".total-money");
        total.innerHTML = `$&nbsp;${data.total}`;
      });

      //更新數量
      arr[0].product_list.forEach(function (data) {
        const countInput = document.getElementById(
          `count_${data.cart_product_id}`
        );
        countInput.addEventListener("change", function () {
          const cartid = data.cart_product_id;
          const num = countInput.value;
          if (num <= "0") {
            alert("商品數量不得為0或小於0，如需取消請按刪除紐🥲");
            location.reload();
          } else {
            const url = `https://localhost:7094/api/Cart`;
            const newdata = new FormData();
            newdata.append("cart_product_id", cartid);
            newdata.append("cart_product_amount", num);

            axios
              .put(url, newdata)
              .then((response) => {
                console.log(response);
                location.reload();
              })
              .catch((error) => {
                console.log(error);
              });
          }
        });
      });

      // 刪除商品
      arr[0].product_list.forEach(function (data) {
        const deleteButton = document.querySelector(
          `.dd_${data.cart_product_id}`
        );
        deleteButton.addEventListener("click", function () {
          const cartid = data.cart_product_id;
          const url = `https://localhost:7094/api/Cart/?product_id=${cartid}`;

          axios
            .delete(url)
            .then((response) => {
              console.log(response);
              location.reload();
              alert(response.data);
            })
            .catch((error) => {
              console.log(error);
            });
        });
      });
    }
  }
  //登出
  var logout1 = document.getElementById("logout");

  function logout() {
    window.location.href = "../front/login.html";
    localStorage.clear();
  }
  logout1.addEventListener("click", logout);
}
