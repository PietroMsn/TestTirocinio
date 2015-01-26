using UnityEngine;
using Leap;
using System;



public class CubeTestController : MonoBehaviour {

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
		private float previousScale = 0f;
		private float actualScale = 0f;
		private float moduloValore = 0f;
		private float TimeValue = 0f;
		private float count;
	
		private bool rotationBool = false;  // temp, da mettere nel menu
		private bool scaleBool = false;	   // temp, da mettere nel menu
		private bool rotateXBool = true;	// temp...
		private bool rotateYBool = true ;	// temp...
		private bool seeHandsBool = false;
		//private bool otherRotation = false;
		//private bool otherRotation2 = false;
		private bool PinchRotationBool = false;
		private bool PinchScaleBool = false;
		private bool translatePinch = false;

		private string choice = null;
		private string rotationSelection = null;
		private string scaleSelection = null;
		private string pinchRotationSelection = null;
		private string pinchScaleSelection = null;
		
		
		// Use this for initialization
		void Start () {

			LeapController = new Controller();

			LeapController.EnableGesture(Gesture.GestureType.TYPECIRCLE); // abilito il riconoscimento della gestre TYPECIRCLE
	
			// la visibilità dell'oggetto corrente viene messa di default a false
			gameObject.active = false;
			Target.active = false;
	
			// leggo nella stringa "choice" in PlayerPrefs, la quale viene impostata nel menù iniziale
		    // per la selezione dell'oggetto da manipolare
			choice = PlayerPrefs.GetString ("choice");

			// se l'oggetto corrente è l'oggetto selezionato, viene attivato
			if (choice.Equals ("cubo"))
				gameObject.active = true;

					
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

			// estraggo il contenuto delle stringhe di selezione impostate dalla VRGUI per
			// vedere quali gesture sono attive
			rotationSelection = PlayerPrefs.GetString ("rotation");
			scaleSelection = PlayerPrefs.GetString ("scale");
			pinchRotationSelection = PlayerPrefs.GetString ("pinchRotation");
			pinchScaleSelection = PlayerPrefs.GetString ("pinchScale");
			
			
			
			// faccio un controllo sulle stringhe e metto a true le variabili 
			// relative alle gesture che l'utente ha deciso di attivare
			if(pinchScaleSelection.Equals("true"))
					PinchScaleBool = true;
			else 
					PinchScaleBool = false;

			if(pinchRotationSelection.Equals("true"))
					PinchRotationBool = true;
			else 
					PinchRotationBool = false;
					
			if(rotationSelection.Equals("true"))
					rotationBool = true;
			else 
					rotationBool = false;

			if(scaleSelection.Equals("true"))
					scaleBool = true;
			else 
					scaleBool = false;

			
			
			if(seeHandsBool == true)
				seeHands();
			
			if(scaleBool == true)
				scale();

			if (translatePinch == true)
						TranslatePinch ();
			
			
			// quando seleziono la rotazione con il pinch, controllo che sia fatto con la mano destra e sia maggiore di un dato valore
			// chiamo la funzione per la rotazione
			if (PinchRotationBool) {
				
				if (RightHand.PinchStrength > 0.7f) {
					
					renderer.material = ColorBlue;
					
					if (RightHand.IsValid && LeftHand.IsValid)
						
						PinchRotation ();
					
				} else {
					renderer.material = ColorRed;
				}
			}
			
			
			
			
			if (RightHand.PinchStrength > 0.7f) {
				
				renderer.material = ColorBlue;
				
				if (RightHand.IsValid && LeftHand.IsValid)
					
					TranslatePinch();
				
			} else {
				renderer.material = ColorRed;
			}
			
			
			
			
			//faccio la stessa procedura con la scala
			if (PinchScaleBool) {
				
				if (RightHand.PinchStrength > 0.7f) {
					
					renderer.material = ColorBlue;
					
					if (RightHand.IsValid && LeftHand.IsValid)
						PinchScale ();
					
				} else {
					renderer.material = ColorRed;
				}
				
			}
			
			
			// se seleziono la rotazione normale, chiamo la funzione rotation	
			if(rotationBool)
				Rotation();
		
		}

	

