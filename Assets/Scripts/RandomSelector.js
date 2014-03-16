#pragma strict

public class RandomSelector extends Task{
	public function Run():boolean{
		var n : int;
		var child : Task;
		n = Random.Range(1,10);
		for (var c in children) {
			if(n < 5)
				child = c;
			else
				n = n - 5;
		}
		return child.Run();
	}
}