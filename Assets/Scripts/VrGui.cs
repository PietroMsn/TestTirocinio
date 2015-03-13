using UnityEngine;
using System.Collections;
using System.IO;


public class VrGui : VRGUI {

	private GUIStyle gs = null;
	private GUIStyle gsCounter = null;

	private static float Crono = 0f;
	private bool clickked = false;
				
	private float minutes = (int) Crono /60;
	private float seconds = (int) Crono % 60;
	private float milliseconds = (int) (Crono * 100) % 100;

		/* Metodo per creare i bottoni visibili tramite oculus, è stato necessario importare
		 * il package VRGUI nel progetto*/
		public override void OnVRGUI () {

			// creo un GUIStyle per impostare le dimensioni del font ultilizzato per i bottoni
			gs = new GUIStyle(GUI.skin.button);
			gs.fontSize = 30;


			// counter dei secondi da quando parte la scena

			if (GUI.Button (new Rect (Screen.width / 2 - 50, 10 + 150, 100, 50), "Start")) {
				if (!clickked){
					clickked = true;
					Crono = 0f;
				}
				else {
					clickked = false;
					if(PlayerPrefs.GetString("GestureChoice").Equals("rotation")){
						WriteResults();
						Application.LoadLevel("MainMenu");

					}

				}
			}

			if(clickked){

				Crono += Time.deltaTime;
					
				minutes = (int) Crono /60;
				seconds = (int) Crono % 60;
				milliseconds = (int) (Crono * 100) % 100;


			}
				GUI.Label(new Rect (Screen.width / 2 - 50, 10 + 150, 100, 50), minutes + "." + seconds + "." + milliseconds, gs);

			

				
			

			


			if(PlayerPrefs.GetString("Goal").Equals("true")){

				WriteResults();
				
			}


			

			/* bottoni per l'attivazione/disattivazione dei movimenti per la rotazione e l'ingrandimento dell'oggetto.
			 * PlayerPrefs è una zona di memoria riservata alle prefenze scelte dall'utente, essa è accessibile 
			 * e mantiene i dati anche nei passaggi da una scena all'altra*/
			/*if (GUI.Button (new Rect (0, 10, 200, 200), "Attivare\nRotation", gs)) 
					PlayerPrefs.SetString("rotation", "true");*/


			
		}

		private void WriteResults(){

			string path = @"C:\Users\Pietro\Desktop\MyTest\MyTest.txt";
		        // This text is added only once to the file.
		        if (!File.Exists(path)) 
		        {
		            // Create a file to write to.
		            using (StreamWriter sw = File.CreateText(path)) 
		            {
		                sw.WriteLine("Utente");
		                sw.WriteLine(PlayerPrefs.GetString("GestureChoice"));
		                sw.WriteLine("Welcome");
		            }	
		        }

		        // This text is always added, making the file longer over time
		        // if it is not deleted.
		        using (StreamWriter sw = File.AppendText(path)) 
		        {
		            sw.WriteLine("Utente: " );
		            sw.WriteLine(PlayerPrefs.GetString("GestureChoice"));
		            if(PlayerPrefs.GetString("GestureChoice").Equals("translation"))
		            	sw.WriteLine(" " + PlayerPrefs.GetString("TranslateType"));
		            if(PlayerPrefs.GetString("GestureChoice").Equals("rotation"))
		            	sw.WriteLine(" " + PlayerPrefs.GetString("RotationType"));
		            if(PlayerPrefs.GetString("GestureChoice").Equals("scale"))
		            	sw.WriteLine(" " + PlayerPrefs.GetString("ScaleType"));
		            sw.WriteLine(minutes + "." + seconds + "." + milliseconds);
		            sw.WriteLine(" ");
		        }

				PlayerPrefs.SetString("Goal", "false");
				minutes = 0;
				seconds = 0;
				milliseconds = 0;



		}
		
	}

