//  用getItem抓login的資料存入login裡
var login = localStorage.getItem('login');
console.log(login);
//  如果login是null就跳轉到登入畫面(會是null是因為登出時清空了所有在localStorage裡的資料)
if (login === null) {
    window.location.href = "../front/login.html";
} else {




    var editor = CKEDITOR.replace('editorDemo', {});


    //JSON 檔案網址
    const url = "https://localhost:7094/api/Place";
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
        // 抓取欄位
        const p_title = document.querySelector('#place-select')
        let str = "";
        // 將資料存入
        arr.forEach(function(data) {
            eee = `<option value="">--請選擇產地--</option>`
            str += `
        <option id="${data.place_id}" value="${data.place_id}">${data.place_name}</option>
    `
        })
        p_title.innerHTML = eee + str;
    }
    var pplace = document.getElementById("place-select");
    pplace.addEventListener("change", function() {
        console.log(pplace.value);
    });


    //JSON 檔案網址
    const url1 = "https://localhost:7094/api/brand";
    let data1 = [];
    // step 1 - 取得資料
    (function getData() {
        axios.get(url1)
            .then(function(response) {
                // 檢查
                console.log(response.data);
                // 將取得資料帶入空陣列data中
                data1 = response.data;
                title1(data1);
            })
    })();

    function title1(arr) {
        // 抓取欄位
        const p_title1 = document.querySelector('#brand-select')
        let str1 = "";
        // 將資料存入
        arr.forEach(function(data1) {
            bbb = `<option value="">--請選擇品牌--</option>`
            str1 += `
        <option id="${data1.brand_id}" value="${data1.brand_id}">${data1.brand_name}</option>
    `
        })
        p_title1.innerHTML = bbb + str1;
    }
    var bbrand = document.getElementById("brand-select");
    bbrand.addEventListener("change", function() {
        console.log(bbrand.value);
    });


    window.onload = function() {
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
            console.log(abc);
            document.getElementById("blah").src = abc;
            var abc = getObjectURL(e.target.files[0]);
            const myFile = document.getElementsByName('myfile')[0].files[0];
            document.getElementById("imged").value = myFile;
            console.log(myFile);
        });
    }


    var aass = document.getElementById('button')

    function bb() {
        var chinese = document.getElementById('product_name')
        var english = document.getElementById('product_eng')
        var ml = document.getElementById('product_ml')
        var price = document.getElementById('product_price')
        var htmll = CKEDITOR.instances.editorDemo.getData();
        console.log("htmll", htmll);
        const myFile = document.getElementsByName('myfile')[0].files[0];
        var bbb = chinese.value;
        var ccc = english.value;
        var ddd = ml.value;
        var eee = price.value;


        if ((htmll === "") || (bbb === "") || (ccc === "") || (ddd === "") || (eee === "") || (myFile === undefined)) {
            alert("欄位不得為空，請輸入");
            return false;
        } else if ((htmll != ""), (bbb != ""), (ccc != ""), (ddd != ""), (eee != ""), (myFile != undefined)) {
            const formData = new FormData();
            formData.append("product_img", myFile);
            formData.append('product_name', chinese.value);
            formData.append('product_eng', english.value);
            formData.append('product_price', price.value);
            formData.append('product_ml', ml.value);
            formData.append('product_content', htmll);
            formData.append('brand_id', bbrand.value);
            formData.append('place_id', pplace.value);


            axios.post('https://localhost:7094/api/Product', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            }).then(response => {
                console.log('Response:', response);
                window.location.href = "back-product.html";
                // location.reload();
                alert("新增成功");
            }).catch(error => {
                console.error('Error:', error);
            });
        }


    }
    aass.addEventListener('click', bb);










    var aas = document.getElementById('reset')

    function aa(e) {
        document.getElementById("blah").src = " ";
    }
    aas.addEventListener('click', aa);


}