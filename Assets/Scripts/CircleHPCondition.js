#pragma strict
public class CircleHPCondition extends Task{
	var circle : GameObject;
	var triangle : GameObject;
	public function Run() : boolean{
		//if(!(triangle.GetComponent.<TriangleImproved>().CurrentHealth() <= circle.GetComponent.<Circle>().getCurrenHealth())){
			return true;
		//}
		//else { return false;}
	}
}