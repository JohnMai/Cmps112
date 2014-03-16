#pragma strict

public class Sequence extends Task{
	public function Run():boolean{
		for (var c in children) {
			if(!c.Run()){
				return false;
			}
		}
		return true;
	}
}