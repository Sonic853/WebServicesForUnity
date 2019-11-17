# WebServicesForUnity
Unity的UnityWebRequest简单 get / post 包装。
### 使用方法：
1. 把 WebService.cs 和 SimpleJSON文件夹 放入Unity项目的Scripts文件夹下
2. Enjoy~
### 代码示例：
#### 返回String（Get形式发送）：
```
//↓ 创建一个string
string someone;

//↓ 开始发送Get
WebService.Instance.Get("http://127.0.0.1:853/index.php?user=Sonic853",(downloadHandler)=>{
    //↓ 得到的东西放到someone里
    someone = downloadHandler.text;
    print(someone);
});
```
#### 返回JSON（Put形式，带上Json数据发送）：
```
//↓ 创建一个要发送的Json
JSONNode jsonData = JSON.Parse("{}");
jsonData["Sonic853"].Value = "Is Me!";

//↓ 创建一个要获得返回的Json
JSONNode responseJSON;

//↓ 开始发送Put
WebService.Instance.Put("http://127.0.0.1:853/index.php",jsonData,(downloadHandler)=>{
    //↓ 获得返回的Json
    responseJSON = JSON.Parse(downloadHandler.text);
});
```
#### 返回Texture（Post形式，带上表单数据发送）：
```
//↓ 创建一个要发送的表单数据
Dictionary<string,string> postDictionary = new Dictionary<string, string>();
postDictionary.Add("Lindinia","My Love.");

//创建一个Texture
Texture Lindinia;

//↓ 开始发送Post
WebService.Instance.Post("https://853lab.com/20190203011324.png",postDictionary,(downloadHandler)=>{
    //↓ 获得返回的图片写入Texture里
    Lindinia = ((DownloadHandlerTexture)downloadHandler).texture;
});
```
PS: 能力有限，只会弄成这样。。。或者帮忙完善orz
### 参考至以下的代码：
* [https://github.com/mw-felker/WebServicesForUnity3D](https://github.com/mw-felker/WebServicesForUnity3D)
* [https://github.com/Bunny83/SimpleJSON](https://github.com/Bunny83/SimpleJSON)