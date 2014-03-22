#pragma strict
public class CircleDistanceCondition extends Task{
	var circle : GameObject;
	var triangle : GameObject;
	var minDist : float;

	public function Run() : boolean{
		var distance = Vector3.Distance(circle.transform.position, triangle.transform.position);
		if(distance <= minDist){
			return true;
		}
		else { return false;}
	}
}