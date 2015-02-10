using UnityEngine;
using Leap;
//using System;



public class ObjectControllerHuman : MonoBehaviour {
	
	Controller LeapController;
	
	
	//public GameObject[] palm;
	public GameObject Target;
	
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
	
	private bool rotationBool = false;  // temp, da mettere nel menu
   // temp, da mettere nel menu
	private bool rotateXBool = true;	// temp...
	private bool rotateYBool = true ;	// temp...
	private bool seeHandsBool = false;
	//private bool otherRotation = false;
	//private bool otherRotation2 = false;
	private bool reachedGoal = false;
	private int goalCounter;

	private float TargetX;


	private bool pinchScaleBool = false;
	private bool scaleBool = false;
	private bool sphereScaleBool = true;
	
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
		gameObject.active = true;
		
		// leggo nella stringa "choice" in PlayerPrefs, la quale viene impostata nel menù iniziale
		// per la selezione dell'oggetto da manipolare
		refreshTargetScale();
		goalCounter = 0;
		transform.localScale = new Vector3(5f, 5f, 5f);
		
		// se l'oggetto corrente è l'oggetto selezionato, viene attivato
	
		
		
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
	
		if(scaleBool)
			scale();
		if(pinchScaleBool)
			PinchScale();
		if(sphereScaleBool)
			SphereScale();


			//Debug.Log(transform.localScale + " " + Target.transform.localScale);
			if(transform.localScale.x < Target.transform.localScale.x + 0.1f
				&& transform.localScale.x > Target.transform.localScale.x - 0.1f)
				//&& (CubePosition.x  < TargetPosition.x + 0.1 && CubePosition.x > TargetPosition.x -0.5)
				reachedGoal = true;

			

			//Debug.Log(reachedGoal);

			if(reachedGoal) {
				//renderer.material = ColorRed;
				refreshTargetScale();
				transform.localScale = new Vector3(5f, 5f, 5f);
				goalCounter++;
				reachedGoal = false;	

			}

			if(goalCounter == 5){
				Debug.Log(" Passa al prossimo livello");

				// passa al livello successivo
			}




		
	}


	void refreshTargetScale(){
			
			
			TargetX = Random.Range(11f, 20f);
			
			//TargetZ = rnd.Next(1, 13);
			Target.transform.localScale = new Vector3(TargetX, TargetX, TargetX);
			
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

			float pinchRight = RightHand.PinchStrength;
			float scaleValue = pinchRight * 500f;

			if(pinchRight > 0.6)
				scaleValue -= 250f;

			if(pinchRight < 0.35)
				scaleValue += 250f;

			//Debug.Log(pinchRight);
			/***************************************   DA TESTARE !!!!!!!!!!!!!   ***************************/
			transform.localScale = new Vector3(scaleValue * 0.03f,scaleValue * 0.03f,scaleValue * 0.03f);
			//cambio la scala, utilizzo il pinch della mano destra per ingrandire o rimpicciolire
		}
	}
	
	
	void SphereScale() {
		/* sposto il metodo in PinchScale per la scala qui*/

		if (LeftHand.IsValid && RightHand.IsValid) {

						// ancora da testare
						float sphereDiameter = 2 * LeftHand.SphereRadius;
						float scaleValue = sphereDiameter * 20f;

						// guardare valori reali di sphere Diameter !!!!!!!!!!!!!!
						if(sphereDiameter > 100f)
							scaleValue += 20f;

						if(sphereDiameter > -100f)
							scaleValue -= 20f;

						transform.localScale = new Vector3(scaleValue * 0.005f,scaleValue * 0.005f,scaleValue * 0.005f);
				}
	}
	
	
	
	
}



