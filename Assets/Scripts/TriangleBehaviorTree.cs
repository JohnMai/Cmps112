using UnityEngine;
using System.Collections;

public class TriangleBehaviorTree : MonoBehaviour {

	//hold tree 
	private BehaviorTree triangleBehavior;
	//hold root
	private RootSelector root;

	//Setup Triangles BehaviorTree
	void Start () {

		//Hail Mary Behavior
		BehaviorAction stop = new BehaviorAction (/* pass function that will be executed this unique behavior */);
		BehaviorAction animTurn = new BehaviorAction (/* pass function that will be executed this unique behavior */);
		BehaviorSequence depthFourNodeOneHM = new BehaviorSequence (stop, animTurn);

		BehaviorAction triangleAppear = new BehaviorAction (/* pass function that will be executed this unique behavior */);
		BehaviorAction turnAround = new BehaviorAction (/* pass function that will be executed this unique behavior */);
		BehaviorSequence depthFourNodeTwoHM = new BehaviorSequence (triangleAppear, turnAround);

		BehaviorParallelSequence depthThreeHM = new BehaviorParallelSequence (depthFourNodeOneHM, depthFourNodeTwoHM);
		BehaviorAction fires = new BehaviorAction (/* pass function that will be executed this unique behavior */);

		BehaviorSequence depthTwoHM = new BehaviorSequence (depthThreeHM, fires);
		BehaviorConditional canHailMary = new BehaviorConditional (/* pass function that will be executed this unique behavior */);
		BehaviorConditional healthBelow = new BehaviorConditional (/* pass function that will be executed this unique behavior */);
		BehaviorAction moveToPoint = new BehaviorAction (/* pass function that will be executed this unique behavior */);

		BehaviorSequence HailMary = new BehaviorSequence (canHailMary, healthBelow, moveToPoint, depthTwoHM );

		//Aggressive Behavior
		BehaviorAction moveToCircleUntilInner = new BehaviorAction(/* pass function that will be executed this unique behavior */);
		//Reuse fires behavior above
		BehaviorAction moreThanXHealthThanCircle = new BehaviorAction (/* pass function that will be executed this unique behavior */);
		BehaviorParallelSequence depthThreeAGG = new BehaviorParallelSequence( moveToCircleUntilInner , fires );
	
		BehaviorSequence Aggressive = new BehaviorSequence ( moreThanXHealthThanCircle , depthThreeAGG);
		
		//Defensive Behavior
		BehaviorConditional notInTransit = new BehaviorConditional (/* pass function that will be executed this unique behavior */);
		BehaviorAction moveToRandomPointThree = new BehaviorAction (/* pass function that will be executed this unique behavior */);
		BehaviorSequence depthThreeNodeOneDef = new BehaviorSequence ( notInTransit , moveToRandomPointThree );

		BehaviorConditional onCD = new BehaviorConditional (/* pass function that will be executed this unique behavior */);
		BehaviorAction spawnTriangles = new BehaviorAction (/* pass function that will be executed this unique behavior */);
		BehaviorSequence depthThreeNodeTwoDef = new BehaviorSequence (onCD, spawnTriangles);

		BehaviorRandomSelector Defensive = new BehaviorRandomSelector (depthThreeNodeOneDef, depthThreeNodeTwoDef);

		//Arrange Composites trees
		BehaviorSelector CombineAggDefComposites = new BehaviorSelector (Aggressive, Defensive);

		//Setup Root Node which holds all behaviors
		root = new RootSelector (/* Need A Function still unsure what look in root and look at private index function  */ " " , HailMary, CombineAggDefComposites);

		//Triangle Behavior
		triangleBehavior = new BehaviorTree (root);
	}
	
	//Runs TriangleBehavior
	void Update () {
		triangleBehavior.behave ();
	}
}
