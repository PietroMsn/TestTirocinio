using UnityEngine;
using Leap;
using System;



public class ObjectControllerEarth : MonoBehaviour {
	
	Controller LeapController;
	
	
	public GameObject[] palm;
	
	public GameObject OptionScale;
	public GameObject OptionRotate;
	public Material ColorRed;
	public Material ColorBlue;
	
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
	
	private bool rotationBool = true;  // temp, da mettere nel menu
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
		
		// leggo nella stringa "choice" in PlayerPrefs, la quale viene impostata nel menù iniziale
		// per la selezione dell'oggetto da manipolare
		choice = PlayerPrefs.GetString ("choice");
		
		// se l'oggetto corrente è l'oggetto selezionato, viene attivato
	
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
		/*if(pinchScaleSelection.Equals("true"))
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
			scaleBool = false;*/
		
		
		
		//if(seeHandsBool == true)
		//	seeHands();
		
		//if(scaleBool == true)
		//	scale();
		
		
		
		// quando seleziono la rotazione con il pinch, controllo che sia fatto con la mano destra e sia maggiore di un dato valore
		// chiamo la funzione per la rotazione
		/*if (PinchRotationBool) {
			
			if (RightHand.PinchStrength > 0.7f) {
				
				renderer.material = ColorBlue;
				
				if (RightHand.IsValid && LeftHand.IsValid)
					
					PinchRotation ();
				
			} else {
				renderer.material = ColorRed;
			}
		}*/
		
		
		
		/*if(translatePinch) {
			if (RightHand.PinchStrength > 0.7f) {
				
				renderer.material = ColorBlue;
				
				if (RightHand.IsValid && LeftHand.IsValid)
					
					TranslatePinch();
				
			} else {
				renderer.material = ColorRed;
			}
		}*/
		
		
		
		
		//faccio la stessa procedura con la scala
		/*if (PinchScaleBool) {
			
			if (RightHand.PinchStrength > 0.7f) {
				
				renderer.material = ColorBlue;
				
				if (RightHand.IsValid && LeftHand.IsValid)
					PinchScale ();
				
			} else {
				renderer.material = ColorRed;
			}
			
		}*/
		
		
		// se seleziono la rotazione normale, chiamo la funzione rotation	
		//if(rotationBool)
			Rotation();
			
		
	}
	
	
	
	
	void Rotation() {
		
		// rotazione rispetto all'asse dello spazio				
		if (LeftHand.IsValid && RightHand.IsValid) {	
			
			float roll = LeftHand.PalmNormal.Roll * -10f;
			float pitch = RightHand.PalmNormal.Pitch * -10f;
			
			//metodo aggiornato, se è attivato il roll disattivo il pitch e viceversa (menu)				
			if ((roll >= 3f || roll <= -3f)) {
				
				if (roll >= 3)
					transform.Rotate (Vector3.up, Time.deltaTime * 60f, Space.Self);
				else 
					transform.Rotate (Vector3.up, Time.deltaTime * -60f, Space.Self);
				
				//transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth); // cambiare questa funzione, non è precisa!!!
			}
			
			if ((pitch >= 18f || pitch <= 13f)) {
				
				
				if (pitch >= 18)
					transform.Rotate (Vector3.right, Time.deltaTime * -60f, Space.Self);
				else 
					transform.Rotate (Vector3.right, Time.deltaTime * 60f, Space.Self);
				
				//transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth); // cambiare questa funzione, non è precisa!!!
			}
		}
		
	}
	
	

	
}



