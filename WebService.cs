/*
	Web Service

	参考至以下的代码：
		https://github.com/mw-felker/WebServicesForUnity3D
		https://github.com/Bunny83/SimpleJSON

	将UnityWebRequest封装成简单的post/get请求，带CallBack回调。
	回调的东西可以转为string，json或Texture。
	用法：
		String（Get形式：
			string someone;
			WebService.Instance.Get("http://127.0.0.1:853/index.php?user=Sonic853",(downloadHandler)=>{
				someone = downloadHandler.text;
				print(someone);
			});
		JSON（Put形式，带上Json数据：
			JSONNode jsonData = JSON.Parse("{}");
			jsonData["Sonic853"].Value = "Is Me!";
			JSONNode responseJSON;
			WebService.Instance.Put("http://127.0.0.1:853/index.php",jsonData,(downloadHandler)=>{
				responseJSON = JSON.Parse(downloadHandler.text);
			});
		Texture（Post形式，带上表单←（应该叫做字典？）：
			Dictionary<string,string> postDictionary = new Dictionary<string, string>();
			postDictionary.Add("Lindinia","My Love.");
			Texture Lindinia;
			WebService.Instance.Post("https://853lab.com/20190203011324.png",postDictionary,(downloadHandler)=>{
				Lindinia = ((DownloadHandlerTexture)downloadHandler).texture;
			});

	能力有限，只会弄成这样。。。或者帮忙完善orz
	（感觉自己封装能力像坨屎一样，但能用

 */
using System; 
using UnityEngine;
using System.Collections; 
using System.Collections.Generic;
using SimpleJSON;
using System.Linq;
using UnityEngine.Networking;

public class WebService : MonoBehaviour
{
	// general web service
	public string URL { get; set; }
	public Dictionary<string, string> postDictionary { get; set; }
	public JSONNode jsonData { get; set; }
	public DownloadHandler downloadHandler { get; set; }
	
	// Set up states
    enum State {Idle,Getting, Posting,Responded};
	State state;
	
	// Set up our call back delegation
    public delegate void CallBack(DownloadHandler downloadHandler);
	CallBack callBack;
	
	[HideInInspector]
	public string error;

	static WebService instance;
    public static WebService Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject mounter = new GameObject("WebService");
                instance = mounter.AddComponent<WebService>();
            }
            return instance;
        }
    }
    void Awake(){
        instance = this;
    }

	public void Start() {

		state = State.Idle;

	}
	public void Set(string URL){
		this.URL = URL;
	}
	public void Set(string URL, JSONNode jsonData){
		this.URL = URL;
		this.jsonData = jsonData;
	}
	public void Set(string URL, Dictionary<string, string> postDictionary){
		this.URL = URL;
		this.postDictionary = postDictionary;
	}

	// GET from the server
    public void Get(){

	    UnityWebRequest response = UnityWebRequest.Get(URL);

		state = State.Getting;

	    StartCoroutine( 
			MakeRequest(response,null)
		);

    }
    public void Get(string URL){

	    UnityWebRequest response = UnityWebRequest.Get(URL);

		state = State.Getting;

	    StartCoroutine( 
			MakeRequest(response,null)
		);

    }
    public void Get(string URL, CallBack callBack){

	    UnityWebRequest response = UnityWebRequest.Get(URL);

		state = State.Getting;

	    StartCoroutine( 
			MakeRequest(response,callBack)
		);

    }
    public void Get(CallBack callBack){

	    UnityWebRequest response = UnityWebRequest.Get(URL);

		state = State.Getting;

	    StartCoroutine( 
			MakeRequest(response,callBack)
		);

    }

	// POST to the server use Dictionary
    public void Post(string URL, Dictionary<string,string> postDictionary, CallBack callBack){

		UnityWebRequest response = UnityWebRequest.Post(URL, postDictionary);

		state = State.Posting;

	    StartCoroutine( 
			MakeRequest(response,callBack)
		);

    }

	// POST to the server use WWWForm
    public void Post(string URL, WWWForm form, CallBack callBack){

		UnityWebRequest response = UnityWebRequest.Post(URL, form);

		state = State.Posting;

	    StartCoroutine( 
			MakeRequest(response,callBack)
		);

    }

	// Just POST to the server
    public void Post(string URL, CallBack callBack){

        WWWForm form = new WWWForm();

		UnityWebRequest response = UnityWebRequest.Post(URL, form);

		state = State.Posting;

	    StartCoroutine( 
			MakeRequest(response,callBack)
		);

    }

    public void Post(string URL){

        WWWForm form = new WWWForm();

		UnityWebRequest response = UnityWebRequest.Post(URL, form);

		state = State.Posting;

	    StartCoroutine( 
			MakeRequest(response,null)
		);

    }

    public void Post(CallBack callBack){

		UnityWebRequest response = UnityWebRequest.Post(URL, postDictionary);

		state = State.Posting;

	    StartCoroutine( 
			MakeRequest(response,callBack)
		);

    }

	// PUT to the server use json
    public void Put(string URL, JSONNode jsonData, CallBack callBack){

        string jsonString = jsonData.ToString() ?? "";

		UnityWebRequest response = UnityWebRequest.Put(URL, jsonString);

		state = State.Posting;

	    StartCoroutine( 
			MakeRequest(response,callBack)
		);

    }

    public void Put(string URL, string jsonData, CallBack callBack){

        string jsonString = jsonData;

		UnityWebRequest response = UnityWebRequest.Put(URL, jsonString);

		state = State.Posting;

	    StartCoroutine( 
			MakeRequest(response,callBack)
		);

    }

	// PUT to the server use byte
    public void Put(string URL, byte[] bodyData, CallBack callBack){

		UnityWebRequest response = UnityWebRequest.Put(URL, bodyData);

		state = State.Posting;

	    StartCoroutine( 
			MakeRequest(response,callBack)
		);

    }

    public void Put(CallBack callBack){

        string jsonString = jsonData.ToString() ?? "";

		UnityWebRequest response = UnityWebRequest.Put(URL, jsonString);

		state = State.Posting;

	    StartCoroutine( 
			MakeRequest(response,callBack)
		);

    }

	// Make the Request & Exectute Call Back function
    IEnumerator MakeRequest( UnityWebRequest response, CallBack callBack){

        yield return response.SendWebRequest();

		state = State.Responded;

		Debug.Log("Web Service Response: "+response.downloadHandler.text);

		downloadHandler = response.downloadHandler;

		if(callBack != null) {

			callBack( downloadHandler );

		}

		state = State.Idle;
    }
}
