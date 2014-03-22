#pragma strict

var stop : GameObject;
var channelHeal : GameObject;
var dash  : GameObject;
var move  : GameObject;
var circleHP  : GameObject;
var withinDis  : GameObject;
var dash2  : GameObject;
var explode : GameObject;
var pickRanPoint  : GameObject;
var leaveBomb : GameObject;
var teleport  : GameObject;

var stopChannel : Sequence;
var dashMove : Sequence;
var defensive : RandomSelector;
var DWhenLow : Sequence;
var aggressive : Sequence;
var hiMary : Sequence;
var circleTree : Selector;
function Start () {
	//sequence for stop then channel heal
	stopChannel = new Sequence();
	//var stop : GameObject;
	//var channelHeal : GameObject;
	stopChannel.children.Add(stop.GetComponent(Stop));
	stopChannel.children.Add(stopChannel);

	//sequence for dash then move to the cloest point 3
	dashMove = new Sequence();
	//var dash  : GameObject;
	//var move  : GameObject;
	dashMove.children.Add(dash.GetComponent(Dash));
	dashMove.children.Add(move.GetComponent(MoveToClosestPointThree));
	
	//defensive tree 
	defensive = new RandomSelector();
	defensive.children.Add(stopChannel);
	defensive.children.Add(dashMove);
	
	//defense when low hp tree 
	DWhenLow = new Sequence();
	//var circleHP  : GameObject;
	DWhenLow.children.Add(circleHP.GetComponent(CircleHPCondition));
	DWhenLow.children.Add(defensive);
	
	//aggressive tree
	aggressive = new Sequence();
	//var withinDis  : GameObject;
	//var dash2  : GameObject;
	//var explode : GameObject;
	aggressive.children.Add(withinDis.GetComponent(CircleDistanceCondition));
	aggressive.children.Add(dash2.GetComponent(Dash));
	aggressive.children.Add(explode.GetComponent(Explode));
	
	//hail mary tree
	hiMary = new Sequence();
	//var pickRanPoint  : GameObject;
	//var leaveBomb : GameObject;
	//var teleport  : GameObject;
	hiMary.children.Add(pickRanPoint.GetComponent(PickRNDPointThree));
	hiMary.children.Add(leaveBomb.GetComponent(LeaveBomb));
	hiMary.children.Add(teleport.GetComponent(Teleport));
	
	//the entire circle tree
	circleTree = new Selector();
	circleTree.children.Add(DWhenLow);
	circleTree.children.Add(aggressive);
	circleTree.children.Add(hiMary);
	
	//circleTree.Run();
	runTree();
}

function runTree(){
	circleTree.Run();
	Invoke("runTree", 1.5);
}

