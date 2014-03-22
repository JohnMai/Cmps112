#pragma strict
import Holoville.HOTween;

public class Dash extends Task{
	var circle : GameObject;
	var circleScript : Circle;
	var triangle : GameObject;
	var circleAgent : NavMeshAgent;
	var triAgent : NavMeshAgent;
	var myHeading : GameObject;
	var parms : TweenParms = new TweenParms();
	var outParms : TweenParms = new TweenParms();
	
	public function Run():boolean{
		circleScript = circle.GetComponent.<Circle>();
		parms.Prop("speed", circleScript.dashSpeed);
		parms.Ease(EaseType.EaseInCubic);
	
		outParms.Prop("speed", circleScript.defSpeed);
		outParms.Ease(EaseType.EaseInCubic);
		
		circleScript.setIsDashing(true);
		circle.GetComponent.<TrailRenderer>().time = 0.25;
	
		myHeading = new GameObject("target");
	
//		Debug.Log("dash");
		myHeading.transform.position = (grabLastDirVector());
//		Debug.Log("doubledash");
		
		circleAgent = circle.GetComponent.<NavMeshAgent>();
		circleAgent.SetDestination(myHeading.transform.position);
	
		circleAgent.acceleration = circleScript.dashAcc;
		HOTween.To(circleScript.agent, 0.175, parms.OnComplete(slowDown));
		
		return true;
	}
	
	function grabLastDirVector() : Vector3{
		triAgent = triangle.GetComponent.<NavMeshAgent>();
		var triPosition : Vector3 = new Vector3(triangle.transform.position.x, transform.position.y, triangle.transform.position.z);
		var triSpeed : Vector3 = new Vector3(triAgent.velocity.x, transform.position.y, triAgent.velocity.z);
		var dirVector : Vector3 = ((triPosition + triSpeed) - transform.position);
		return dirVector;
	}
	
	function wait(){
		yield WaitForSeconds(0.2);
		circleAgent.acceleration = circleScript.defAcc;
		circleAgent.speed = circleScript.defSpeed;
		//Debug.Log("evading");
	}
	
	function slowDown(){
		Debug.Log("slowing down");
		//0.125
		HOTween.To(circleAgent, 0.125, outParms.OnComplete(stopDash));
	}
	function stopDash(){
		//Debug.Log("stop");
		circleScript.setIsDashing(false);
		circleAgent.acceleration = circleScript.dashAcc * 0.34;
		HOTween.To(gameObject.GetComponent.<TrailRenderer>(), 1.75, "time", 0);

		wait();
	}
}