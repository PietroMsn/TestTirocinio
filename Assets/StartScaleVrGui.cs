using UnityEngine;
using System.Collections;

public class StartScaleVrGui : VRGUI {

	private GUIStyle gs = null;


	public override void OnVRGUI () {

			gs = new GUIStyle(GUI.skin.button);
			gs.fontSize = 30;

			string stringToEdit = "";
		


				stringToEdit = "Pinch Scale";


	        	stringToEdit = GUI.TextArea(new Rect(Screen.width / 2 - 200, 10, 400, 150), stringToEdit, gs);

				if (GUI.Button (new Rect (Screen.width / 2 + 200, 90, 100, 50), "Start", gs)) {
								Application.LoadLevel("levelThree");
								PlayerPrefs.SetString("ScaleType", "one");
				}
			



				stringToEdit = "Hands Scale";


	        	stringToEdit = GUI.TextArea(new Rect(Screen.width / 2 - 200, 160, 400, 150), stringToEdit, gs);

				if (GUI.Button (new Rect (Screen.width / 2 + 200, 100 + 150, 100, 50), "Start", gs)) {
								Application.LoadLevel("levelThree");
								PlayerPrefs.SetString("ScaleType", "two");
				}
			




				stringToEdit = "Sphere Scale";


	        	stringToEdit = GUI.TextArea(new Rect(Screen.width / 2 - 200, 310, 400, 150), stringToEdit, gs);

				if (GUI.Button (new Rect (Screen.width / 2 + 200, 250 + 150, 100, 50), "Start", gs)) {
								Application.LoadLevel("levelThree");
								PlayerPrefs.SetString("ScaleType", "three");
				}
			


	}

}
