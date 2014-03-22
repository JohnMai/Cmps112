using UnityEngine;
using System.Collections;

public class TriangleBehaviorTree : MonoBehaviour {
    public GameObject action, rootSelector, behaviorTree, behaviorSequence, behaviorSelector, behaviorParallelSequence , condition;

    //this is the gameobject that our other stuff will be childed to
    private GameObject rootTree;
    private TriangleImproved triangle;
	//hold tree 
	private BehaviorTree triangleBehavior;
	//hold root
	private RootSelector root;

    private enum TaskType { BehaviorTree, Action, Condition, RootSelector, BehaviorSequnce, BehaviorParallelSequence };
	//Setup Triangles BehaviorTree
	void Start () {

        triangle = GetComponent<TriangleImproved>();

        //Set root behavior Tree
        rootTree = newTask("*Behavior Tree", TaskType.BehaviorTree);
        rootTree.transform.parent = transform;
        triangleBehavior = rootTree.GetComponent<BehaviorTree>();

		//////////////////////////////////Hail Mary Behavior/////////////////////////////////////////////////////////

        //stop action
        BehaviorAction stop = newTask("Stop Action", TaskType.Action).GetComponent<BehaviorAction>();
        stop.setAction(triangle.Stop);

        //small tri's spawn action
        BehaviorAction triangleAppear = newTask("Triangle Appear Action", TaskType.Action).GetComponent<BehaviorAction>();
        triangleAppear.setAction(triangle.SpawnTriangles);

        //turn around action
        BehaviorAction turnAround = newTask("Tris Rotating Action", TaskType.Action).GetComponent<BehaviorAction>();
        turnAround.setAction(triangle.sendTrisRotating);

        //charge action
        BehaviorAction charge = newTask("Charge Action", TaskType.Action).GetComponent<BehaviorAction>();
        charge.setAction(triangle.ChargeUp);

        //fire beam action
        BehaviorAction beam = newTask("Beam Action", TaskType.Action).GetComponent<BehaviorAction>();
        beam.setAction(triangle.FireBeam);

        //stop-triAppear-Spin-charge sequence
        BehaviorSequence stopAppearChargeSeq = newTask("Hail Mary Charge Sequence", TaskType.BehaviorSequnce).GetComponent<BehaviorSequence>();
        stopAppearChargeSeq.setBehaviorSequence(stop, triangleAppear, turnAround, charge);

        //stop-triAppear-Spin-charge--beam sequence
        BehaviorSequence stopAppearChargeBeamSeq = newTask("Hail Mary Charge Beam Sequence", TaskType.BehaviorSequnce).GetComponent<BehaviorSequence>();
        stopAppearChargeBeamSeq.setBehaviorSequence(stopAppearChargeSeq, beam);

        //can Hm condiiton
        BehaviorConditional canHailMary = newTask("Can Hail Mary Condition", TaskType.Condition).GetComponent<BehaviorConditional>();
        canHailMary.setBehaviorConditional(triangle.CanHailMary);

		BehaviorConditional healthLessThanPercentage = newTask ("Health Less Than Percentage", TaskType.Condition).GetComponent<BehaviorConditional> ();
		healthLessThanPercentage.setBehaviorConditional (triangle.HealthLessThanPercentage);

		BehaviorAction moveToPointOne = newTask ("Move To Point one", TaskType.Action).GetComponent<BehaviorAction> ();
		moveToPointOne.setAction (triangle.MoveToDestinationOne);

        //stop-triAppear-Spin-charge--beam sequence
        BehaviorSequence mainHailMarySequence = newTask("Condition And Action Sequence", TaskType.BehaviorSequnce).GetComponent<BehaviorSequence>();
        mainHailMarySequence.setBehaviorSequence(canHailMary, healthLessThanPercentage ,  moveToPointOne ,stopAppearChargeBeamSeq);
        ////stop-charge sequnce
        //BehaviorSequence depthFourNodeOneHM = newTask("Stop Turn Sequence", TaskType.BehaviorSequnce).GetComponent<BehaviorSequence>();
        //depthFourNodeOneHM.setBehaviorSequence(stop, charge);

        ////small tri's spawn-turn around sequence
        //BehaviorSequence depthFourNodeTwoHM = newTask("SpawnTri TrisRotat Sequence", TaskType.BehaviorSequnce).GetComponent<BehaviorSequence>();
        //depthFourNodeTwoHM.setBehaviorSequence(triangleAppear, turnAround);

        ////stop-charge--small tri's spawn-turn around parallel sequence
        //BehaviorParallelSequence depthThreeHM = newTask("Charge Spawn Parrellel Sequence", TaskType.BehaviorParallelSequence).GetComponent<BehaviorParallelSequence>();
        //depthThreeHM.setBehaviorParallelSequence(depthFourNodeOneHM, depthFourNodeTwoHM);

        ////parallel sequnce-fire beam sequence
        //BehaviorSequence depthTwoHM = newTask("Parallel Beam Sequence", TaskType.BehaviorSequnce).GetComponent<BehaviorSequence>();
        //depthTwoHM.setBehaviorSequence(depthThreeHM, beam);

        //BehaviorConditional canHailMary = new BehaviorConditional (/* pass function that will be executed this unique behavior */);
        //BehaviorConditional healthBelow = new BehaviorConditional (/* pass function that will be executed this unique behavior */);
        //BehaviorAction moveToPoint = new BehaviorAction (/* pass function that will be executed this unique behavior */);

        //BehaviorSequence HailMary = new BehaviorSequence (canHailMary, healthBelow, moveToPoint, depthTwoHM );

        ////Aggressive Behavior
        //BehaviorAction moveToCircleUntilInner = new BehaviorAction(/* pass function that will be executed this unique behavior */);
        ////Reuse fires behavior above
        //BehaviorAction moreThanXHealthThanCircle = new BehaviorAction (/* pass function that will be executed this unique behavior */);
        //BehaviorParallelSequence depthThreeAGG = new BehaviorParallelSequence( moveToCircleUntilInner , fires );
	
        //BehaviorSequence Aggressive = new BehaviorSequence ( moreThanXHealthThanCircle , depthThreeAGG);
		
        ////Defensive Behavior
        //BehaviorConditional notInTransit = new BehaviorConditional (/* pass function that will be executed this unique behavior */);
        //BehaviorAction moveToRandomPointThree = new BehaviorAction (/* pass function that will be executed this unique behavior */);
        //BehaviorSequence depthThreeNodeOneDef = new BehaviorSequence ( notInTransit , moveToRandomPointThree );

        //BehaviorConditional onCD = new BehaviorConditional (/* pass function that will be executed this unique behavior */);
        //BehaviorAction spawnTriangles = new BehaviorAction (/* pass function that will be executed this unique behavior */);
        //BehaviorSequence depthThreeNodeTwoDef = new BehaviorSequence (onCD, spawnTriangles);

        //BehaviorRandomSelector Defensive = new BehaviorRandomSelector (depthThreeNodeOneDef, depthThreeNodeTwoDef);

        ////Arrange Composites trees
        //BehaviorSelector CombineAggDefComposites = new BehaviorSelector (Aggressive, Defensive);

        ////Setup Root Node which holds all behaviors
        root = newTask("Root Selector", TaskType.RootSelector).GetComponent<RootSelector>();
        root.setRootSelector(switchbehavior, mainHailMarySequence , );

        triangleBehavior.setBehaviorTree(root);

	}

