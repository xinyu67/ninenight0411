//  用getItem抓login的資料存入login裡
var login = localStorage.getItem('login');
console.log(login);
//  如果login是null就跳轉到登入畫面(會是null是因為登出時清空了所有在localStorage裡的資料)
if (login === null) {
    window.location.href = "../front/login.html";
} else {




    var editor = CKEDITOR.replace('editorDemo');

    // 抓現在網址
    var bb = window.location.href;
    // console.log(bb);
    // 從'='切割
    var ary1 = bb.split('=');
    // console.log(ary1[1]);

    //JSON 檔案網址
    const url = "https://localhost:7094/api/Product/product_id?product_id=" + ary1[1];
    // console.log(url);
    let data = [];
    // step 1 - 取得資料
    (function getData() {
        axios.get(url)
            .then(function(response) {
                // 檢查
                console.log(response.data);
                // 將取得資料帶入空陣列data中
                data = response.data;
                title(data);
            })
    })();

    function title(arr) {
        var chinese = document.getElementById('chinese')
        var place = document.getElementById('place')
        var brand = document.getElementById('brand')
        var english = document.getElementById('english')
        var ml = document.getElementById('ml')
        var price = document.getElementById('price')
        var htmll = document.getElementById('editorDemo')
        var reset = document.getElementById('reset')
        var product_num = document.getElementById('product_num')

        document.getElementById("blah").src = data[0].product_img;
        document.getElementById('imgsrc1').value = data[0].product_img;
        chinese.value = data[0].product_name;
        place.value = data[0].place_name;
        brand.value = data[0].brand_name;
        english.value = data[0].product_eng;
        ml.value = data[0].product_ml;
        price.value = data[0].product_price;
        htmll.value = data[0].product_content;
        product_num.value = data[0].product_num;
        console.log(data[0].product_img);
        console.log(ary1[1])

        reset.addEventListener('click', function() {
            document.getElementById("blah").src = data[0].product_img;
            chinese.value = data[0].product_name;
            place.value = data[0].place_name;
            brand.value = data[0].brand_name;
            english.value = data[0].product_eng;
            ml.value = data[0].product_ml;
            price.value = data[0].product_price;
            htmll.value = data[0].product_content;
        });
    }


    window.onload = function() {
        //上傳圖片預覽
        let input = document.querySelector('#img-ing')
        input.addEventListener('change', (e) => {
            function getObjectURL(file) {
                var url = null;
                if (window.createObjcectURL != undefined) {
                    url = window.createObjectURL(file);
                } else if (window.URL != undefined) {
                    url = window.URL.createObjectURL(file);
                } else if (window.webkituRL != undefined) {
                    url = window.webkituRL.createObjectURL(file);
                }
                return url;
            }
            var abc = getObjectURL(e.target.files[0]);
            console.log(e.target.files[0].name);
            console.log(abc);
            document.getElementById("blah").src = abc;
            document.getElementById('imgsrc').value = e.target.files[0].name;
            console.log(e.target.files[0].name);
        });
    }

    var aas = document.getElementById('button')

    function aa(e) {

        // 本來圖片的檔名
        var aaa = document.getElementById('imgsrc1').value;
        // 上傳圖片時才會有的檔名
        var ggg = document.getElementById('imgsrc').value;

        const myFile = document.getElementsByName('myfile')[0].files[0];
        var bbb = chinese.value;
        var ccc = english.value;
        var ddd = ml.value;
        var eee = price.value;
        var htmll = CKEDITOR.instances.editorDemo.getData();
        var hhh = product_num.value;
        if (ggg === "") {
            iii = aaa;
        } else if (ggg != "") {
            iii = ggg;
        }
        console.log(iii);
        console.log(ggg);



        if ((htmll === "") || (bbb === "") || (ccc === "") || (ddd === "") || (eee === "")) {
            alert("欄位不得為空，請輸入");
            return false;
        } else if ((htmll != ""), (bbb != ""), (ccc != ""), (ddd != ""), (eee != "")) {
            const formData = new FormData();
            // 添加文本字段
            formData.append('product_id', ary1[1]);
            formData.append('product_num', hhh);
            formData.append('product_name', bbb);
            formData.append('product_eng', ccc);
            if (ggg === "") {
                iii = aaa;
                // document.getElementById("blah").src = "";
                formData.append('product_img', "");
            } else if (ggg != "") {
                iii = ggg;
                formData.append('product_img', myFile);
            }
            formData.append('product_price', eee);
            formData.append('product_ml', ddd);
            formData.append('product_content', htmll);

            axios.put('https://localhost:7094/api/Product', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            }).then(response => {
                console.log('Response:', response);
                window.location.href = "back-product.html";
                // location.reload();
                alert("編輯成功");
            }).catch(error => {
                console.error('Error:', error);
            });
        }


    }
    aas.addEventListener('click', aa);






}