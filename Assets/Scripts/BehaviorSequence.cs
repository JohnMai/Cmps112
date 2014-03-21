using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BehaviorSequence : BehaviorTask {
	
	//holds list of behaviorTask to do
	private  BehaviorTask[] behaviorList;
	
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

		bool anyBehaviorRunning = false;

		for (int i = 0; i < behaviorList.Length; i ++) {
			try {
				switch (behaviorList [i].behave ()) {
					case BehaviorReturnResult.Failure:
						result = BehaviorReturnResult.Failure;
						return result;
					case BehaviorReturnResult.Success:
						continue;
					case BehaviorReturnResult.Running:
						result = BehaviorReturnResult.Running;
						anyBehaviorRunning = true;
						return result;
					default:
						result = BehaviorReturnResult.Success;
						return result;
				}
			} catch (Exception e) {
				Console.Error.WriteLine (e.ToString ());
				result = BehaviorReturnResult.Running;
				return result;
			}
		}
		if (!anyBehaviorRunning)
			result = BehaviorReturnResult.Success;
		else
			result = BehaviorReturnResult.Running;
		
		return result;
	}
}
