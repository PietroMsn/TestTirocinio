using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {


	// interfaccia del menu iniziale, per selezionare tramite bottoni quale oggetto sarà caricato nella scena
	// successiva. La scelta viene salvata in PlayerPrefs, quindi il valore della stringa "choice" non sarà
	// perso nonostante il passaggio ad un'altra scena.
	void OnGUI() {
			
		PlayerPrefs.SetString("rotation", "false");
		PlayerPrefs.SetString("scale", "false");
		PlayerPrefs.SetString("pinchRotation", "false");
		PlayerPrefs.SetString("pinchScale", "false");
		PlayerPrefs.SetString("newRotation", "false");
		PlayerPrefs.SetString("newScale", "false");
		PlayerPrefs.SetString("pinchTranslate", "false");
	
		if (GUI.Button (new Rect (Screen.width / 2 - 50, 10 + 150, 100, 50), "Load Head")) {
						Application.LoadLevel("Scene");				// carica la nuova scena
						PlayerPrefs.SetString("choice", "head");
				}

		if (GUI.Button (new Rect (Screen.width / 2 - 50, 70 + 150, 100, 50), "Load Cube")) {
						Application.LoadLevel("Scene");
						PlayerPrefs.SetString("choice", "cubo");
		}

		if (GUI.Button (new Rect (Screen.width / 2 - 50, 130 + 150, 100, 50), "Load Human")) {
						Application.LoadLevel("Scene");
						PlayerPrefs.SetString("choice", "human");
		}

		if (GUI.Button (new Rect (Screen.width / 2 - 50, 190 + 150, 100, 50), "Load Heart")) {
						Application.LoadLevel("Scene");
						PlayerPrefs.SetString("choice", "earth");
		}
	}
}