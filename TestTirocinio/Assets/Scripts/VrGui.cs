using UnityEngine;
using System.Collections;


public class VrGui : VRGUI {

	private GUIStyle gs = null;

		/* Metodo per creare i bottoni visibili tramite oculus, è stato necessario importare
		 * il package VRGUI nel progetto*/
		public override void OnVRGUI () {

			// creo un GUIStyle per impostare le dimensioni del font ultilizzato per i bottoni
			gs = new GUIStyle(GUI.skin.button);
			gs.fontSize = 30;
			

			/* bottoni per l'attivazione/disattivazione dei movimenti per la rotazione e l'ingrandimento dell'oggetto.
			 * PlayerPrefs è una zona di memoria riservata alle prefenze scelte dall'utente, essa è accessibile 
			 * e mantiene i dati anche nei passaggi da una scena all'altra*/
			if (GUI.Button (new Rect (0, 10, 200, 200), "Attivare\nRotation", gs)) 
					PlayerPrefs.SetString("rotation", "true");

			if (GUI.Button (new Rect (200, 10, 200, 200), "Disattivare\nRotation", gs)) 
					PlayerPrefs.SetString("rotation", "false");

			if (GUI.Button (new Rect (0, 210, 200, 200), "Attivare\nScale", gs)) 
					PlayerPrefs.SetString("scale", "true");

			if (GUI.Button (new Rect (200, 210, 200, 200), "Disattivare\nScale", gs)) 
					PlayerPrefs.SetString("scale", "false");



			if (GUI.Button (new Rect (950, 10, 200, 200), "Attivare\nPinch Rotation", gs)) 
					PlayerPrefs.SetString("pinchRotation", "true");
			
			if (GUI.Button (new Rect (1150, 10, 200, 200), "Disattivare\nPinch Rotation", gs)) 
					PlayerPrefs.SetString("pinchRotation", "false");
			
			if (GUI.Button (new Rect (950, 210, 200, 200), "Attivare\nPinch Scale", gs)) 
					PlayerPrefs.SetString("pinchScale", "true");
			
			if (GUI.Button (new Rect (1150, 210, 200, 200), "Disattivare\nPinch Scale", gs)) 
					PlayerPrefs.SetString("pinchScale", "false");


			// cambia la posizione dei bottoni

			if (GUI.Button (new Rect (0, 410, 200, 200), "Attivare\nNew Rotation", gs)) 
					PlayerPrefs.SetString("newRotation", "true");
			
			if (GUI.Button (new Rect (200, 410, 200, 200), "Disattivare\nNew Rotation", gs)) 
					PlayerPrefs.SetString("newRotation", "false");
			
			if (GUI.Button (new Rect (400, 410, 200, 200), "Attivare\nPinch Translate", gs)) 
					PlayerPrefs.SetString("pinchTranslate", "true");

			if (GUI.Button (new Rect (600, 410, 200, 200), "Disattivare\nPinch Translate", gs)) 
					PlayerPrefs.SetString("pinchTranslate", "false");
			
			if (GUI.Button (new Rect (800, 410, 200, 200), "Attivare\nNew Scale", gs)) 
					PlayerPrefs.SetString("newScale", "true");
			
			if (GUI.Button (new Rect (1000, 410, 200, 200), "Disattivare\nNew Scale", gs)) 
					PlayerPrefs.SetString("newScale", "false");


			
		}
		
	}

