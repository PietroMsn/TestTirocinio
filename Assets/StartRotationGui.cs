using UnityEngine;
using System.Collections;

public class StartRotationGui : VRGUI {

	public override void OnVRGUI () {

			string stringToEdit = "";
		


				stringToEdit = "spostare l'indice nella direzione in cui si vuole far ruotare \nl'oggetto e far rientrare l'Europa nel riquadro";


	        	stringToEdit = GUI.TextArea(new Rect(Screen.width / 2 - 200, 10, 400, 150), stringToEdit);

				if (GUI.Button (new Rect (Screen.width / 2 + 200, 90, 100, 50), "Start")) {
								Application.LoadLevel("levelTwo");
								PlayerPrefs.SetString("RotationType", "one");
				}
			



				stringToEdit = "usare le due mani e ruotarle per far ruotare l'oggetto";


	        	stringToEdit = GUI.TextArea(new Rect(Screen.width / 2 - 200, 160, 400, 150), stringToEdit);

				if (GUI.Button (new Rect (Screen.width / 2 + 200, 100 + 150, 100, 50), "Start")) {
								Application.LoadLevel("levelTwo");
								PlayerPrefs.SetString("RotationType", "two");
				}
			




				stringToEdit = "inclinare le mani per ruotare rispetto ai due assi";


	        	stringToEdit = GUI.TextArea(new Rect(Screen.width / 2 - 200, 310, 400, 150), stringToEdit);

				if (GUI.Button (new Rect (Screen.width / 2 + 200, 250 + 150, 100, 50), "Start")) {
								Application.LoadLevel("levelTwo");
								PlayerPrefs.SetString("RotationType", "three");
				}
			


	}

}
