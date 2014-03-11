using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BehaviorSelector : BehaviorTask {

	//holds list of behaviorTask to do
	private BehaviorTask[] behaviorList;

	//constructor
	//params "behaviors" an array of all behaviors this selector will hold
	public BehaviorSelector( params  BehaviorTask[] behaviors ){
		behaviorList = behaviors;
	}

	//runs each selector children left to right till all fails
	public override BehaviorReturnResult behave ()	{
		for (int i = 0; i < behaviorList.Length; i ++) {
			try {
				switch (behaviorList [i].behave ()) {
					case BehaviorReturnResult.Failure:
						continue;
					case BehaviorReturnResult.Success:
						result = BehaviorReturnResult.Success;
						return result;
					case BehaviorReturnResult.Running:
						result = BehaviorReturnResult.Running;
						return result;
					default:
						continue;
				}
			} catch (Exception e) {
				Console.Error.WriteLine (e.ToString ());
				continue;
				
			}
		}
			result = BehaviorReturnResult.Failure;
			return result;
	}
}