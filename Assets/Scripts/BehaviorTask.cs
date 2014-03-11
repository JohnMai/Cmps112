using UnityEngine;
using System.Collections;

public abstract class BehaviorTask : MonoBehaviour {

	//children classes inherit result 
	protected BehaviorReturnResult result;

	//contructor for abstract class dont really think I need it
	public BehaviorTask (){}

	//Virtual funtion that is needs to beoverwritten for each unique type of BehaviorTask
	public abstract BehaviorReturnResult behave();

}
