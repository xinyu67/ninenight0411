window.onload = function(){

// const request = new XMLHttpRequest()

      fetch('https://localhost:7094/api/Product')
      .then(response => {
        // 檢查回傳的狀態碼是否為 200 OK
      if (response.status !== 200) {
      console.log(`Error: ${response.status}`)
      return
    }
    // 將回傳的資料轉換成 JSON 格式
    return response.json()
  })
  .then(data => {
    // 使用回傳的資料做一些事情
    console.log(data)
  })
  .catch(error => {
    console.error(error)
  })
		
    };