using UnityEngine;
using Leap;
using System;



public class ObjectControllerEarth : MonoBehaviour {
	
	Controller LeapController;
	
	
	//public GameObject[] palm;
	
	public Material ColorRed;
	public Material ColorBlue;
	
	public float roll;
	public float pitch;
	
	private HandList hands;
	private Hand LeftHand;
	private Hand RightHand;

	private HandList previousHands;
	private Hand previousLeftHand;
	private Hand previousRightHand;

	private Vector previousRightPalmPosition;
	private Vector previousLeftPalmPosition;
	
	private Vector RightPalmPosition;
	private Vector LeftPalmPosition;
	private Frame frame;
	private Frame previous;
	
	//private Vector minus = new Vector(2f, 2f, 2f);
		
	private bool rotationBool = true;  // temp, da mettere nel menu
	private bool seeHandsBool = false;
	
	
	private bool FingerRotation = false;
	private bool HandsRotation = false;
	private bool MyRotation = false;

	private bool YouWin = false;
	
	private Vector3 Goal;
	private Vector3 ActualRotation;
	private float constant = 0.05f;
	private string Scelta;
	
	
	// Use this for initialization
	void Start () {
		
		LeapController = new Controller();
		
		LeapController.EnableGesture(Gesture.GestureType.TYPECIRCLE); // abilito il riconoscimento della gestre TYPECIRCLE
		LeapController.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
		// la visibilità dell'oggetto corrente viene messa di default a false
		gameObject.active = false;
		
		
		
		gameObject.active = true;
		Goal = new Vector3(-0.1f,-0.5f,0.7f);

		Scelta = PlayerPrefs.GetString("RotationType");

			if(Scelta.Equals("one"))
				FingerRotation = true;

			else if (Scelta.Equals("two"))
				HandsRotation = true;

			else if (Scelta.Equals("three"))
				MyRotation = true;
		

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

		hands = frame.Hands;
		LeftHand = hands.Leftmost;
		RightHand = hands.Rightmost;
		
		previousHands = previous.Hands;
		previousLeftHand = previousHands.Leftmost;
		previousRightHand = previousHands.Rightmost;
				
		previousLeftPalmPosition = previousLeftHand.PalmPosition;
		previousRightPalmPosition = previousRightHand.PalmPosition;

		LeftPalmPosition = LeftHand.PalmPosition;
		RightPalmPosition = RightHand.PalmPosition;
		
	
			if(LeftHand.IsValid && RightHand.IsValid)
				if(MyRotation)
					Rotation();

			if(LeftHand.IsValid && RightHand.IsValid)
				if(HandsRotation)
					NewRotation();

			if(LeftHand.IsValid || RightHand.IsValid)
				if(FingerRotation)
					RotationFingerPosition();

			ActualRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);

			/*if(ActualRotation.x > Goal.x -0.1f && ActualRotation.x < Goal.x +0.1f
				&& ActualRotation.y > Goal.y -0.1f && ActualRotation.y < Goal.y +1f
				&& ActualRotation.z > Goal.z -0.1f && ActualRotation.z < Goal.z +0.7f)*/
				

			Debug.Log(ActualRotation + " " + Goal);
			if(YouWin){
					Debug.Log("Passa al prossimo livello");
					Application.LoadLevel("MainMenu");
					PlayerPrefs.SetString("Goal", "true");

				}

			
		
	}
	

	void RotationFingerPosition(){

				float[] fingerPositionX = new float[5];
				float[] fingerPositionY = new float[5];
				float[] fingerPositionZ = new float[5];

				for (int f = 0; f < RightHand.Fingers.Count; f++) {
					fingerPositionX[f] = RightHand.Fingers[f].TipPosition.x * constant;
					fingerPositionY[f] = RightHand.Fingers[f].TipPosition.y * constant;
					fingerPositionZ[f] = RightHand.Fingers[f].TipPosition.z * constant;
				}	

				if(fingerPositionX[0] > transform.position.x +5f)
					transform.Rotate (Vector3.up, Time.deltaTime * +50f, Space.World);

				if(fingerPositionX[0] <= transform.position.x - 5f)
					transform.Rotate (Vector3.up, Time.deltaTime * -50f, Space.World);

				if(fingerPositionY[0] > transform.position.y + 8f)
					transform.Rotate (Vector3.right, Time.deltaTime * +50f, Space.World);

				if(fingerPositionY[0] <= transform.position.y + 3f)
					transform.Rotate (Vector3.right, Time.deltaTime * -50f, Space.World);

	}


	void NewRotation() {

		/*una mano va in avanti e una va indietro, una va su e l'altra giù...da una parte o dall'altra, determina 
		 * se il movimento è orario o antiorario*/

		if (LeftHand.IsValid && RightHand.IsValid) {

						
							if (RightPalmPosition.z < LeftPalmPosition.z +20f)
										transform.Rotate (Vector3.up, Time.deltaTime * -50f, Space.World);
										

							if (RightPalmPosition.z > LeftPalmPosition.z -20f)
										transform.Rotate (Vector3.up, Time.deltaTime * 50f, Space.World);
										
						/*}

						if(RightPalmPosition.y - LeftPalmPosition.y > 0) 
										obj.transform.Rotate (Vector3.right, Time.deltaTime * -80f, Space.World);

						else if(LeftPalmPosition.y - RightPalmPosition.y > 0)
										obj.transform.Rotate (Vector3.right, Time.deltaTime * 80f, Space.World);

						else{*/
							if (RightPalmPosition.y < LeftPalmPosition.y +20f )
										transform.Rotate (Vector3.right, Time.deltaTime * -50f, Space.World);
										

							if (RightPalmPosition.y > LeftPalmPosition.y -20f)
										transform.Rotate (Vector3.right, Time.deltaTime * 50f, Space.World);
						//}			

				}
		
	
	
	}
	
	
	
	
	void Rotation() {
		
		// rotazione rispetto all'asse dello spazio				
		if (LeftHand.IsValid && RightHand.IsValid) {	
			
			float roll = LeftHand.PalmNormal.Roll * -10f;
			float pitch = RightHand.PalmNormal.Pitch * -10f;
			
			//metodo aggiornato, se è attivato il roll disattivo il pitch e viceversa (menu)				
			if ((roll >= 4f || roll <= -4f)) {
				
				if (roll >= 4)
					transform.Rotate (Vector3.up, Time.deltaTime * 50f, Space.World);
				else 
					transform.Rotate (Vector3.up, Time.deltaTime * -50f, Space.World);
				
				//transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth); // cambiare questa funzione, non è precisa!!!
			}
			
			if ((pitch >= 18f || pitch <= 13f)) {
				
				
				if (pitch >= 18)
					transform.Rotate (Vector3.right, Time.deltaTime * -50f, Space.World);
				else 
					transform.Rotate (Vector3.right, Time.deltaTime * 50f, Space.World);
				
				//transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth); // cambiare questa funzione, non è precisa!!!
			}
		}
		
	}
	
	

	
}



