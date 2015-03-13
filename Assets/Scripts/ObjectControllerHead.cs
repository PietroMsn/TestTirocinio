using UnityEngine;
using Leap;
using System;



public class ObjectControllerHead : MonoBehaviour {
	
	Controller LeapController;
	
	
	public GameObject[] palm;
	
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

	private HandList previousHands;
	private Hand previousLeftHand;
	private Hand previousRightHand;

	private Vector previousRightPalmPosition;
	private Vector previousLeftPalmPosition;
	
	
	private Vector RightPalmPosition;
	private Frame frame;
	private Frame previous;
	private Vector LeftPalmPosition;
	
	private Vector minus = new Vector(2f, 2f, 2f);
	
	private float pinch = 0;
	private float pinchLeft = 0;
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
	private bool translatePinchBool = false;
	private bool NewRotationBool = false;
	private bool NewScaleBool = false;
	
	private string choice = null;
	private string rotationSelection = null;
	private string scaleSelection = null;
	private string pinchRotationSelection = null;
	private string pinchScaleSelection = null;
	private string newRotationSelection = null;
	private string newScaleSelection = null;
	private string translatePinchSelection = null;
	
	
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
		if (choice.Equals ("head"))
			gameObject.active = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		
				frame = LeapController.Frame(); //The latest frame
				previous = LeapController.Frame(1); //The previous frame
				
				hands = frame.Hands;
				LeftHand = hands.Leftmost;
				RightHand = hands.Rightmost;

				previousHands = previous.Hands;
				previousLeftHand = previousHands.Leftmost;
				previousRightHand = previousHands.Rightmost;
				
				LeftPalmPosition = previousLeftHand.PalmPosition;
				RightPalmPosition = previousRightHand.PalmPosition;

				previousLeftPalmPosition = LeftHand.PalmPosition;
				previousRightPalmPosition = RightHand.PalmPosition;
				
				
				// estraggo il contenuto delle stringhe di selezione impostate dalla VRGUI per
				// vedere quali gesture sono attive
				rotationSelection = PlayerPrefs.GetString ("rotation");
				scaleSelection = PlayerPrefs.GetString ("scale");
				pinchRotationSelection = PlayerPrefs.GetString ("pinchRotation");
				pinchScaleSelection = PlayerPrefs.GetString ("pinchScale");
				newRotationSelection = PlayerPrefs.GetString ("newRotation");
				newScaleSelection = PlayerPrefs.GetString ("newScale");
				translatePinchSelection = PlayerPrefs.GetString ("pinchTranslate");
				
				
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

				if(newRotationSelection.Equals("true"))
					NewRotationBool = true;
				else 
					NewRotationBool = false;

				if(newScaleSelection.Equals("true"))
					NewScaleBool = true;
				else 
					NewScaleBool = false;

				if(translatePinchSelection.Equals("true"))
					translatePinchBool = true;
				else 
					translatePinchBool = false;
				
			

				
			
				
				if(scaleBool)
					ExtensionMethods.scale(this.gameObject , LeftHand, RightHand, scaleValue);
				
				// quando seleziono la rotazione con il pinch, controllo che sia fatto con la mano destra e sia maggiore di un dato valore
				// chiamo la funzione per la rotazione
				if (PinchRotationBool) {
					
					if (RightHand.PinchStrength > 0.7f) {
						
						renderer.material = ColorBlue;
						
						if (RightHand.IsValid && LeftHand.IsValid)
							
							ExtensionMethods.PinchRotation (this.gameObject, LeftHand, RightHand, frame);
						
					} else {
						renderer.material = ColorRed;
					}
				}
				
				
				
				if (translatePinchBool) {
					if (RightHand.PinchStrength > 0.7f) {
						
						renderer.material = ColorBlue;
						
						if (RightHand.IsValid && LeftHand.IsValid)
							
							ExtensionMethods.TranslatePinch(this.gameObject, RightPalmPosition);
						
					} else {
						renderer.material = ColorRed;
					}
				}
				
				
				
				//faccio la stessa procedura con la scala
				if (PinchScaleBool) {
					
						
						if (RightHand.IsValid && LeftHand.IsValid)
							ExtensionMethods.PinchScale (this.gameObject, LeftHand, RightHand);
					
				}
				
				
				// se seleziono la rotazione normale, chiamo la funzione rotation	
				if(rotationBool)
					ExtensionMethods.Rotation(this.gameObject, LeftHand, RightHand);

				if(NewRotationBool)
					ExtensionMethods.NewRotation (this.gameObject, LeftHand, RightHand, LeftPalmPosition, RightPalmPosition, previousLeftPalmPosition, previousRightPalmPosition);

				if(NewScaleBool) {
				if (RightHand.PinchStrength > 0.7f) {
						
						renderer.material = ColorBlue;
						
						if (RightHand.IsValid && LeftHand.IsValid)
							
							ExtensionMethods.NewScale(this.gameObject, LeftHand, RightHand);
						
					} else {
						renderer.material = ColorRed;
					}
					
				}
	}
	
	
}



