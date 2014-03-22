#pragma strict
public class Explode extends Task{
	var circle : GameObject;
	var explosion : GameObject;
	public function Run() : boolean{
		explode();
		//yield WaitForSeconds(1);//maybe not here
		return true;
	}
	
	function explode(){
		circle.GetComponent.<Circle>().point = circle.transform;
		circle.GetComponent.<Circle>().DestroyHeading();
		var explosionPosition : Vector3 = circle.transform.position;
		explosionPosition.y = 0.65;
		var currentExplosion = Instantiate(explosion, explosionPosition, Quaternion.identity);
		circle.GetComponent.<Circle>().setIsAttacking(false);
		circle.GetComponent.<Circle>().attackCooldown(circle.GetComponent.<Circle>().attackCD);
	}
}