    int switchbehavior() {
        return 0;
    }

	//Runs TriangleBehavior
	void Update () {
		//triangleBehavior.behave ();
        //StartCoroutine(tick(0.1f));
	}

    void FixedUpdate() {
        triangleBehavior.behave();
    }
    private IEnumerator tick(float timeToWait) {
        triangleBehavior.behave();
        yield return new WaitForSeconds(timeToWait);
    }
    GameObject newTask(string name, TaskType task ) {
        GameObject tempTask;

        switch (task) {
            case TaskType.Action:
                tempTask = (GameObject)Instantiate(action, Vector3.zero, Quaternion.identity);
                break;
            case TaskType.BehaviorTree:
                tempTask = (GameObject)Instantiate(behaviorTree, Vector3.zero, Quaternion.identity);
                break;
            case TaskType.Condition:
                tempTask = (GameObject)Instantiate(condition, Vector3.zero, Quaternion.identity);
                break;
            case TaskType.RootSelector:
                tempTask = (GameObject)Instantiate(rootSelector, Vector3.zero, Quaternion.identity);
                break;
            case TaskType.BehaviorSequnce:
                tempTask = (GameObject)Instantiate(behaviorSequence, Vector3.zero, Quaternion.identity);
                break;
            case TaskType.BehaviorParallelSequence:
                tempTask = (GameObject)Instantiate(behaviorParallelSequence, Vector3.zero, Quaternion.identity);
                break;
            default:
                tempTask = (GameObject)Instantiate(this, Vector3.zero, Quaternion.identity);
                break;
        }
        if(task != TaskType.BehaviorTree)
            tempTask.transform.parent = rootTree.transform;

        tempTask.name = name;
        return tempTask;
    }
}
