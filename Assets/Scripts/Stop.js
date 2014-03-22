#pragma strict
public class Stop extends Task{
	var circle : GameObject;
	
	public function Run() : boolean{
		circle.GetComponent.<Circle>().agent.SetDestination(circle.transform.position);
		return true;
	}
}