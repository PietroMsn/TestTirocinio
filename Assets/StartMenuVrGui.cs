using UnityEngine;
using System.Collections;

public class StartMenuVrGui : VRGUI {

	private GUIStyle gs = null;
	// Use this for initialization
	public override void OnVRGUI () {

		gs = new GUIStyle(GUI.skin.button);
		gs.fontSize = 30;




		if (GUI.Button (new Rect (Screen.width / 2 - 50, 10 + 150, 140, 70), "Scale", gs)) {
						Application.LoadLevel("StartScale");
						PlayerPrefs.SetString("GestureChoice", "scale");
		}

		if (GUI.Button (new Rect (Screen.width / 2 - 50, 110 + 150, 140, 70), "Rotation",gs)) {
						Application.LoadLevel("StartRotation");
						PlayerPrefs.SetString("GestureChoice", "rotation");
		}

		if (GUI.Button (new Rect (Screen.width / 2 - 50, 210 + 150, 140, 70), "Translation", gs)) {
						Application.LoadLevel("StartTranslate");
						PlayerPrefs.SetString("GestureChoice", "translation");
		}

	}
}
