using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Selector : Task {

	//inherits List<Task> children
	//inherits int nodeNumber

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public override bool? run (){
		SendMessage ("AdrianFunction", getNode() );
		foreach (Task kid in children) {
			//if( kid.run() ){
				return true;
			//}
		}

		return false;
	}
}
