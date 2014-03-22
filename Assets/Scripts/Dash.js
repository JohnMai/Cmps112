#pragma strict

public class Dash extends Task{
	private var isDashing : boolean;
	private var myHeading : GameObject;
	var point : Transform;
	private var isAttacking : boolean;
	private var hailMary : boolean;
	var rankThreeWaypoints : GameObject[];;
	var triangle : GameObject;
	var triAgent : NavMeshAgent;
	private var agent : NavMeshAgent;
	var dashAcc : float = 50;
	var parms : TweenParms = new TweenParms();
	var outParms : TweenParms = new TweenParms();
	private var channeling : boolean;
	var defSpeed : float = 3.5;
	var dashCD = 1.5;
	private var canDash : boolean;
	var defAcc : float = 8;
	
	function wait(){
		yield WaitForSeconds(0.2);
		agent.acceleration = defAcc;
		agent.speed = defSpeed;
		Debug.Log("evading");
	}
	
	
	function dashCooldown(time : float){
		canDash = false;
		yield WaitForSeconds(time);
		canDash = true;
	}
	
	function generateRandomRankThree () : int{
		return (Random.Range(0, rankThreeWaypoints.Length - 1));
	}
	function slowDown(){
		Debug.Log("slowing down");
		//0.125
		HOTween.To(agent, 0.125, outParms.OnComplete(stopDash));
	}
	function stopDash(){
		Debug.Log("stop");
		isDashing = false;
		agent.acceleration = dashAcc * 0.34;
		HOTween.To(gameObject.GetComponent.<TrailRenderer>(), 1.75, "time", 0);
		if(channeling){
			point = this.transform;
			Destroy(myHeading);
			Debug.Log("gonna heal");
			Invoke("channelHeal", 0.05);
		}
		else if (isAttacking){
			Debug.Log("gonna Explode");
			Invoke("explode", 0.05);
		}
		else {
			//Continue to point 3
			agent.speed = defSpeed * 0.5;
		}
		dashCooldown(dashCD);
		wait();
	}
	
	function createHeading(){
		if(isAttacking){
			myHeading.transform.position = (grabLastDirVector());
		}
		else if(!isAttacking || hailMary){
			myHeading.transform.position = (rankThreeWaypoints[generateRandomRankThree()].transform.position);
		}
	}
	//Might not be working correctly
	function grabLastDirVector() : Vector3{
		var triPosition : Vector3 = new Vector3(triangle.transform.position.x, transform.position.y, triangle.transform.position.z);
		var triSpeed : Vector3 = new Vector3(triAgent.velocity.x, transform.position.y, triAgent.velocity.z);
		var dirVector : Vector3 = ((triPosition + triSpeed) - transform.position);
		return dirVector;
	}
	public function Run():boolean{
		isDashing = true;
		gameObject.GetComponent.<TrailRenderer>().time = 0.25;
	
		myHeading = new GameObject("target");
	
		createHeading();

		point = myHeading.transform;
	
		agent.acceleration = dashAcc;
		HOTween.To(agent, 0.175, parms.OnComplete(slowDown));
		
		return true;
	}
}