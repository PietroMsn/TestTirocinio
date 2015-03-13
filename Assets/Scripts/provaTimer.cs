using UnityEngine;

using System.Threading;

public class provaTimer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	   
	   /*Debug.Log("Main thread: starting a timer"); 
       Timer t = new Timer(ComputeBoundOp, 5, 0, 2000); 
       Debug.Log("Main thread: Doing other work here...");
       Thread.Sleep(10000); // Simulating other work (10 seconds)
       t.Dispose(); // Cancel the timer now*/
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

 

        // This method's signature must match the TimerCallback delegate
    private static void ComputeBoundOp(Object state) 
    { 
       // This method is executed by a thread pool thread 
       //Debug.Log("In ComputeBoundOp: "); 
       //Thread.Sleep(1000); // Simulates other work (1 second)
       // When this method returns, the thread goes back 
       // to the pool and waits for another task 
    }


}
