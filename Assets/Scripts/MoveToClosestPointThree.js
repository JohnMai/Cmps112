#pragma strict
public class MoveToClosestPointThree extends Task{
	var circle : GameObject;
	var circleAgent : NavMeshAgent;
	var rankThreeWaypoints : GameObject[];
	var myHeading : GameObject;
	public function Run() : boolean{
		circleAgent = circle.GetComponent.<NavMeshAgent>();
		myHeading = rankThreeWaypoints[circle.GetComponent.<Circle>().generateRandomRankThree()];
		
		if(myHeading != null){
			circleAgent.SetDestination(myHeading.transform.position);
			return true;
		}
		else { return false;}
	}
}