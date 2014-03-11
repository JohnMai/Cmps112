using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Parallel : Task {

	//inherits List<Task> children
	//inherits int nodeNumber

	//holds all the children currently running
	public List<Task> runningChildren;

	//holds the final results for our run method
	public bool? results;
	

	// Use this for initialization
	void Start () {
		runningChildren = new List<Task>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override bool? run (){
		SendMessage ("AdrianFunction", getNode() );

		results = null;

		//Start all our children running for child in children
		//--thread = new Thread()
		//--thread.start( runChild, child )

		//c# Start all our children running for child in children;
		foreach (Task kid in children) {
            runChild(kid);
        }

		//Wait until we have a result to return
        while (results == null) {
            //--sleep
        }
            return results;

		//--sleep()
		//return result


	}

	//run child coroutine??? relates within the run function
	void runChild( Task child ){
		runningChildren.Add (child);
		bool? returned = child.run ();
		runningChildren.Remove (child);

		if (returned == false) {
						//--terminate()
					results = false;
		} else if (runningChildren.Count <= 0) {
			results = true;
		}
	}

	//need to build out terminate virtual function
	//--public override void terminate(){
	//--	foreach( Task kid in runningChildren ){
	//--		child.terminate();
	//--    }
}
