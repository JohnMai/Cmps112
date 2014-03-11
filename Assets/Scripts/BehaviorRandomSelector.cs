using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BehaviorRandomSelector : BehaviorTask {

	//holds list of behaviorTask to do
	private BehaviorTask[] behaviorList;

	//creates a seed that will be used to get a random behavior
	private System.Random randomBehavior = new System.Random (DateTime.Now.Second);

	//constructor
	//params "behaviors" an array of all behaviors this selector will hold
	public BehaviorRandomSelector( params  BehaviorTask[] behaviors ){
		behaviorList = behaviors;
	}

	//runs each selector children left to right till all fails
	public override BehaviorReturnResult behave ()	{
		randomBehavior = new System.Random (DateTime.Now.Second);
			try {
				switch (behaviorList [ randomBehavior.Next( 0, behaviorList.Length -1  )].behave ()) {
					case BehaviorReturnResult.Failure:
						result = BehaviorReturnResult.Failure;
						return result;
					case BehaviorReturnResult.Success:
						result = BehaviorReturnResult.Success;
						return result;
					case BehaviorReturnResult.Running:
						result = BehaviorReturnResult.Running;
						return result;
					default:
						result = BehaviorReturnResult.Failure;
						return result;
				}
			} catch (Exception e) {
				Console.Error.WriteLine (e.ToString ());
				result = BehaviorReturnResult.Failure;
				return result;
				
			}
	}
}