		void Rotate(){

				roll = (LeftHand.PalmPosition.y * RightHand.PalmPosition.y) * 0.01f;
				pitch = (LeftHand.PalmPosition.z * RightHand.PalmPosition.z) * 0.01f;
				Vector3 RotationValue = new Vector3(0f,0f,0f);
				
				if(roll > 3f){
					RotationValue.y = roll;
					transform.Rotate(RotationValue, Time.deltaTime * 60f, Space.World);
				}
				
				if(pitch > 3f) { 
					
					RotationValue.z = pitch;
					transform.Rotate(RotationValue, Time.deltaTime * 60f, Space.World);
				}
				
				//metodo aggiornato, se è attivato il roll disattivo il pitch e viceversa (menu)
				
				if ((roll >= 3f || roll <= -3f) && rotateYBool == false){
					
					
					if (roll >= 3)
						transform.Rotate(RotationValue, Time.deltaTime * 90f, Space.World);
					else 
						transform.Rotate(Vector3.up, Time.deltaTime * -90f, Space.World);
					
					//transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth); // cambiare questa funzione, non è precisa!!!
				}
				
				if ((pitch >= 18f || pitch <= 13f) && rotateXBool == true){ 	
					
					if (pitch >= 18)
						transform.Rotate(Vector3.right, Time.deltaTime * -90f, Space.World);
					else 
						transform.Rotate(Vector3.right, Time.deltaTime * 90f, Space.World);
					
					//transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth); // cambiare questa funzione, non è precisa!!!
				}


		}

		
		void Rotation() {

			// rotazione rispetto all'asse dello spazio				
			if (LeftHand.IsValid && RightHand.IsValid) {	
							
							float roll = LeftHand.PalmNormal.Roll * -10f;
							float pitch = RightHand.PalmNormal.Pitch * -10f;
					
							//metodo aggiornato, se è attivato il roll disattivo il pitch e viceversa (menu)				
							if ((roll >= 3f || roll <= -3f) && rotateYBool == true) {
			        
									if (roll >= 3)
											transform.Rotate (Vector3.up, Time.deltaTime * 90f, Space.World);
									else 
											transform.Rotate (Vector3.up, Time.deltaTime * -90f, Space.World);
						
									//transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth); // cambiare questa funzione, non è precisa!!!
							}
					
							if ((pitch >= 18f || pitch <= 13f) && rotateXBool == true) {
			        	
		
									if (pitch >= 18)
											transform.Rotate (Vector3.right, Time.deltaTime * -90f, Space.World);
									else 
											transform.Rotate (Vector3.right, Time.deltaTime * 90f, Space.World);
						
									//transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth); // cambiare questa funzione, non è precisa!!!
							}
					}
					
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
					
				
					
					//metodo aggiornato, se è attivato il roll disattivo il pitch e viceversa (menu)
					if (LeftHand.IsValid || RightHand.IsValid) {
									if ((roll >= 3f || roll <= -3f) && rotateYBool == true) {
					        	
					        
											if (roll >= 3)
													transform.RotateAround (transform.position, transform.up, Time.deltaTime * 90f);
											else 
													transform.RotateAround (transform.position, transform.up, Time.deltaTime * -90f);
								
											//transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth); // cambiare questa funzione, non è precisa!!!
									}
							
									if ((pitch >= 18f || pitch <= 13f) && rotateXBool == true) {
					        	
				
											if (pitch >= 18)
													transform.RotateAround (transform.position, transform.right, Time.deltaTime * -90f);
											else 
													transform.RotateAround (transform.position, transform.right, Time.deltaTime * 90f);
								
											//transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth); // cambiare questa funzione, non è precisa!!!
									}
							}
		
		}
		

		/* funzione che ingrandisce e rimpicciolisce l'oggetto, da implementare metodo per 
				farlo smettere: dopo tot secondi che non rimpicciolisco (troppo: provious scale- actual scale <= valore basso
				chiedo all'utente se va bene la scala scelta, altrimenti torno a dove ero prima---- da settare tutti i parametri: previousScale
				actualScale, scaleBool (true se è selezionato scale), moduloVAlore, timeValue. Da implementare askToQuit!!!!*/
		
		void scale() {
			scaleValue = (LeftHand.PalmPosition.x * RightHand.PalmPosition.x) * 0.05f;
				
			if (LeftHand.IsValid || RightHand.IsValid) {
							if (scaleBool == true) {
									transform.doScale (scaleValue); 
									if (previousScale - actualScale >= moduloValore) {
								
											while (time == TimeValue)
													time = ExtensionMethods.startTime (count);
						
											ExtensionMethods.askToQuit ();  				
									} else
											transform.doScale (scaleValue);
							}
					}
		
		}

		
		void PinchScale() {

			//cambio la scala solo se tutte e due le mani sono presenti
			if (LeftHand.IsValid && RightHand.IsValid ) {

							float sphereDiameter = 2 * LeftHand.SphereRadius;
	
							transform.doScale (sphereDiameter * 100f);
					}
		}


		// funzione per la rotazione tramite la circle gesture
		void PinchRotation() {

			string clockwiseness = null;

			if (LeftHand.IsValid && RightHand.IsValid) {

							GestureList gesture = frame.Gestures ();
				
							for (int i = 0; i < gesture.Count; i++) {

									CircleGesture circle = new CircleGesture (gesture [i]);
									string state = circle.State.ToString ();

									if (state.Equals ("STATE_UPDATE")){
											
											if (circle.Pointable.Direction.AngleTo(circle.Normal) <= Math.PI/2) {
													//clockwiseness
													transform.Rotate (Vector3.up, Time.deltaTime * -90f, Space.World);
											}

											else {
													//clockwiseness
													transform.Rotate (Vector3.up, Time.deltaTime * 90f, Space.World);
											}
											
											
									}
							}
							
					}

			}
				
}

	

