using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BehaviorParallelSequence : BehaviorTask {

	//holds list of behaviorTask to do
	protected  BehaviorTask[] behaviorList;

	//holds all the behaviors that are currently running
	private List<BehaviorTask> runningBehaviors;

    void Start() {
        runningBehaviors = new List<BehaviorTask>();
    }

	//constructor
	//params "behaviors" an array of all behaviors this selector will hold
	public BehaviorParallelSequence( params  BehaviorTask[] behaviors ){
		behaviorList = behaviors;
	}

    public void setBehaviorParallelSequence( params BehaviorTask[] behaviors ){
        behaviorList = behaviors;
    }

	private void runBehaviors( BehaviorTask behaviorToRun ){
		runningBehaviors.Add (behaviorToRun);
		BehaviorReturnResult returnedResult = behaviorToRun.behave ();
		runningBehaviors.Remove (behaviorToRun);

		if (returnedResult == BehaviorReturnResult.Failure) {
			result = BehaviorReturnResult.Failure;
		} else if (runningBehaviors.Count <= 0) {
			result = BehaviorReturnResult.Success;
		} else
			result = BehaviorReturnResult.Running;
	}

	//runs each behaviorTask children left to right till one fails
	public override BehaviorReturnResult behave ()	{

		//set that all behaviors are running
		result = BehaviorReturnResult.Running;

		//Runs all the behaviors simultaneously 
		for (int i = 0; i < behaviorList.Length; i++) {
			runBehaviors( behaviorList[i] );
		}
        
		//waits till one fails or all succeed 
		while (result == BehaviorReturnResult.Running) {
            Debug.Log("Wild");
		}

		return result;
	}
}