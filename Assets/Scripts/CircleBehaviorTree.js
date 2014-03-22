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
function Start () {
	//sequence for stop then channel heal
	var stopChannel = new Sequence();
	//var stop : GameObject;
	//var channelHeal : GameObject;
	stopChannel.children.Add(stop.GetComponent(Stop));
	stopChannel.children.Add(stopChannel);

	//sequence for dash then move to the cloest point 3
	var dashMove = new Sequence();
	//var dash  : GameObject;
	//var move  : GameObject;
	dashMove.children.Add(dash.GetComponent(Dash));
	dashMove.children.Add(move.GetComponent(MoveToClosestPointThree));
	
	//defensive tree 
	var defensive = new RandomSelector();
	defensive.children.Add(stopChannel);
	defensive.children.Add(dashMove);
	
	//defense when low hp tree 
	var DWhenLow = new Sequence();
	//var circleHP  : GameObject;
	DWhenLow.children.Add(circleHP.GetComponent(CircleHPCondition));
	DWhenLow.children.Add(defensive);
	
	//aggressive tree
	var aggressive = new Sequence();
	//var withinDis  : GameObject;
	//var dash2  : GameObject;
	//var explode : GameObject;
	aggressive.children.Add(withinDis.GetComponent(CircleDistanceCondition));
	aggressive.children.Add(dash2.GetComponent(Dash));
	aggressive.children.Add(explode.GetComponent(Explode));
	
	//hail mary tree
	var hiMary = new Sequence();
	//var pickRanPoint  : GameObject;
	//var leaveBomb : GameObject;
	//var teleport  : GameObject;
	hiMary.children.Add(pickRanPoint.GetComponent(PickRNDPointThree));
	hiMary.children.Add(leaveBomb.GetComponent(LeaveBomb));
	hiMary.children.Add(teleport.GetComponent(Teleport));
	
	//the entire circle tree
	var circleTree = new Selector();
	circleTree.children.Add(DWhenLow);
	circleTree.children.Add(aggressive);
	circleTree.children.Add(hiMary);
	
	circleTree.Run();
}

