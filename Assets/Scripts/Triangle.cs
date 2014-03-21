using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using ProBuilder2.Common;

public class Triangle : MonoBehaviour {
    public float maxHealth = 100, chargeTimeSec = 4, miniTriExpandSpeed = 1, speedTriRotAround = 1, beamWidth = 0.2f, shotWidth = 0.1f, beamSpeed = 0.2f, shotSpeed  = 0.1f;
    public GameObject circle, miniTrianglePrefab, bulletPrefab;
    public Transform point, triOnePos, triTwoPos, triThreePos, levelOneWaypoint;
    public Transform[] level2Waypoints = new Transform[2], level3Waypoints = new Transform[4];

    private GameObject triOne, triTwo, triThree;
	private NavMeshAgent agent;
    private LineRenderer triLineOne, triLineTwo, triLineThree;
    private float currentHealth;
    [HideInInspector]
    public float shotTempWidth;
    private bool canHailMary = true, canRotate = false;
	// Use this for initialization
	void Start () {
        shotTempWidth = 0;
		agent = GetComponent<NavMeshAgent>();
        triLineOne = triOnePos.GetComponent<LineRenderer>();
        triLineTwo = triTwoPos.GetComponent<LineRenderer>();
        triLineThree = triThreePos.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        //debugging
        if(Input.GetKeyUp(KeyCode.LeftArrow)){
            StartCoroutine(ChargeUp());
        }
        //debugging
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            SpawnTriangles();
        }
        //debugging
        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            FireBullets();
            //FireBeam(shotWidth, shotSpeed);
        }
        //debugging
        if (Input.GetKeyUp(KeyCode.RightArrow)) {
            int randomNumber = Random.Range(1,4);
            MoveToDestination(randomNumber);
        }

        if (canRotate) {
            sendTrisRotating();
        }

	}

    //called by other functions to send us damage
    public void ApplyDamage(int damage) {
        currentHealth -= damage;

        if (damage < 0)
            death();
    }

    //tells our object to move to a specific location
	public void MoveToDestination(Vector3 position) {
        StartCoroutine(RandomSpin());
		agent.SetDestination(point.position);
	}

    public void MoveToDestination(int zone) {
        StartCoroutine(RandomSpin());
        int randomNumber = Random.Range(0, 4);
        if (zone == 1) {
            agent.SetDestination(point.position);
        } else if (zone == 2) {
            agent.SetDestination(level2Waypoints[randomNumber].position);
        } else if (zone == 3) {
            agent.SetDestination(level3Waypoints[randomNumber].position);
        }
    }
    public IEnumerator RandomSpin() {
        Vector3 newRotation = gameObject.transform.eulerAngles;
        newRotation.y += Random.Range(0f, 360f);
        speedTriRotAround = -1 * speedTriRotAround;
        yield return StartCoroutine(HOTween.To(gameObject.transform, 1, new TweenParms().Prop("eulerAngles", newRotation).Ease(EaseType.Linear)).WaitForCompletion());
        speedTriRotAround = -1 * speedTriRotAround;
    }

    //makes our triangle spin to charge up
	public IEnumerator ChargeUp() {
		Vector3 newRotation = gameObject.transform.eulerAngles;
		newRotation.y += 360;
        speedTriRotAround = -1*speedTriRotAround;
        yield return StartCoroutine(HOTween.To(gameObject.transform, chargeTimeSec, new TweenParms().Prop("eulerAngles", newRotation).Ease(EaseType.Linear)).WaitForCompletion());
        speedTriRotAround = -1*speedTriRotAround;
        FireBeam(beamWidth, beamSpeed);

	}

    //fires a beam of energy from the tips of our triangle
    public void FireBeam(float widthOfShot, float speedOfShot) {
        RaycastHit hitOne, hitTwo, hitThree;

        Vector3 mainPosition = new Vector3(transform.position.x, triOnePos.position.y, transform.position.z);
        Vector3 dirOne = triOnePos.position - mainPosition, dirTwo = triTwoPos.position - mainPosition, dirThree = triThreePos.position - mainPosition;
        dirOne.Normalize();

        Physics.Raycast(mainPosition, dirOne, out hitOne);
        Physics.Raycast(mainPosition, dirTwo, out hitTwo);
        Physics.Raycast(mainPosition, dirThree, out hitThree);

        if (hitOne.collider.gameObject.tag == "Cylinder" || hitTwo.collider.gameObject.tag == "Cylinder" || hitThree.collider.gameObject.tag == "Cylinder")
            hitOne.collider.gameObject.SendMessage("ApplyDamage", -1);
        StartCoroutine(HelperBeam(triLineOne, triOnePos, hitOne, widthOfShot, speedOfShot));
        StartCoroutine(HelperBeam(triLineTwo, triTwoPos, hitTwo, widthOfShot, speedOfShot));
        StartCoroutine(HelperBeam(triLineThree, triThreePos, hitThree, widthOfShot, speedOfShot));


    }

    //fires actual bullets in specified direction
    public void FireBullets() {
        Vector3 mainPosition = new Vector3(transform.position.x, triOnePos.position.y, transform.position.z);
        Vector3 dirOne = triOnePos.position - mainPosition, dirTwo = triTwoPos.position - mainPosition, dirThree = triThreePos.position - mainPosition;
        dirOne.Normalize(); dirTwo.Normalize(); dirThree.Normalize();

        //set them to spawn in center of tri 
        triOne = ProBuilder.Instantiate(bulletPrefab, new Vector3(0, 0, 1), Quaternion.identity);
        triTwo = ProBuilder.Instantiate(bulletPrefab, new Vector3(1, 0, 0), Quaternion.identity);
        triThree = ProBuilder.Instantiate(bulletPrefab, new Vector3(1, 0, 0), Quaternion.identity);

        //here to fix bug that wont set position on instantiate
        triOne.transform.position = triOnePos.position;
        triTwo.transform.position = triTwoPos.position;
        triThree.transform.position = triThreePos.position;

        triOne.rigidbody.AddRelativeForce(dirOne*1000);
        triTwo.rigidbody.AddRelativeForce(dirTwo*1000);
        triThree.rigidbody.AddRelativeForce(dirThree*1000);
    }

    //sends our minitriangles eminating from the center of our triangle
    public void SpawnTriangles() {
        canRotate = true;
        //set them to spawn in center of tri 
        triOne = ProBuilder.Instantiate(miniTrianglePrefab, new Vector3(0, 0, 1), Quaternion.identity);
        triTwo = ProBuilder.Instantiate(miniTrianglePrefab, new Vector3(1, 0, 0), Quaternion.identity);
        triThree = ProBuilder.Instantiate(miniTrianglePrefab, new Vector3(1, 0, 0), Quaternion.identity);

        triOne.transform.parent = transform;
        triTwo.transform.parent = transform;
        triThree.transform.parent = transform;

        //here to fix bug that wont set position on instantiate
        triOne.transform.localPosition = Vector3.zero;
        triTwo.transform.localPosition = Vector3.zero;
        triThree.transform.localPosition = Vector3.zero;

        //tween outwards
        HOTween.To(triOne.transform, miniTriExpandSpeed, "localPosition", triOnePos.localPosition);
        HOTween.To(triTwo.transform, miniTriExpandSpeed, "localPosition", triTwoPos.localPosition);
        HOTween.To(triThree.transform, miniTriExpandSpeed, "localPosition", triThreePos.localPosition);

        //set there initial full spin
        Vector3 oneRot = triOne.transform.eulerAngles, twoRot = triTwo.transform.eulerAngles, threeRot = triThree.transform.eulerAngles;
        oneRot.y += 360; twoRot.y += 360; threeRot.y += 360;
        //they full spin infinitely here
        HOTween.To(triOne.transform, 1, new TweenParms().Prop("eulerAngles", oneRot).Loops(-1).Ease(EaseType.Linear));
        HOTween.To(triTwo.transform, 1, new TweenParms().Prop("eulerAngles", twoRot).Loops(-1).Ease(EaseType.Linear));
        HOTween.To(triThree.transform, 1, new TweenParms().Prop("eulerAngles", threeRot).Loops(-1).Ease(EaseType.Linear));

    }

    //makes the triangles rotate around our triangle
    public void sendTrisRotating(){
        triOne.transform.RotateAround(transform.position, Vector3.up, speedTriRotAround * Time.deltaTime);
        triTwo.transform.RotateAround(transform.position, Vector3.up, speedTriRotAround * Time.deltaTime);
        triThree.transform.RotateAround(transform.position, Vector3.up, speedTriRotAround * Time.deltaTime);
    }

    //accesor functions 
	public float CurrentHealth {
		get { return currentHealth; }
	}
	
	public bool CanHailMary {
		get { return canHailMary; }
	}

    //sets up the linerenders for our fireBeam function
    private IEnumerator HelperBeam(LineRenderer beam, Transform beamStart, RaycastHit hit, float widthOfShot, float speedOfShot) {

        //set the Line renderers for the beams
        beam.SetPosition(1, hit.point);
        beam.SetPosition(0, beamStart.position);
        yield return StartCoroutine(HOTween.To(this, speedOfShot, new TweenParms().Prop("shotTempWidth", widthOfShot).OnUpdate(BeamAppDissapear, beam)).WaitForCompletion());
        HOTween.To(this, speedOfShot, new TweenParms().Prop("shotTempWidth", 0).OnUpdate(BeamAppDissapear, beam));

    }

    //allows the beam to appear and dissapear with hotween
    private void BeamAppDissapear(TweenEvent data) {
        LineRenderer beam = (LineRenderer)data.parms[0];
        beam.SetWidth(shotTempWidth, shotTempWidth);
    }

    //tell me how to die damnit
    private void death() {

    }
}
