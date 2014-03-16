#pragma strict

public class Selector extends Task{
	
	public function Run():boolean{
		var flag : boolean; 
		flag = false;
		for (var c in children) {
			if (c.Run())
				flag = true;
		}
		return flag;
	}
}