#pragma strict
import Holoville.HOTween;

public class Dash extends Task{
	var circle : GameObject;
	var circleScript : Circle;
	var triangle : GameObject;
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
	
		myHeading.transform.position = (circleScript.grabLastDirVector());

		circleScript.agent.SetDestination(myHeading.transform.position);
	
		circleScript.agent.acceleration = circleScript.dashAcc;
		HOTween.To(circleScript.agent, 0.175, parms.OnComplete(slowDown));
		
		return true;
	}
	function wait(){
		yield WaitForSeconds(0.2);
		circleScript.agent.acceleration = circleScript.defAcc;
		circleScript.agent.speed = circleScript.defSpeed;
		//Debug.Log("evading");
	}
	
	function slowDown(){
		Debug.Log("slowing down");
		//0.125
		HOTween.To(circleScript.agent, 0.125, outParms.OnComplete(stopDash));
	}
	function stopDash(){
		//Debug.Log("stop");
		circleScript.setIsDashing(false);
		circleScript.agent.acceleration = circleScript.dashAcc * 0.34;
		HOTween.To(gameObject.GetComponent.<TrailRenderer>(), 1.75, "time", 0);

		wait();
	}
}