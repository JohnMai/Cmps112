#pragma strict
import System.Collections.Generic;

public interface ITask{
	//var TheDamage : ArrayList = new ArrayList();
	//var children = new ArrayList();  
	function Run ():boolean;
}

public class Task extends MonoBehaviour implements ITask{
	var children : List.<Task> = new List.<Task>();
	
	public function Run(): boolean{
	}
}
