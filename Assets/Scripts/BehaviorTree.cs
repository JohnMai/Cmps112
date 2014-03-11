using System;
using UnityEngine;
using System.Collections;


//used to return condition of behavior
public enum BehaviorReturnResult { Failure, Success, Running }

public delegate BehaviorReturnResult BehaviorReturn();


public class BehaviorTree : MonoBehaviour {

	//holds root selector for BehaviorTree
	private RootSelector root;

	//holds result for the BehaviorTree
	private BehaviorReturnResult result;

	//result has two function for set and get made for the private result var above
	public BehaviorReturnResult ReturnResult{
		get{ return result; }
		set{ result = value; }
	}

	//constructor for behavior tree taking in a root as param
	public BehaviorTree( RootSelector createdRoot ){
		root = createdRoot;
	}

	//starts running root
	public BehaviorReturnResult behave(){
		try{
			switch( root.behave() ){

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
