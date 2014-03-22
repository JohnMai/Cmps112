#pragma strict
public class MoveToClosestPointThree extends Task{
	var circle : GameObject;
	var rankThreeWaypoints : GameObject[];
	var myHeading : GameObject;
	public function Run() : boolean{
		myHeading = rankThreeWaypoints[circle.GetComponent.<Circle>().generateRandomRankThree()];
		
		if(myHeading != null){
			circle.GetComponent.<Circle>().agent.SetDestination(myHeading.transform.position);
			return true;
		}
		else { return false;}
	}
}