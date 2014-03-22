#pragma strict
public class PickRNDPointThree extends Task{
	var circle : GameObject;
	var rankThreeWaypoints : GameObject[];
	var myHeading : GameObject;
	public function Run() : boolean{
		myHeading = rankThreeWaypoints[circle.GetComponent.<Circle>().generateRandomRankThree()];
		circle.GetComponent.<Circle>().myHeading = myHeading;
		
		if(myHeading != null){
			return true;
		}
		else { return false;}
	}
}