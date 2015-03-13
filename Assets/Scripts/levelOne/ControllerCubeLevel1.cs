using UnityEngine;
using Leap;
//using System;


// TODO: ------seeHands: mostro le dita (sfere, posso anche metterle invisibili) e le rendo tutte rigidbody
// per spostare il cubo
// ---- sistemo la pinch translation (deve avere la traslazione relativa!!!!)
// ---- per la traslazione quando tocco il cubo con la pallina (dito) si spostano assieme (modo per staccarsi????--> se mi allontano con tanta velocità dal cubo nella direzione opposta)
// ---- un'altra traslazione creo una sfera immaginaria sopra la leap, se con l'indice esco dalla sfera si sposta nella direzione dell'indice 
// ---- disegnare la mano completa anche con il palmo (sfera schiacciata)

// ---- la scala la metti in modo che sopra/sotto una certa soglia continua ad ingrandire/rimpicciolire

// ---- implementare il primo livello (traslazione) con i tre movimenti (basta usare i boolean o sostituire i metodi)
// ---- implementare il livello della scala, ingrandire e rimpicciolire una sfera (tenere a una dimensione per tot secondi (potrebbero bastare 3))
// ---- implementare la ricerca dell'italia nella terra, controlla le coordinate giuste (costanti) e comparale con la rotazione attuale dell'oggetto
// ---- carico un oggetto con una scritta e devo trovarla e scriverla (parola random)

public class ControllerCubeLevel1 : MonoBehaviour {

		Controller LeapController;

		public GameObject[] palm;
		public GameObject[] rightFingersRep;
		public GameObject[] leftFingersRep;

		private FingerList allFingers;

	
		public Material ColorRed;
		public Material ColorBlue;

		public GameObject Target;
		
		
		private HandList hands;
		private Hand LeftHand;
		private Hand RightHand;
		
		private Vector RightPalmPosition;
		private Frame frame;
		//private Frame previous;
		private Vector LeftPalmPosition;
		
		//private Vector minus = new Vector(2f, 2f, 2f);

		//private float pinch = 0;
		//private float time = 0;

		private bool seeHandsBool = true;
		private bool translatePinch = false;
		private bool translateFinger = false;
		private bool twoFingerTranslate = false;

		private bool isSelected = false;

		private bool reachedGoal = false;
		private Vector CubePosition = new Vector(2f, 2f, 2f);
		private Vector TargetPosition = new Vector(2f, 2f, 2f);
		private int goalCounter = 0;

		private float TargetX;
		private float TargetY;
		private float constant = 0.05f;
		private string Scelta;
		
		
		// Use this for initialization
		void Start () {

			LeapController = new Controller();

			//LeapController.EnableGesture(Gesture.GestureType.TYPECIRCLE); // abilito il riconoscimento della gestre TYPECIRCLE
			LeapController.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
	
			// la visibilità dell'oggetto corrente viene messa di default a false
			gameObject.active = true;
			Target.active = true;

			Scelta = PlayerPrefs.GetString("TranslateType");

			if(Scelta.Equals("one"))
				translatePinch = true;

			else if (Scelta.Equals("two"))
				translateFinger = true;

			else if (Scelta.Equals("three"))
				twoFingerTranslate = true;



	

			//lancio la funzione che posiziona il target per la prima volta
			refreshTargetPosition();

					
		}
		
