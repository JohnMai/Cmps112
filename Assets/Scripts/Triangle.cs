using UnityEngine;
using System.Collections;

public class Triangle : MonoBehaviour {
    public GameObject circle;
    public Transform point;
    private NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(point.position);
	}
}
