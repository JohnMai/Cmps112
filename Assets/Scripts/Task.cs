using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public abstract class Task : MonoBehaviour {


	//Hold list of children 
	public List<Task> children;
	//Holds node reference number for gui
	private int nodeNumber;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//virtual function 
	public abstract bool? run();

	//public virtual void terminate();

	//node reference get accessor
	public int getNode(){
		return nodeNumber;
	}

	//node set accessor
	public void setNode( int newNode ){
		nodeNumber = newNode;
	}

}
