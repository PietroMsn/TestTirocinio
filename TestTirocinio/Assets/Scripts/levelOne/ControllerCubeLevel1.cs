using UnityEngine;
using Leap;
using System;

// TODO: ------seeHands: mostro le dita (sfere, posso anche metterle invisibili) e le rendo tutte rigidbody
// per spostare il cubo
// ---- sistemo la pinch translation (deve avere la traslazione relativa!!!!)
// ---- per la traslazione quando tocco il cubo con la pallina (dito) si spostano assieme (modo per staccarsi????--> se mi allontano con tanta velocità dal cubo nella direzione opposta)
// ---- un'altra traslazione creo una sfera immaginaria sopra la leap, se con l'indice esco dalla sfera si sposta nella direzione dell'indice 


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

		
		public GameObject OptionScale;
		public GameObject OptionRotate;
		public Material ColorRed;
		public Material ColorBlue;

		public GameObject Target;
		
		public float roll;
		public float pitch;

		public float smooth = 5.0F;
		public float tiltAngle = 30.0F;
		public float scaleValue;
		
		private HandList hands;
		private Hand LeftHand;
		private Hand RightHand;
		
		private Vector RightPalmPosition;
		private Frame frame;
		private Frame previous;
		private Vector LeftPalmPosition;
		
		private Vector minus = new Vector(2f, 2f, 2f);

		private float pinch = 0;
		private float time = 0;
		//private float previousScale = 0f;//////////////////
		//private float actualScale = 0f;//////////////
		private float moduloValore = 0f;
		//private float TimeValue = 0f;///////////////////
		private float count;
		// temp...
		private bool seeHandsBool = false;
		private bool translatePinch = false;

		private bool reachedGoal = false;
		private Vector CubePosition = new Vector(2f, 2f, 2f);
		private Vector TargetPosition = new Vector(2f, 2f, 2f);
		private int goalCounter = 0;

		private Vector distance;
		
		
		// Use this for initialization
		void Start () {

			LeapController = new Controller();

			LeapController.EnableGesture(Gesture.GestureType.TYPECIRCLE); // abilito il riconoscimento della gestre TYPECIRCLE
	
			// la visibilità dell'oggetto corrente viene messa di default a false
			gameObject.active = true;
			Target.active = true;
	

			//lancio la funzione che posiziona il target per la prima volta
			refreshTargetPosition(CubePosition, TargetPosition);

			CubePosition.x = transform.position.x;
			CubePosition.y = transform.position.y;
			CubePosition.z = transform.position.z;

			TargetPosition.x = Target.transform.position.x;
			TargetPosition.y = Target.transform.position.y;
			TargetPosition.z = Target.transform.position.z;

					
			//for (int i = 0; i < 5; i++)
			//	palm1Finger[i].transform.position = new Vector3(0f, 0f, 0f);
			
			//for (int i = 0; i < 5; i++)
			//	palm2Finger[i].transform.position = new Vector3(0f, 0f, 0f);
			
			//FingerList allFingers = frame.Fingers;
					
		}
		
		// Update is called once per frame
		void Update () {

			frame = LeapController.Frame(); //The latest frame
			previous = LeapController.Frame(1); //The previous frame
			
			hands = frame.Hands;
			LeftHand = hands.Leftmost;
			RightHand = hands.Rightmost;
			
			LeftPalmPosition = LeftHand.PalmPosition;
			RightPalmPosition = RightHand.PalmPosition;
			
			
			
			
			
			seeHands();
			
			
			
			// chiamata al translate
			

				if (RightHand.PinchStrength > 0.7f) {
					
					renderer.material = ColorBlue;
					
					if (RightHand.IsValid && LeftHand.IsValid)
						
							TranslatePinch ();				
				} else {
					renderer.material = ColorRed;
				}
			
			
				// da rivedere completamente!!!!
			if((CubePosition.x - TargetPosition.x < 5 && CubePosition.x - TargetPosition.x > -5) 
				&& (CubePosition.y - TargetPosition.y < 5 && CubePosition.y - TargetPosition.y > -5)
				&& (CubePosition.z - TargetPosition.z < 5 && CubePosition.z - TargetPosition.z > -5))
				reachedGoal = true;


			if(reachedGoal) {
				renderer.material = ColorRed;
				refreshTargetPosition(CubePosition, TargetPosition);
				goalCounter += 1;
				reachedGoal = false;

			}

			if(goalCounter == 2){

				// passa al livello successivo
			}
		
		}


		void refreshTargetPosition(Vector CubePos, Vector TargetPos){
			Vector newPosition;
			// funzione random nel range, diversa da quella del cubo e diversa da quella precedente
		}
		

		
		// metodo per implementare la traslazione dell'oggetto tramite pinch
		void TranslatePinch(){
			distance = new Vector(CubePosition.x - RightPalmPosition.x, CubePosition.y - RightPalmPosition.y, CubePosition.z - RightPalmPosition.z);
			Debug.Log(RightPalmPosition.x +" " + RightPalmPosition.y + " " +RightPalmPosition.z);
			Debug.Log(CubePosition.x +" " + CubePosition.y + " " +CubePosition.z);
			Debug.Log(" ");

			//Cambiare riferimento!!!! *******************************************************
			//******************************************************************************
			//*****************************************************************************
			/*if((CubePosition.x - RightPalmPosition.x < 10 && CubePosition.x - RightPalmPosition.x > -10) 
				&& (CubePosition.y - RightPalmPosition.y < 10 && CubePosition.y - RightPalmPosition.y > -10)
				&& (CubePosition.z - RightPalmPosition.z < 10 && CubePosition.z - RightPalmPosition.z > -10))*/
				transform.position = new Vector3(RightHand.Fingers[1].TipPosition.x, RightHand.Fingers[0].TipPosition.y, -RightHand.Fingers[0].TipPosition.z); 
		}
		
		
		void seeHands() {

			// pinch = RightHand.PinchStrength;
			// funzione per muovere le due sfere che simulano i palmi delle mani
			// Vector3 normal1 = new Vector3(LeftHand.PalmNormal.x, LeftHand.PalmNormal.y, LeftHand.PalmNormal.z* -1);
			// Vector3 normal2 = new Vector3(RightHand.PalmNormal.x, RightHand.PalmNormal.y, RightHand.PalmNormal.z* -1);

					
			if (pinch > 0.7f)
				renderer.material = ColorRed;
			else 
				renderer.material = ColorBlue;

					
			/*if(!LeftHand.IsValid)
				palm[0].renderer.enabled = false;
			if(!RightHand.IsValid)
				palm[1].renderer.enabled = false;
					
			palm[0].transform.position = new Vector3(LeftPalmPosition.x , LeftPalmPosition.y , LeftPalmPosition.z  * (-1f));
			palm[1].transform.position = new Vector3(RightPalmPosition.x , RightPalmPosition.y , RightPalmPosition.z * (-1f));*/


			
			for (int f = 0; f < LeftHand.Fingers.Count; f++) {
				if(LeftHand.Fingers[f].IsValid)
    				leftFingersRep[f].transform.position = new Vector3(LeftHand.Fingers[f].TipPosition.x, LeftHand.Fingers[f].TipPosition.y, -LeftHand.Fingers[f].TipPosition.z);
			
    			else
    				leftFingersRep[f].active = false;
			}

			for (int f = 0; f < RightHand.Fingers.Count; f++){
				if(RightHand.Fingers[f].IsValid) 
    				rightFingersRep[f].transform.position = new Vector3(RightHand.Fingers[f].TipPosition.x, RightHand.Fingers[f].TipPosition.y, -RightHand.Fingers[f].TipPosition.z);
				else
					rightFingersRep[f].active = false;
			}
			//for( int i = 0; i < 5 ; i ++)
			//	palm1Finger[i].transform.position = new Vector3(frame.Fingers[0].TipPosition.x * 0.05f, frame.Fingers[0].TipPosition.y * 0.03f, frame.Fingers[0].TipPosition.z * -0.3f);
					
			//palm[0].transform.position = new Vector3(LeftPalmPosition.x, LeftPalmPosition.y, LeftPalmPosition.z);
			//palm[1].transform.position = new Vector3(RightPalmPosition.x, RightPalmPosition.y, RightPalmPosition.z);

					
			//for (int i = 0; i < 5; i++)
			//	palm1Finger[i].transform.position = new Vector3(LeftPalmPosition.x * 0.03f, LeftPalmPosition.y * 0.01f, LeftPalmPosition.z * -0.01f);
					
					
			

					
				
			
		
		}
											
						
				
}

	

