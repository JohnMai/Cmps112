//Add particle effect for channel heal.
#pragma strict
import Holoville.HOTween;

var maxHealth : int = 100;
var defSpeed : float = 3.5;
var defAcc : float = 8;

var dashSpeed : float = 20;
var dashAcc : float = 50;
var parms : TweenParms = new TweenParms();
var outParms : TweenParms = new TweenParms();
var healMagnitude : int = 1;
var healFrequency : float = 0.2;

var attackCD = 1.5;
var healCD = 5;
var dashCD = 1.5;
var hailMaryCD = 10;

var explosion : GameObject;
var bomb : GameObject;
private var currentExplosion : GameObject;

var triangle : GameObject;
var triAgent : NavMeshAgent;
//Test point.
var point : Transform;

private var agent : NavMeshAgent;
private var currentHealth : int;

private var isDashing : boolean;
private var isAttacking : boolean;
private var channeling : boolean;
private var hailMary : boolean;

private var canAttack : boolean;
private var canChannel : boolean;
private var canHailMary : boolean;
private var canDash : boolean;

private var myHeading : GameObject;

//Set up in inspector for now.
var rankOneWaypoints : GameObject[];
var rankTwoWaypoints : GameObject[];;
var rankThreeWaypoints : GameObject[];;

function Start () {
	agent = GetComponent.<NavMeshAgent>();
	triAgent = triangle.GetComponent.<NavMeshAgent>();
	
	parms.Prop("speed", dashSpeed);
	parms.Ease(EaseType.EaseInCubic);
	
	outParms.Prop("speed", defSpeed);
	outParms.Ease(EaseType.EaseInCubic);
	
	channeling = false;
	isAttacking = false;
	isDashing = false;
	
	canAttack = true;
	canChannel = true;
	canDash = true;
	canHailMary = true;
	
	//startHailMary();
	startAttack();
	//startEvade();
}

function Update () {
	moveToDestination(point.position);
}

//Can we change destination during attack and channel?
function moveToDestination(destination : Vector3) {
	agent.SetDestination(point.position);
}

function getCurrentHealth () : float {
	return this.currentHealth;
}

/*function setCurrentHealth (health : float) {
	this.currentHealth = health;
}*/

// ---------------- Aggressive Actions -------------------//
//Should be called by behavior tree. Meaning that dash won't be called from here but it will enabled dash on trigger.
//Should maybe return bool on success or failure depending on canAttack
function startAttack(){
	if(canAttack){
		point = triangle.transform;
		isAttacking = true;
	}
}

function explode(){
	point = this.transform;
	Destroy(myHeading);
	var explosionPosition : Vector3 = transform.position;
	explosionPosition.y = 0.65;
	currentExplosion = Instantiate(explosion, explosionPosition, Quaternion.identity);
	isAttacking = false;
	attackCooldown(attackCD);
}

function attackCooldown(time : float){
	canAttack = false;
	yield WaitForSeconds(time);
	canAttack = true;
}

// ---------------- Defensive Actions --------------------//
//Should maybe return bool on success or failure depending on canChannel
function startChannel(){
	if(canChannel){
		channeling = true;
		channelHeal();
	}
}

//Should maybe return bool on success or failure depending on canDash.
function startEvade(){
	if(canDash){
		isAttacking = false;
		dash();
	}
}

function channelHeal(){
	if(canChannel && currentHealth < maxHealth){
		healDamage(healMagnitude);
		
		Invoke("channelHeal", healFrequency);
	}
	else {
		channeling = false;
		channelCooldown(healCD);
	}
}

function channelCooldown(time : float){
	canChannel = false;
	yield WaitForSeconds(time);
	canChannel = true;
}

// -------------------- Hail Mary ------------------------//
//Should maybe return bool on success or failure depending on canHailMary.
function startHailMary(){
	if(canHailMary){
		hailMary = true;
	}
}

function teleport(){
	myHeading = new GameObject("target");
	
	createHeading();
	dropBomb();
	
	transform.position = myHeading.transform.position;
}

function dropBomb(){
	hailMary = false;
	Instantiate(bomb, transform.position, Quaternion.identity);
	hailMaryCooldown(hailMaryCD);
}

function hailMaryCooldown(time : float){
	canHailMary = false;
	yield WaitForSeconds(time);
	canHailMary = true;
}

// ---------------- Helper Functions --------------------//

function dash(){
	isDashing = true;
	gameObject.GetComponent.<TrailRenderer>().time = 0.25;
	
	myHeading = new GameObject("target");
	
	createHeading();

	point = myHeading.transform;
	
	agent.acceleration = dashAcc;
	HOTween.To(agent, 0.175, parms.OnComplete(slowDown));
}

function slowDown(){
	Debug.Log("slowing down");
	//0.125
	HOTween.To(agent, 0.125, outParms.OnComplete(stopDash));
}

function stopDash(){
	Debug.Log("stop");
	isDashing = false;
	agent.acceleration = dashAcc * 0.34;
	HOTween.To(gameObject.GetComponent.<TrailRenderer>(), 1.75, "time", 0);
	if(channeling){
		point = this.transform;
		Destroy(myHeading);
		Debug.Log("gonna heal");
		Invoke("channelHeal", 0.05);
	}
	else if (isAttacking){
		Debug.Log("gonna Explode");
		Invoke("explode", 0.05);
	}
	else {
		//Continue to point 3
		agent.speed = defSpeed * 0.5;
	}
	dashCooldown(dashCD);
	wait();
}

//Can't invoke coroutines.
function wait(){
	yield WaitForSeconds(0.2);
	agent.acceleration = defAcc;
	agent.speed = defSpeed;
	Debug.Log("evading");
}

function dashCooldown(time : float){
	canDash = false;
	yield WaitForSeconds(time);
	canDash = true;
}

function healDamage(health : int){
	currentHealth += health;
}

function applyDamage(){
	canChannel = false;
}

function createHeading(){
	if(isAttacking){
		myHeading.transform.position = (grabLastDirVector());
	}
	else if(!isAttacking || hailMary){
		myHeading.transform.position = (rankThreeWaypoints[generateRandomRankThree()].transform.position);
	}
}

//Might not be working correctly
function grabLastDirVector() : Vector3{
	var triPosition : Vector3 = new Vector3(triangle.transform.position.x, transform.position.y, triangle.transform.position.z);
	var triSpeed : Vector3 = new Vector3(triAgent.velocity.x, transform.position.y, triAgent.velocity.z);
	var dirVector : Vector3 = ((triPosition + triSpeed) - transform.position);
	return dirVector;
}

function generateRandomRankThree () : int{
	return (Random.Range(0, rankThreeWaypoints.Length - 1));
}

function OnTriggerEnter(other : Collider){
	if(other.gameObject.name == "Triangle" && isAttacking){
		dash();
	}
	else if(other.gameObject.name == "Triangle" && hailMary){
		teleport();
	}
}