		// Update is called once per frame
		void Update () {


			CubePosition.x = transform.position.x;
			CubePosition.y = transform.position.y;
			CubePosition.z = transform.position.z;

			TargetPosition.x = Target.transform.position.x;
			TargetPosition.y = Target.transform.position.y;
			TargetPosition.z = Target.transform.position.z;

			frame = LeapController.Frame(); //The latest frame
			//previous = LeapController.Frame(1); //The previous frame
			
			hands = frame.Hands;
			LeftHand = hands.Leftmost;
			RightHand = hands.Rightmost;
			
			LeftPalmPosition = LeftHand.PalmPosition;
			RightPalmPosition = RightHand.PalmPosition;

			if(seeHandsBool)
				seeHands();
			
			// chiamata al translate

			if(translateFinger)
				TranslateFingerPosition();
			
			//Debug.Log(RightHand.PinchStrength);
			if(translatePinch){
		
					if (RightHand.PinchStrength > 0.5f) {
						//Debug.Log(RightHand.PinchStrength);
						renderer.material = ColorBlue;
						
						if (RightHand.IsValid && LeftHand.IsValid)
							
								TranslatePinch ();				
					} else {
						renderer.material = ColorRed;
					}
				
			}
			if(twoFingerTranslate)
				TwoFingerTranslate();

			/*if(CubePosition.x < -2f || CubePosition.x > 8f || CubePosition.y < -2f || CubePosition.y > 8f)
				refreshCubePosition();*/
			
			
			
			if(CubePosition.x  < TargetPosition.x + 1 
				&& CubePosition.x > TargetPosition.x - 1 
				&& CubePosition.y  < TargetPosition.y + 1
				&& CubePosition.y > TargetPosition.y - 1)
				//&& (CubePosition.x  < TargetPosition.x + 0.1 && CubePosition.x > TargetPosition.x -0.5)
				
				reachedGoal = true;

			

			//Debug.Log(reachedGoal);

			if(reachedGoal) {
				//renderer.material = ColorRed;
				refreshTargetPosition();
				refreshCubePosition();
				isSelected = false;
				goalCounter += 1;
				reachedGoal = false;	

			}

			if(goalCounter == 10){
				Debug.Log(" Passa al prossimo livello");
				Application.LoadLevel("MainMenu");
				PlayerPrefs.SetString("Goal", "true");
				// passa al livello successivo
			}
		
		}

		

		void refreshTargetPosition(){
	
			TargetX = Random.Range(0f, 8f);
			TargetY = Random.Range(5f, 20f);

			Target.transform.position = new Vector3(TargetX, TargetY, transform.position.z);
			// funzione random nel range, diversa da quella del cubo e diversa da quella precedente
		}

		void refreshCubePosition(){
	
			TargetX = Random.Range(0f, 8f);
			TargetY = Random.Range(5f, 20f);

			transform.position = new Vector3(TargetX, TargetY, transform.position.z);
			// funzione random nel range, diversa da quella del cubo e diversa da quella precedente
		}
		

		
		// metodo per implementare la traslazione dell'oggetto tramite pinch
		void TranslatePinch(){

			//Debug.Log(CubePosition + " (" + RightHand.Fingers[1].TipPosition.x * constant + "," + RightHand.Fingers[1].TipPosition.y * constant + "," + -RightHand.Fingers[1].TipPosition.z * constant + ") ");

			if(RightHand.Fingers[1].TipPosition.x * constant > CubePosition.x - 1.5f && RightHand.Fingers[1].TipPosition.x * constant < CubePosition.x + 1.5f
					&& RightHand.Fingers[1].TipPosition.y * constant > CubePosition.y - 1.5f && RightHand.Fingers[1].TipPosition.y * constant < CubePosition.y + 1.5f)

				transform.position = new Vector3(RightHand.Fingers[1].TipPosition.x * constant, RightHand.Fingers[1].TipPosition.y * constant, -RightHand.Fingers[1].TipPosition.z * constant); 

		}


