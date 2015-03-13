using UnityEngine;
using System.Collections;

public class StartMenuGui : VRGUI {

	// Use this for initialization
	public override void OnVRGUI () {

		string UserName;

		UserName = GUI.TextField(new Rect(Screen.width / 2 - 200, 10, 400, 150), "User: ", 25);

		PlayerPrefs.SetString("User", UserName);

		if (GUI.Button (new Rect (Screen.width / 2 - 50, 10 + 150, 100, 50), "Scale")) {
						Application.LoadLevel("StartScale");
						PlayerPrefs.SetString("GestureChoice", "scale");
		}

		if (GUI.Button (new Rect (Screen.width / 2 - 50, 110 + 150, 100, 50), "Rotation")) {
						Application.LoadLevel("StartRotation");
						PlayerPrefs.SetString("GestureChoice", "rotation");
		}

		if (GUI.Button (new Rect (Screen.width / 2 - 50, 210 + 150, 100, 50), "Translation")) {
						Application.LoadLevel("StartTranslate");
						PlayerPrefs.SetString("GestureChoice", "translation");
		}

	}
}
