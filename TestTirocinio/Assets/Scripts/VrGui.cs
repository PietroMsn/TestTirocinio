using UnityEngine;
using System.Collections;


public class VrGui : VRGUI {

	private GUIStyle gs = null;
	private GUIStyle gsCounter = null;

		/* Metodo per creare i bottoni visibili tramite oculus, è stato necessario importare
		 * il package VRGUI nel progetto*/
		public override void OnVRGUI () {

			// creo un GUIStyle per impostare le dimensioni del font ultilizzato per i bottoni
			gs = new GUIStyle(GUI.skin.button);
			gs.fontSize = 30;


			// counter dei secondi da quando parte la scena
			float Crono = Time.time;
				
			float minutes = (int) Crono /60;
			float seconds = (int) Crono % 60;
			float milliseconds = (int) (Crono * 100) % 100;

			GUI.Label(new Rect(500, 10, 150, 50), minutes + "." + seconds + "." + milliseconds, gs);

			

			/* bottoni per l'attivazione/disattivazione dei movimenti per la rotazione e l'ingrandimento dell'oggetto.
			 * PlayerPrefs è una zona di memoria riservata alle prefenze scelte dall'utente, essa è accessibile 
			 * e mantiene i dati anche nei passaggi da una scena all'altra*/
			/*if (GUI.Button (new Rect (0, 10, 200, 200), "Attivare\nRotation", gs)) 
					PlayerPrefs.SetString("rotation", "true");*/


			
		}
		
	}

