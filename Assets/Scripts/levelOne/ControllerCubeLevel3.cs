using UnityEngine;
using Leap;
using System;

// TODO: ------seeHands: mostro le dita (sfere, posso anche metterle invisibili) e le rendo tutte rigidbody
// per spostare il cubo
// ----sistemo la pinch translation (deve avere la traslazione relativa!!!!)
// ----trova l'ultimo movimento per la traslazione
// ----la scala la metti in modo che sopra/sotto una certa soglia continua ad ingrandire/rimpicciolire

// ---- implementare il primo livello (traslazione) con i tre movimenti (basta usare i boolean o sostituire i metodi)
// ---- implementare il livello della scala, ingrandire e rimpicciolire una sfera (tenere a una dimensione per tot secondi (potrebbero bastare 3))
// ---- implementare la ricerca dell'italia nella terra, controlla le coordinate giuste (costanti) e comparale con la rotazione attuale dell'oggetto


public class ControllerCubeLevel3 : MonoBehaviour {

		Controller LeapController;

		public GameObject[] palm;
		
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
		private float previousScale = 0f;//////////////////
		private float actualScale = 0f;//////////////
		private float moduloValore = 0f;
		private float TimeValue = 0f;///////////////////
		private float count;
		// temp...
		private bool seeHandsBool = false;
		private bool translatePinch = false;

		private bool reachedGoal = false;
		private Vector CubePosition;
		private Vector TargetPosition;
		private int goalCounter = 0;
		
		
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

			// se l'oggetto corrente è l'oggetto selezionato, viene attivato
			//if (choice.Equals ("cubo"))
			//	gameObject.active = true;

					
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
			
			//FingerList allFingers = frame.Fingers;
			
			
			if(seeHandsBool == true)
				seeHands();
			
			
			
			// chiamata al translate
			if(translatePinch == true){

				if (RightHand.PinchStrength > 0.7f) {
					
					renderer.material = ColorBlue;
					
					if (RightHand.IsValid && LeftHand.IsValid)
						
							TranslatePinch ();				
				} else {
					renderer.material = ColorRed;
				}
			}
			

			if(CubePosition.Equals(TargetPosition))
				reachedGoal = true;


			if(reachedGoal) {

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
			transform.position = new Vector3(RightPalmPosition.x, RightPalmPosition.y, RightPalmPosition.z); 
		}
		
		
		void seeHands() {

			pinch = RightHand.PinchStrength;
			// funzione per muovere le due sfere che simulano i palmi delle mani
			Vector3 normal1 = new Vector3(LeftHand.PalmNormal.x, LeftHand.PalmNormal.y, LeftHand.PalmNormal.z* -1);
			Vector3 normal2 = new Vector3(RightHand.PalmNormal.x, RightHand.PalmNormal.y, RightHand.PalmNormal.z* -1);

					
			if (pinch > 0.7f)
				renderer.material = ColorRed;
			else 
				renderer.material = ColorBlue;

					
			if(!LeftHand.IsValid)
				palm[0].renderer.enabled = false;
			if(!RightHand.IsValid)
				palm[1].renderer.enabled = false;
					
			palm[0].transform.position = new Vector3(LeftPalmPosition.x , LeftPalmPosition.y , LeftPalmPosition.z  * (-1f));
			palm[1].transform.position = new Vector3(RightPalmPosition.x , RightPalmPosition.y , RightPalmPosition.z * (-1f));
					
			palm[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, normal1);
			palm[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, normal2);
			
				
			//for( int i = 0; i < 5 ; i ++)
			//	palm1Finger[i].transform.position = new Vector3(frame.Fingers[0].TipPosition.x * 0.05f, frame.Fingers[0].TipPosition.y * 0.03f, frame.Fingers[0].TipPosition.z * -0.3f);
					
			//palm[0].transform.position = new Vector3(LeftPalmPosition.x, LeftPalmPosition.y, LeftPalmPosition.z);
			//palm[1].transform.position = new Vector3(RightPalmPosition.x, RightPalmPosition.y, RightPalmPosition.z);

					
			//for (int i = 0; i < 5; i++)
			//	palm1Finger[i].transform.position = new Vector3(LeftPalmPosition.x * 0.03f, LeftPalmPosition.y * 0.01f, LeftPalmPosition.z * -0.01f);
					
					
			//rotazione rispetto all'asse dell'oggetto
			float roll = LeftHand.PalmNormal.Roll * -10f;
			float pitch = RightHand.PalmNormal.Pitch * -10f;
					
				
		
		}
											
						
				
}

	

