# WebServicesForUnity
Unity的UnityWebRequest简单 get / post 包装。
### 使用方法：
1. 把 WebService.cs 和 SimpleJSON文件夹 放入Unity项目的Scripts文件夹下
2. Enjoy~
### 代码示例：
#### 返回String（Get形式发送）：
```
string someone;
WebService webService = new WebService();
webService.Get("http://127.0.0.1:853/index.php?user=Sonic853",(downloadHandler)=>{
    someone = downloadHandler.text;
    print(someone);
});
```
#### 返回JSON（Post形式，带上Json数据发送）：
```
JSONNode jsonData = JSON.Parse("{}");
jsonData["Sonic853"].Value = "Is Me!";
JSONNode responseJSON;
WebService webService = new WebService();
webService.Post("http://127.0.0.1:853/index.php",jsonData,(downloadHandler)=>{
    responseJSON = JSON.Parse(downloadHandler.text);
});
```
#### 返回Texture（Post形式，带上表单数据发送）：
```
Dictionary<string,string> postDictionary = new Dictionary<string, string>();
postDictionary.Add("Lindinia","My Love.");
Texture Lindinia;
WebService webService = new WebService();
webService.Post("https://853lab.com/20190203011324.png",postDictionary,(downloadHandler)=>{
    Lindinia = ((DownloadHandlerTexture)downloadHandler).texture;
});
```
#### 不知道会不会出问题的用法：
```
string someone;
WebService.Instance.Get("http://127.0.0.1:853/index.php",(downloadHandler)=>{
    someone = downloadHandler.text;
    print(someone);
});
```
PS: 能力有限，只会弄成这样。。。或者帮忙完善orz
### 参考至以下的代码：
* [https://github.com/mw-felker/WebServicesForUnity3D](https://github.com/mw-felker/WebServicesForUnity3D)
* [https://github.com/Bunny83/SimpleJSON](https://github.com/Bunny83/SimpleJSON)