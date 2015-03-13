using UnityEngine;
using System;
using Leap;
using System.Timers;

public static class ExtensionMethods {

	//private static Timer timer= new Timer(3000);


    public static void doScale(this Transform trans, float scaleValue) {
		trans.localScale = new Vector3(scaleValue * -0.01f,scaleValue * -0.01f,scaleValue * -0.01f);		
	
	}

	public static float startTime(float count) {
		
		count += 1f;
		return count;
	
	}
	
	public static void askToQuit() {
		bool quit = true;
	}


	/*public static void Rotate(this GameObject obj, Hand LeftHand, Hand RightHand, float roll, float pitch){
		
		roll = (LeftHand.PalmPosition.y * RightHand.PalmPosition.y) * 0.01f;
		pitch = (LeftHand.PalmPosition.z * RightHand.PalmPosition.z) * 0.01f;
		Vector3 RotationValue = new Vector3(0f,0f,0f);
		
		if(roll > 3f){
			RotationValue.y = roll;
			obj.transform.Rotate(RotationValue, Time.deltaTime * 60f, Space.World);
		}
		
		if(pitch > 3f) { 
			
			RotationValue.z = pitch;
			obj.transform.Rotate(RotationValue, Time.deltaTime * 60f, Space.World);
		}
		
		//metodo aggiornato, se è attivato il roll disattivo il pitch e viceversa (menu)
		
		if ((roll >= 3f || roll <= -3f)){
			
			
			if (roll >= 3)
				obj.transform.Rotate(RotationValue, Time.deltaTime * 90f, Space.World);
			else 
				obj.transform.Rotate(Vector3.up, Time.deltaTime * -90f, Space.World);
			
			//transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth); // cambiare questa funzione, non è precisa!!!
		}
		
		if ((pitch >= 18f || pitch <= 13f) ){ 	
			
			if (pitch >= 18)
				obj.transform.Rotate(Vector3.right, Time.deltaTime * -90f, Space.World);
			else 
				obj.transform.Rotate(Vector3.right, Time.deltaTime * 90f, Space.World);
			
			//transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth); // cambiare questa funzione, non è precisa!!!
		}
		
		
	}*/
	
	
	public static void Rotation(this GameObject obj, Hand LeftHand, Hand RightHand) {
		
		// rotazione rispetto all'asse dello spazio				
		if (LeftHand.IsValid && RightHand.IsValid) {	
			
			float roll = LeftHand.PalmNormal.Roll * -10f;
			float pitch = RightHand.PalmNormal.Pitch * -10f;
			
			Debug.Log(pitch);
			Debug.Log(roll);
			//metodo aggiornato, se è attivato il roll disattivo il pitch e viceversa (menu)				
			if ((roll >= 3f || roll <= -3f)) {
				
				if (roll >= 3)
					obj.transform.Rotate (Vector3.up, Time.deltaTime * 90f, Space.World);
				else 
					obj.transform.Rotate (Vector3.up, Time.deltaTime * -90f, Space.World);
				
				//transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth); // cambiare questa funzione, non è precisa!!!
			}
			
			if ((pitch >= 18f || pitch <= 13f)) {
				
				
				if (pitch >= 18)
					obj.transform.Rotate (Vector3.right, Time.deltaTime * -90f, Space.World);
				else 
					obj.transform.Rotate (Vector3.right, Time.deltaTime * 90f, Space.World);
				
				//transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth); // cambiare questa funzione, non è precisa!!!
			}

			if((roll < 3f && roll > -3f) && (pitch < 18f && pitch > 13f)){
				
					Debug.Log("ehi cicico");
					/*int counter = 60;
	        		Timer timer1 = new System.Windows.Forms.Timer();
	        		timer1.Tick += new EventHandler(timer1_Tick);
	        		timer1.Interval = 1000; // 1 second
	        		timer1.Start();
	        		lblCountDown.Text = counter.ToString();*/
			}
		}
		
	}

	/*private static int counter = 60;
	private static void timer1_Tick(object sender, EventArgs e) {
	        counter--;
	        if (counter == 0)
	            timer1.Stop();
	        lblCountDown.Text = counter.ToString();
	}*/
	
	// metodo per implementare la traslazione dell'oggetto tramite pinch
	public static void TranslatePinch(this GameObject obj, Vector RightPalmPosition){
		obj.transform.position = new Vector3(RightPalmPosition.x, RightPalmPosition.y, RightPalmPosition.z); 

		// mettere la traslazione relativa!!!!!!!
	}
	
	
	/*void seeHands() {
		
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
		
	}*/
	
	
	/* funzione che ingrandisce e rimpicciolisce l'oggetto, da implementare metodo per 
				farlo smettere: dopo tot secondi che non rimpicciolisco (troppo: provious scale- actual scale <= valore basso
				chiedo all'utente se va bene la scala scelta, altrimenti torno a dove ero prima---- da settare tutti i parametri: previousScale
				actualScale, scaleBool (true se è selezionato scale), moduloVAlore, timeValue. Da implementare askToQuit!!!!*/
	
