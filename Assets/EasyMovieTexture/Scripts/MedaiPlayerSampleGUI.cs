using UnityEngine;
using System.Collections;
using System.Net; 

public class MedaiPlayerSampleGUI : MonoBehaviour {


	public MediaPlayerCtrl scrMedia;
	public string url;
	public bool m_bFinish = false;
	private int toolbarInt;
	public static string str_show = "UDPserver已启动!";
	// Use this for initialization

	void Start () {
		scrMedia.OnEnd += OnEnd;
	}

	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))  
			Application.Quit();  	
	}
	
	#if !UNITY_WEBGL
	void OnGUI() {
		
		toolbarInt = GUI.Toolbar(new Rect(10,10,250,50),toolbarInt,new string[]{"内网","腾讯云","搬瓦工"});
		GUI.Label(new Rect(400, 10, 450, 50), scrMedia.GetCurrentState().ToString());
		GUI.Label(new Rect(500, 10, 550, 50), "本机IP：" + Network.player.ipAddress.ToString() + " 端口：8001");
		GUI.Label(new Rect(500, 60, 550, 100), str_show);
		switch (toolbarInt)
		{
			case 0:
				url = "rtsp://192.168.2.124:9554/webcam";
				break;
			case 1:
				url = "rtsp://rty813.xyz:28080/webcam";
				break;
			case 2:
				url = "rtsp://vps.rty813.xyz:28080/webcam";
				break;
		}
		scrMedia.m_strFileName = url;
		if( GUI.Button(new Rect(50,80,100,110),"Load"))
		{
			scrMedia.Load(url);
			m_bFinish = false;
		}
		
		if( GUI.Button(new Rect(50,200,100,100),"Play"))
		{
			scrMedia.Play();
			m_bFinish = false;
		}
	 	
		if( GUI.Button(new Rect(50,350,100,100),"stop"))
		{
			scrMedia.Stop();
		}
		
		if( GUI.Button(new Rect(50,500,100,100),"pause"))
		{
			scrMedia.Pause();
		}
		
		if( GUI.Button(new Rect(50,650,100,100),"Unload"))
		{
			scrMedia.UnLoad();
		}
		
		if( GUI.Button(new Rect(50,800,100,100), " " + m_bFinish))
		{
		
		}
		
		// if( GUI.Button(new Rect(200,50,100,100),"SeekTo"))
		// {
		// 	scrMedia.SeekTo(10000);
		// }


		// if( scrMedia.GetCurrentState() == MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING)
		// {
		// 	if( GUI.Button(new Rect(200,200,100,100),scrMedia.GetSeekPosition().ToString()))
		// 	{
		// 		scrMedia.SetSpeed(2.0f);
		// 	}
			
		// 	if( GUI.Button(new Rect(200,350,100,100),scrMedia.GetDuration().ToString()))
		// 	{
		// 		scrMedia.SetSpeed(1.0f);
		// 	}

		// 	if( GUI.Button(new Rect(200,450,100,100),scrMedia.GetVideoWidth().ToString()))
		// 	{
				
		// 	}

		// 	if( GUI.Button(new Rect(200,550,100,100),scrMedia.GetVideoHeight().ToString()))
		// 	{
				
		// 	}
		// }

		// if( GUI.Button(new Rect(200,650,100,100),scrMedia.GetCurrentSeekPercent().ToString()))
		// {
			
		// }
	

	}
	#endif


	
	void OnEnd()
	{
		m_bFinish = true;
	}
}
