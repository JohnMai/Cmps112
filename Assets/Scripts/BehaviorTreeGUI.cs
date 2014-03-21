using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BehaviorTreeGUI : MonoBehaviour {
	//private List<int> nodeList;
	public List<UISprite> nodes;
	public UISprite[] nodeArray;

	private Color defaultColor = Color.white;
	private Color highlightColor = Color.green;
	
	// Use this for initialization
	void Start () {
		nodeArray = new UISprite[this.transform.childCount];
		nodeArray[0] = this.transform.FindChild("0_0_Selector").GetComponent<UISprite>();

		initList ();
		StartCoroutine(testHighlight());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void highlightNode(int nodeNumber){
		if(nodeNumber < nodeArray.Length){
			//nodes[nodeNumber].color = highlightColor;
			nodeArray[nodeNumber].color = highlightColor;
		}
	}

	public void unHighlightNode(int nodeNumber){
		if(nodeNumber < nodeArray.Length){
			//nodes[nodeNumber].color = defaultColor;
			nodeArray[nodeNumber].color = defaultColor;
		}
	}

	void initList(){
		foreach(Transform child in transform){
			if(child.name != "0_0_Selector"){
				//nodes.Insert(child.GetComponent<drawLine>().nodeNumber, child.GetComponent<UISprite>());
				//nodes.Add(child.GetComponent<UISprite>());
				nodeArray[child.GetComponent<drawLine>().nodeNumber] = child.GetComponent<UISprite>();
			}
		}
	}

	IEnumerator testHighlight(){
		int nodeCount = 0;
		while(nodeCount < nodeArray.Length){
			highlightNode (nodeCount);
			nodeCount++;
			yield return new WaitForSeconds(0.4f);
		}
	}
}