		void TranslateFingerPosition(){

				float[] fingerPositionX = new float[5];
				float[] fingerPositionY = new float[5];
				float[] fingerPositionZ = new float[5];

				for (int f = 0; f < RightHand.Fingers.Count; f++) {
					fingerPositionX[f] = RightHand.Fingers[f].TipPosition.x * constant;
					fingerPositionY[f] = RightHand.Fingers[f].TipPosition.y * constant;
					fingerPositionZ[f] = RightHand.Fingers[f].TipPosition.z * constant;
				}

				GestureList gesture = frame.Gestures();

				for (int i = 0; i < gesture.Count; i++) {
					ScreenTapGesture tap = new ScreenTapGesture (gesture[i]);

				string state = tap.State.ToString();
				Vector tapPosition = new Vector(tap.Position.x * constant, tap.Position.y * constant , tap.Position.z * -constant);

				//Debug.Log(state + "\n" + tapPosition + "\n" + isSelected + "\n");

				//if(state.Equals("STATE_UPDATE")){

					

					if(tapPosition.x > transform.position.x - 2.5f && tapPosition.x < transform.position.x + 2.5f
						&& tapPosition.y > transform.position.y - 2.5f && tapPosition.y < transform.position.y + 2.5f && tap.IsValid){

								if(!isSelected){
									isSelected = true; 
									renderer.material = ColorRed;
								}
								else{
									isSelected = false;
									renderer.material = ColorBlue;
								}

					}

					
				//}
			}	
				if(isSelected){


						if(fingerPositionX[1] > transform.position.x)
								transform.position = new Vector3(transform.position.x + 1f , transform.position.y, transform.position.z);

						if(fingerPositionX[1] <= transform.position.x)
								transform.position = new Vector3(transform.position.x - 1f , transform.position.y, transform.position.z);

						if(fingerPositionY[1] > transform.position.y)
								transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);

						if(fingerPositionY[1] <= transform.position.y)
								transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
				}
		}

		void TwoFingerTranslate() {

			float[] RightFingerPositionX = new float[5];
			float[] RightFingerPositionY = new float[5];
			float[] RightFingerPositionZ = new float[5];

				for (int f = 0; f < RightHand.Fingers.Count; f++) {
					RightFingerPositionX[f] = RightHand.Fingers[f].TipPosition.x * constant;
					RightFingerPositionY[f] = RightHand.Fingers[f].TipPosition.y * constant;
					RightFingerPositionZ[f] = RightHand.Fingers[f].TipPosition.z * constant;
				}

				float[] LeftFingerPositionX = new float[5];
				float[] LeftFingerPositionY = new float[5];
				float[] LeftFingerPositionZ = new float[5];

				for (int f = 0; f < RightHand.Fingers.Count; f++) {
					LeftFingerPositionX[f] = LeftHand.Fingers[f].TipPosition.x * constant;
					LeftFingerPositionY[f] = LeftHand.Fingers[f].TipPosition.y * constant;
					LeftFingerPositionZ[f] = LeftHand.Fingers[f].TipPosition.z * constant;
				}
			if(  RightFingerPositionX[1] < CubePosition.x + 1.5f 
				&& RightFingerPositionY[1] > CubePosition.y - 1.5f &&  RightFingerPositionY[1] < CubePosition.y + 1.5f)
					transform.position = new Vector3(RightFingerPositionX[1], RightFingerPositionY[1], transform.position.z);

				Debug.Log(LeftFingerPositionX[1] + " " + LeftFingerPositionY[1]);	

		}

		
		
		
		void seeHands() {

			// funzione per muovere le due sfere che simulano i palmi delle mani
			// Vector3 normal1 = new Vector3(LeftHand.PalmNormal.x, LeftHand.PalmNormal.y, LeftHand.PalmNormal.z* -1);
			// Vector3 normal2 = new Vector3(RightHand.PalmNormal.x, RightHand.PalmNormal.y, RightHand.PalmNormal.z* -1);

			//Debug.Log("entra in see hands");

					
			/*if (pinch > 0.7f)
				renderer.material = ColorRed;
			else 
				renderer.material = ColorBlue;*/

					
			/*if(!LeftHand.IsValid)
				palm[0].renderer.enabled = false;
			if(!RightHand.IsValid)
				palm[1].renderer.enabled = false;
			
			palm[0].transform.position = new Vector3(LeftPalmPosition.x * constant, LeftPalmPosition.y * constant, LeftPalmPosition.z  * (-1f)* constant);
			palm[1].transform.position = new Vector3(RightPalmPosition.x * constant, RightPalmPosition.y * constant, RightPalmPosition.z * (-1f)* constant);

			*/
			
			for (int f = 0; f < LeftHand.Fingers.Count; f++) {
				    				//Debug.Log("sono nel for");

				if(LeftHand.Fingers[f].IsValid)

    				leftFingersRep[f].transform.position = new Vector3(LeftHand.Fingers[f].TipPosition.x * constant, LeftHand.Fingers[f].TipPosition.y * constant, -LeftHand.Fingers[f].TipPosition.z * constant);

    			else

    				leftFingersRep[f].active = false;
			}

			for (int f = 0; f < RightHand.Fingers.Count; f++){

				if(RightHand.Fingers[f].IsValid) 

    				rightFingersRep[f].transform.position = new Vector3(RightHand.Fingers[f].TipPosition.x * constant, RightHand.Fingers[f].TipPosition.y * constant, -RightHand.Fingers[f].TipPosition.z * constant);
				
				else

					rightFingersRep[f].active = false;
			}
			
			
		
		}
											
						
				
}

	

