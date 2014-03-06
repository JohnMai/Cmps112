using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using ProBuilder2.Common;

public class Triangle : MonoBehaviour {
    public float maxHealth = 100, chargeTimeSec = 4, miniTriExpandSpeed = 1, speedTriRotAround = 1;
    public GameObject circle, miniTrianglePrefab;
	public Transform point, triOnePos, triTwoPos, triThreePos;
	public bool shitsOn = false;

    private GameObject triOne, triTwo, triThree;
	private NavMeshAgent agent;
	private float currentHealth;
    private bool canHailMary = true, canRotate = false;
	
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (shitsOn) 
			StartCoroutine(ChargeUp());
        MoveToDestination(point.position);
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            spawnTriangles();
        }

        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            canRotate = true;
        }
        if (canRotate) {
            sendTrisRotating();
        }

	}
	
	public void MoveToDestination(Vector3 position) {
		agent.SetDestination(point.position);
	}
	
	public IEnumerator ChargeUp() {
		Vector3 newRotation = gameObject.transform.eulerAngles;
		newRotation.y += 360;
		yield return (HOTween.To(gameObject.transform, chargeTimeSec, "eulerAngles", newRotation).WaitForCompletion());
        shitsOn = false;
	}

    public void spawnTriangles() {
        //set them to spawn in center of tri 
        triOne = ProBuilder.Instantiate(miniTrianglePrefab, new Vector3(0, 0, 1), Quaternion.identity);
        triTwo = ProBuilder.Instantiate(miniTrianglePrefab, new Vector3(1, 0, 0), Quaternion.identity);
        triThree = ProBuilder.Instantiate(miniTrianglePrefab, new Vector3(1, 0, 0), Quaternion.identity);

        triOne.transform.parent = transform;
        triTwo.transform.parent = transform;
        triThree.transform.parent = transform;

        //here to fix bug that wont set position on instantiate
        triOne.transform.position = transform.position;
        triTwo.transform.position = transform.position;
        triThree.transform.position = transform.position;

        //tween outwards
        HOTween.To(triOne.transform, miniTriExpandSpeed, "position", triOnePos.position);
        HOTween.To(triTwo.transform, miniTriExpandSpeed, "position", triTwoPos.position);
        HOTween.To(triThree.transform, miniTriExpandSpeed, "position", triThreePos.position);

        //set there initial full spin
        Vector3 oneRot = triOne.transform.eulerAngles, twoRot = triTwo.transform.eulerAngles, threeRot = triThree.transform.eulerAngles;
        oneRot.y += 360; twoRot.y += 360; threeRot.y += 360;
        //they full spin infinitely here
        HOTween.To(triOne.transform, 1, new TweenParms().Prop("eulerAngles", oneRot).Loops(-1).Ease(EaseType.Linear));
        HOTween.To(triTwo.transform, 1, new TweenParms().Prop("eulerAngles", twoRot).Loops(-1).Ease(EaseType.Linear));
        HOTween.To(triThree.transform, 1, new TweenParms().Prop("eulerAngles", threeRot).Loops(-1).Ease(EaseType.Linear));

    }

    public void sendTrisRotating(){
        triOne.transform.RotateAround(transform.position, Vector3.up, speedTriRotAround * Time.deltaTime);
        triTwo.transform.RotateAround(transform.position, Vector3.up, speedTriRotAround * Time.deltaTime);
        triThree.transform.RotateAround(transform.position, Vector3.up, speedTriRotAround * Time.deltaTime);
    }
	public float CurrentHealth {
		get { return currentHealth; }
	}
	
	public bool CanHailMary {
		get { return canHailMary; }
	}
}
