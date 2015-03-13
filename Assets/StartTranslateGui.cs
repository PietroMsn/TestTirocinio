using UnityEngine;
using System.Collections;

public class StartTranslateGui : VRGUI {

	public override void OnVRGUI () {

			string stringToEdit = "";
		

			

				stringToEdit = "selezionare l'oggetto stringendolo tra pollice e indice e poi trascinarlo sul bersaglio";


	        	stringToEdit = GUI.TextArea(new Rect(Screen.width / 2 - 200, 10, 400, 150), stringToEdit);

				if (GUI.Button (new Rect (Screen.width / 2 + 200, 90, 100, 50), "Start")) {
								Application.LoadLevel("levelOne");
								PlayerPrefs.SetString("TranslateType", "one");
				}
			


			

				stringToEdit = "fare il tap per selezionare l'oggetto e trascinare verso il bersaglio";


	        	stringToEdit = GUI.TextArea(new Rect(Screen.width / 2 - 200, 160, 400, 150), stringToEdit);

				if (GUI.Button (new Rect (Screen.width / 2 + 200, 100 + 150, 100, 50), "Start")) {
								Application.LoadLevel("levelOne");
								PlayerPrefs.SetString("TranslateType", "two");
				}
			


			


				stringToEdit = "selezionare l'oggetto con l'indice e poi trascinare verso il bersagio";


	        	stringToEdit = GUI.TextArea(new Rect(Screen.width / 2 - 200, 310, 400, 150), stringToEdit);

				if (GUI.Button (new Rect (Screen.width / 2 + 200, 250 + 150, 100, 50), "Start")) {
								Application.LoadLevel("levelOne");
								PlayerPrefs.SetString("TranslateType", "three");
				}
			


	}

}
