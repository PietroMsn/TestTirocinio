using UnityEngine;
using System.Collections;

public class StartScaleGui : VRGUI {

	public override void OnVRGUI () {

			string stringToEdit = "";
		


				stringToEdit = "allargare pollice e indice o avvicinarli per ingrandire o rimpicciolire";


	        	stringToEdit = GUI.TextArea(new Rect(Screen.width / 2 - 200, 10, 400, 150), stringToEdit);

				if (GUI.Button (new Rect (Screen.width / 2 + 200, 90, 100, 50), "Start")) {
								Application.LoadLevel("levelThree");
								PlayerPrefs.SetString("ScaleType", "one");
				}
			



				stringToEdit = "allontanare o avvicinare le mani per ingrandire/rimpicciolire";


	        	stringToEdit = GUI.TextArea(new Rect(Screen.width / 2 - 200, 160, 400, 150), stringToEdit);

				if (GUI.Button (new Rect (Screen.width / 2 + 200, 100 + 150, 100, 50), "Start")) {
								Application.LoadLevel("levelThree");
								PlayerPrefs.SetString("ScaleType", "two");
				}
			




				stringToEdit = "allargare o stringere la mano per ingrandire/rimpicciolire";


	        	stringToEdit = GUI.TextArea(new Rect(Screen.width / 2 - 200, 310, 400, 150), stringToEdit);

				if (GUI.Button (new Rect (Screen.width / 2 + 200, 250 + 150, 100, 50), "Start")) {
								Application.LoadLevel("levelThree");
								PlayerPrefs.SetString("ScaleType", "three");
				}
			


	}

}
