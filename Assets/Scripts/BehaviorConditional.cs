using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BehaviorConditional : BehaviorTask {

	private Func<Boolean> conditionFunction;

	public BehaviorConditional( Func<Boolean> testFunction ){
		conditionFunction = testFunction;
	}

	public override BehaviorReturnResult behave ()
	{
		try{
			switch( conditionFunction.Invoke() ){
				
			case false:
				result = BehaviorReturnResult.Failure;
				return result;
			case true:
				result = BehaviorReturnResult.Success;
				return result;
			default:
				result = BehaviorReturnResult.Failure;
				return result;
			}
		}catch ( Exception e ){
			
			Console.Error.WriteLine(e.ToString());
			
			result = BehaviorReturnResult.Failure;
			return result;
			
		}
	}
}