	public static void scale(this GameObject obj, Hand LeftHand, Hand RightHand, float scaleValue) {
		scaleValue = (LeftHand.PalmPosition.x * RightHand.PalmPosition.x) * 0.05f;
		
		if (LeftHand.IsValid && RightHand.IsValid) {
		
				obj.transform.localScale = new Vector3(scaleValue * -0.01f,scaleValue * -0.01f,scaleValue * -0.01f); 
				/*if (previousScale - actualScale >= moduloValore) {
					float count = 0;
					while (time == TimeValue)
						time = startTime (count);
					
					askToQuit ();  				
				} else
					obj.transform.doScale (scaleValue);*/
			
		}
		
	}
	
	
	public static void PinchScale(this GameObject obj, Hand LeftHand, Hand RightHand) {
		
		//cambio la scala solo se tutte e due le mani sono presenti
		if (LeftHand.IsValid && RightHand.IsValid ) {

			float pinchRight = RightHand.PinchStrength;
			float scaleValue = pinchRight * 600f;
			/***************************************   DA TESTARE !!!!!!!!!!!!!   ***************************/
			obj.transform.localScale = new Vector3(scaleValue * 0.05f,scaleValue * 0.05f,scaleValue * 0.05f);
			//cambio la scala, utilizzo il pinch della mano destra per ingrandire o rimpicciolire
		}
	}

	public static void NewRotation(this GameObject obj, Hand LeftHand, Hand RightHand, Vector LeftPalmPosition, Vector RightPalmPosition, Vector previousLeftPalmPosition, Vector previousRightPalmPosition) {

		/*una mano va in avanti e una va indietro, una va su e l'altra giù...da una parte o dall'altra, determina 
		 * se il movimento è orario o antiorario*/

		if (LeftHand.IsValid && RightHand.IsValid) {

						/************************** DA TESTARE: costruito senza la leap ********************************/
						// aggiungere al menu e alla selezione (considerare la selezione e deselezione con il movimento del dito)
						// prova anche con il key tap???????!!!!!!


						if (previousRightPalmPosition.z < RightPalmPosition.z && previousLeftPalmPosition.z > LeftPalmPosition.z)
									obj.transform.Rotate (Vector3.up, Time.deltaTime * -80f, Space.World);
									

						if (previousRightPalmPosition.z > RightPalmPosition.z && previousLeftPalmPosition.z < LeftPalmPosition.z)
									obj.transform.Rotate (Vector3.up, Time.deltaTime * 80f, Space.World);
									
						
						if (previousRightPalmPosition.y < RightPalmPosition.y && previousLeftPalmPosition.y > LeftPalmPosition.y)
									obj.transform.Rotate (Vector3.right, Time.deltaTime * -80f, Space.World);
									

						if (previousRightPalmPosition.y > RightPalmPosition.y && previousLeftPalmPosition.y < LeftPalmPosition.y)
									obj.transform.Rotate (Vector3.right, Time.deltaTime * 80f, Space.World);
									

				}
		
	
	
	}

	public static void NewScale(this GameObject obj, Hand LeftHand, Hand RightHand) {
		/* sposto il metodo in PinchScale per la scala qui*/

		if (LeftHand.IsValid && RightHand.IsValid) {

						// ancora da testare
						float sphereDiameter = 2 * LeftHand.SphereRadius;
						float scaleValue = sphereDiameter * 20f;
			
						obj.transform.localScale = new Vector3(scaleValue * 0.005f,scaleValue * 0.005f,scaleValue * 0.005f);
				}
	}
	
	
	// funzione per la rotazione tramite la circle gesture
	public static void PinchRotation(this GameObject obj, Hand LeftHand, Hand RightHand, Frame frame) {
		
		string clockwiseness = null;
		
		if (LeftHand.IsValid && RightHand.IsValid) {
			
			GestureList gesture = frame.Gestures ();
			
			for (int i = 0; i < gesture.Count; i++) {
				
				CircleGesture circle = new CircleGesture (gesture [i]);
				string state = circle.State.ToString ();
				
				if (state.Equals ("STATE_UPDATE")){
					
					if (circle.Pointable.Direction.AngleTo(circle.Normal) <= Math.PI/2) {
						//clockwiseness
						obj.transform.Rotate (Vector3.up, Time.deltaTime * -90f, Space.World);
					}
					
					else {
						//clockwiseness
						obj.transform.Rotate (Vector3.up, Time.deltaTime * 90f, Space.World);
					}
					
					
				}
			}
			
		}
		
	}
	
	
	
}

