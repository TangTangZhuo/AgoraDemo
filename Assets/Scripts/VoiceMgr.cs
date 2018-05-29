using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;
using UnityEngine.UI;

public class VoiceMgr : MonoBehaviour {
	private static IRtcEngine mRtcEngine = null;
	private static string appId = "71b62919392544d88472e0b4763f0fe0";
	public GetGameobject Gameobjects;

	void Start () {
		//初始化
		mRtcEngine = IRtcEngine.GetEngine (appId);
		mRtcEngine.SetChannelProfile (CHANNEL_PROFILE.GAME_FREE_MODE);
		mRtcEngine.EnableAudioVolumeIndication (200, 3);
	}

	void Update () {
		if (mRtcEngine != null) {
			mRtcEngine.Poll ();
		}
	}

	//进入UID选择界面
	public void JoinStormChannel(){
		Gameobjects.Rooms.SetActive (false);
		Gameobjects.Members.SetActive (true);
	}

	//选择自己的UID之后进入聊天室
	public void OnNumberClick(Button button){
		Gameobjects.Members.SetActive (false);
		Gameobjects.RoomContent.SetActive (true);

		uint uid = uint.Parse (button.name.ToString ());
		mRtcEngine.MuteLocalAudioStream (true);
		mRtcEngine.JoinChannel ("Storm", null, uid);
	}

	//离开聊天室
	public void LevelChannel(){
		mRtcEngine.LeaveChannel ();
		Gameobjects.Rooms.SetActive (true);
		Gameobjects.RoomContent.SetActive (false);
	}

	//打开音频
	public void OpenAudio(){
		mRtcEngine.MuteLocalAudioStream (false);
	}

	//关闭音频
	public void CloseAudio(){
		mRtcEngine.MuteLocalAudioStream (true);
	}

	//屏蔽某人
	public void HideSomeone(Text text){
		uint uid = uint.Parse (text.text);
		mRtcEngine.MuteRemoteAudioStream (uid, true);
	}

	//取消屏蔽某人
	public void ListenSomeone(Text text){
		uint uid = uint.Parse (text.text);
		mRtcEngine.MuteRemoteAudioStream (uid, false);
	}

	//禁音某人
	public void AnotherHideSomeone(Text text){
		uint uid = uint.Parse (text.text);
		mRtcEngine.SetParameters ("{\"che.audio.playout.uid.volume\": {\"uid\":"+uid+",\"volume\":0}}");
	}

	//取消禁音某人
	public void AnotherListenSomeone(Text text){
		uint uid = uint.Parse (text.text);
		mRtcEngine.SetParameters ("{\"che.audio.playout.uid.volume\": {\"uid\":"+uid+",\"volume\":100}}");
	}
}
