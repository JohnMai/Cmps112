using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BehaviorAction : BehaviorTask {

	//function that the behavior will execute
	private Func<BehaviorReturnResult> action;

	//empty constructor
	public BehaviorAction(){ }

	//constructor
	//param "actionTaken" function that will be taken for this specific behaviorAction
	public BehaviorAction( Func<BehaviorReturnResult> actionTaken ){
		action = actionTaken;
	}

    public void setAction(Func<BehaviorReturnResult> actionTaken) {
        action = actionTaken;
    }
	//should wait for action to invoke and wait till it returns a result that will return which Behavior result it finished;
	public override BehaviorReturnResult behave ()
	{
		try{
			switch( action.Invoke() ){
				
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
		}catch ( Exception e ){
			
			Console.Error.WriteLine(e.ToString());
			
			result = BehaviorReturnResult.Failure;
			return result;
			
		}
	}

}
