#pragma strict
public class ChannelHeal extends Task{
	var circle : GameObject;
	var notHit = true;;
	
	public function Run() : boolean{
		circle.GetComponent.<Circle>().agent.SetDestination(circle.transform.position);
		return channelHeal();
	}
	
	function channelHeal() : boolean{
		if(notHit && circle.GetComponent.<Circle>().getCurrentHealth() < circle.GetComponent.<Circle>().maxHealth){
			circle.GetComponent.<Circle>().healDamage();
			
			Invoke("channelHeal", circle.GetComponent.<Circle>().healFrequency);
		}
		else {
			circle.GetComponent.<Circle>().setChanneling(false);
			return true;
		}
	}
	
	public function gotHit(){
		notHit = false;
	}
}