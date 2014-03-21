using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {
	public GameObject explosion;
	public float fuse;
	// Use this for initialization
	void Start () {
		Invoke ("explode", fuse);
	}
	
	void explode(){
		Instantiate (explosion, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
