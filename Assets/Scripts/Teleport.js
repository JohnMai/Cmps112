#pragma strict
public class Teleport extends Task{
	var circle : GameObject;
	
	public function Run() : boolean{
		teleport();
		return true;
	}
	
	function teleport(){
		circle.transform.position = circle.GetComponent.<Circle>().myHeading.transform.position;
	}
}