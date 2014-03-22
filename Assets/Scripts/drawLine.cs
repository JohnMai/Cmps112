using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class drawLine : MonoBehaviour {
	public Transform targetPos;
	private LineRenderer connector;
	public int nodeNumber;

	public int defaultZ = 1;
	// Use this for initialization
	void Start () {
		connector = this.GetComponent<LineRenderer>();
		connector.SetWidth(0.01f, 0.01f);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 modTargetPos = new Vector3(targetPos.position.x, targetPos.position.y - 0.065f, defaultZ);
		Vector3 modMyPos = new Vector3(transform.position.x, transform.position.y + 0.065f, defaultZ);
		connector.SetPosition(0, modMyPos);
		connector.SetPosition(1, modTargetPos);
	}
}
