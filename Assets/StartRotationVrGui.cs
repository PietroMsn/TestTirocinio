using UnityEngine;
using System.Collections;

public class StartRotationVrGui : VRGUI {

	private GUIStyle gs = null;


	public override void OnVRGUI () {

			string stringToEdit = "";
			
			gs = new GUIStyle(GUI.skin.button);
			gs.fontSize = 30;


				stringToEdit = "Finger Rotation";


	        	stringToEdit = GUI.TextArea(new Rect(Screen.width / 2 - 200, 10, 400, 150), stringToEdit, gs);

				if (GUI.Button (new Rect (Screen.width / 2 + 200, 90, 100, 50), "Start", gs)) {
								Application.LoadLevel("levelTwo");
								PlayerPrefs.SetString("RotationType", "one");
				}
			



				stringToEdit = "Hands Rotation";


	        	stringToEdit = GUI.TextArea(new Rect(Screen.width / 2 - 200, 160, 400, 150), stringToEdit, gs);

				if (GUI.Button (new Rect (Screen.width / 2 + 200, 100 + 150, 100, 50), "Start", gs)) {
								Application.LoadLevel("levelTwo");
								PlayerPrefs.SetString("RotationType", "two");
				}
			




				stringToEdit = "My Rotation";


	        	stringToEdit = GUI.TextArea(new Rect(Screen.width / 2 - 200, 310, 400, 150), stringToEdit, gs);

				if (GUI.Button (new Rect (Screen.width / 2 + 200, 250 + 150, 100, 50), "Start", gs)) {
								Application.LoadLevel("levelTwo");
								PlayerPrefs.SetString("RotationType", "three");
				}
			


	}

}
