using System.Collections;
using System.Collections.Generic;
using Quobject.SocketIoClientDotNet.Client;
using UnityEngine;

public class SocketController : MonoBehaviour {
	public GameObject obj;
	protected string sockPos;
	// Use this for initialization
	void Start () {
		var socket = IO.Socket ("http://192.168.2.3:3000");
		socket.On (Socket.EVENT_CONNECT, () => {
			socket.Emit("pos");
		});
		socket.On("pos", (data) =>
			{
				Debug.Log(data);
				//socket.Disconnect();

				sockPos = data.ToString();
			});

//		socket.On("pos", (data) =>
//			{
//				int posInfo;// = Int32.parse(data.ToString());
//				Vector3 newPos = new Vector3(posInfo, posInfo, posInfo);
//				obj.transform = Vector3.Lerp(obj.transform.position, newPos);
//			});
	}
	// Update is called once per frame
	void Update () {
		if (sockPos != null) {
			char[] delimiter = { ',' };
			string[] vectorString = sockPos.Split(delimiter, 3);
			Vector3 newPos = new Vector3 (float.Parse (vectorString[0]), float.Parse (vectorString[1]), float.Parse (vectorString[2]));
			obj.transform.position = Vector3.Lerp (obj.transform.position, obj.transform.position + newPos, 0.5f);
			sockPos = null;
		}
	}
}
