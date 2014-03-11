using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RootSelector : BehaviorTask {

	private BehaviorTask[] behaviorList;

	private Func<int> index;

	public RootSelector( Func<int> rootBranchChoosingFunction , params BehaviorTask[] behaviors ){
		index = rootBranchChoosingFunction;
		behaviorList = behaviors;
	}
	
	public override BehaviorReturnResult behave ()
	{
		try{
			switch( behaviorList[index.Invoke()].behave() ){
				
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
				result = BehaviorReturnResult.Running;
				return result;
			}
		}catch ( Exception e ){
			
			Console.Error.WriteLine(e.ToString());
			
			result = BehaviorReturnResult.Failure;
			return result;
			
		}

	}
}
