using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BehaviorSequence : BehaviorTask {
	
	//holds list of behaviorTask to do
	private BehaviorTask[] behaviorList;
    private int currentlyRunning = -1;
	//constructor
	//params "behaviors" an array of all behaviors this sequence will hold
	public BehaviorSequence( params  BehaviorTask[] behaviors ){
		behaviorList = behaviors;
	}

    public void setBehaviorSequence(params BehaviorTask[] behaviors) {
        behaviorList = behaviors;
    }
	//runs each selector children left to right till one fails
	public override BehaviorReturnResult behave ()	{

		for (int i = (currentlyRunning == -1)? 0 : currentlyRunning; i < behaviorList.Length; i ++) {
			try {
				switch (behaviorList [i].behave ()) {
					case BehaviorReturnResult.Failure:
						result = BehaviorReturnResult.Failure;
						return result;
					case BehaviorReturnResult.Success:
						continue;
					case BehaviorReturnResult.Running:
                        currentlyRunning = i;
						result = BehaviorReturnResult.Running;
						return result;
					default:
                        currentlyRunning = -1;
						result = BehaviorReturnResult.Success;
						return result;
				}
			} catch (Exception e) {
				Console.Error.WriteLine (e.ToString ());
				result = BehaviorReturnResult.Running;
				return result;
			}
		}
		currentlyRunning = -1;
		return result = BehaviorReturnResult.Success;
	}
}
