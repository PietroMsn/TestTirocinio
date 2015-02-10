using UnityEngine;
using Leap;
//using System;


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

		//public GameObject[] palm;
		public GameObject[] rightFingersRep;
		public GameObject[] leftFingersRep;

		private FingerList allFingers;

		//public GameObject OptionScale;
		//public GameObject OptionRotate;
		//public Material ColorRed;
		//public Material ColorBlue;

		public GameObject Target;
		
		//public float roll;
		//public float pitch;

		//public float smooth = 5.0F;
		//public float tiltAngle = 30.0F;
		//public float scaleValue;
		
		private HandList hands;
		private Hand LeftHand;
		private Hand RightHand;
		
		//private Vector RightPalmPosition;
		private Frame frame;
		private Frame previous;
		//private Vector LeftPalmPosition;
		
		//private Vector minus = new Vector(2f, 2f, 2f);

		//private float pinch = 0;
		//private float time = 0;
		//private float previousScale = 0f;//////////////////
		//private float actualScale = 0f;//////////////
		//private float moduloValore = 0f;
		//private float TimeValue = 0f;///////////////////
		//private float count;
		// temp...
		//private bool seeHandsBool = false;
		private bool translatePinch = false;
		private bool translateFinger = true;

		private bool reachedGoal = false;
		private Vector CubePosition = new Vector(2f, 2f, 2f);
		private Vector TargetPosition = new Vector(2f, 2f, 2f);
		private int goalCounter = 0;

		//private Vector distance;

		private float TargetX;
		private float TargetY;
		private float constant = 0.05f;
		//protected const float PALM_CENTER_OFFSET = 0.0150f;
		//protected bool mirror_z_axis_ = false;
		
		
		// Use this for initialization
		void Start () {

			LeapController = new Controller();

			//LeapController.EnableGesture(Gesture.GestureType.TYPECIRCLE); // abilito il riconoscimento della gestre TYPECIRCLE
			LeapController.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
	
			// la visibilità dell'oggetto corrente viene messa di default a false
			gameObject.active = true;
			Target.active = true;
	

			//lancio la funzione che posiziona il target per la prima volta
			refreshTargetPosition();

			

					
			//for (int i = 0; i < 5; i++)
			//	palm1Finger[i].transform.position = new Vector3(0f, 0f, 0f);
			
			//for (int i = 0; i < 5; i++)
			//	palm2Finger[i].transform.position = new Vector3(0f, 0f, 0f);
			
			//FingerList allFingers = frame.Fingers;
					
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
			previous = LeapController.Frame(1); //The previous frame
			
			hands = frame.Hands;
			LeftHand = hands.Leftmost;
			RightHand = hands.Rightmost;
			
			//LeftPalmPosition = LeftHand.PalmPosition;
			//RightPalmPosition = RightHand.PalmPosition;

			//Debug.Log(LeftPalmPosition + "  " + RightPalmPosition);
			
			seeHands();
			
			// chiamata al translate

			if(translateFinger)
				TranslateFingerPosition();
			
			//Debug.Log(RightHand.PinchStrength);
			if(translatePinch){
				if(RightHand.Fingers[2].TipPosition.x * constant > CubePosition.x - 5 
					&& RightHand.Fingers[2].TipPosition.x * constant > CubePosition.x + 5
					&& RightHand.Fingers[2].TipPosition.y * constant > CubePosition.y - 5 
					&& RightHand.Fingers[2].TipPosition.y * constant > CubePosition.y + 5){
					if (RightHand.PinchStrength > 0.5f) {
						//Debug.Log(RightHand.PinchStrength);
						//renderer.material = ColorBlue;
						
						if (RightHand.IsValid && LeftHand.IsValid)
							
								TranslatePinch ();				
					} else {
						//renderer.material = ColorRed;
					}
				}
			}
			
			
			
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
				goalCounter += 1;
				reachedGoal = false;	

			}

			if(goalCounter == 5){
				Debug.Log(" Passa al prossimo livello");
				// passa al livello successivo
			}
		
		}

		

		void refreshTargetPosition(){
			Vector newPosition;
			Random rnd = new Random();
			TargetX = Random.Range(-3f, 10f);
			TargetY = Random.Range(3f, 15f);
			//TargetZ = rnd.Next(1, 13);
			Target.transform.position = new Vector3(TargetX, TargetY, transform.position.z);
			// funzione random nel range, diversa da quella del cubo e diversa da quella precedente
		}
		

		
		// metodo per implementare la traslazione dell'oggetto tramite pinch
		void TranslatePinch(){
		
			//Debug.Log(RightHand.Fingers[1].TipPosition);
			//distance = new Vector(CubePosition.x - RightPalmPosition.x, CubePosition.y - RightPalmPosition.y, CubePosition.z - RightPalmPosition.z);
			//Debug.Log(RightPalmPosition.x +" " + RightPalmPosition.y + " " +RightPalmPosition.z);
			//Debug.Log(CubePosition.x +" " + CubePosition.y + " " +CubePosition.z);
			//Debug.Log(" ");

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

				if(fingerPositionX[1] > transform.position.x)
					transform.position = new Vector3(transform.position.x + 1f , transform.position.y, transform.position.z);

				if(fingerPositionX[1] <= transform.position.x)
					transform.position = new Vector3(transform.position.x - 1f , transform.position.y, transform.position.z);

				if(fingerPositionY[1] > transform.position.y)
					transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);

				if(fingerPositionY[1] <= transform.position.y)
					transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

				//Debug.Log(transform.position);
				//Debug.Log(fingerPositionX[1] + "," + fingerPositionY[1] + "," + fingerPositionZ[1]);
				//Debug.Log("\n");


	}

		
		
		
		void seeHands() {

			// pinch = RightHand.PinchStrength;
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
					
			palm[0].transform.position = new Vector3(LeftPalmPosition.x , LeftPalmPosition.y , LeftPalmPosition.z  * (-1f));
			palm[1].transform.position = new Vector3(RightPalmPosition.x , RightPalmPosition.y , RightPalmPosition.z * (-1f));*/


			
			for (int f = 0; f < LeftHand.Fingers.Count; f++) {
				    				//Debug.Log("sono nel for");

				if(LeftHand.Fingers[f].IsValid){
    				leftFingersRep[f].transform.position = new Vector3(LeftHand.Fingers[f].TipPosition.x * constant, LeftHand.Fingers[f].TipPosition.y * constant, -LeftHand.Fingers[f].TipPosition.z * constant);
    				//Debug.Log("sono nel for");
    				//LeftHand.Fingers[f].bone(Bone.TYPE_METACARPAL);

				}
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

	

