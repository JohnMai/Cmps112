#pragma strict
public class LeaveBomb extends Task{
	var circle : GameObject;
	var bomb : GameObject;
	
	public function Run() : boolean{
		dropBomb();
		return true;
	}
	
	function dropBomb(){
		circle.GetComponent.<Circle>().setHailMary(false);
		Instantiate(bomb, circle.transform.position, Quaternion.identity);
		circle.GetComponent.<Circle>().hailMaryCooldown(circle.GetComponent.<Circle>().hailMaryCD);
	}
}