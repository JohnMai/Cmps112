#pragma strict

function Start () {
	//sequence for stop then channel heal
	var stopChannel = new Sequence();
	var stop = new Stop();
	var channelHeal = new ChannelHeal();
	stopChannel.children.Add(stop);
	stopChannel.children.Add(stopChannel);

	//sequence for dash then move to the cloest point 3
	var dashMove = new Sequence();
	var dash = new Dash();
	var move = new MoveToClosestPointThree();
	dashMove.children.Add(dash);
	dashMove.children.Add(move);
	
	//defensive tree 
	var defensive = new RandomSelector();
	defensive.children.Add(stopChannel);
	defensive.children.Add(dashMove);
	
	//defense when low hp tree 
	var DWhenLow = new Sequence();
	var circleHP = new CircleHPCondition();
	DWhenLow.children.Add(circleHP);
	DWhenLow.children.Add(defensive);
	
	//aggressive tree
	var aggressive = new Sequence();
	var withinDis = new CircleDistanceCondition();
	var dash2 = new Dash(); 
	var explode = new Explode();
	aggressive.children.Add(withinDis);
	aggressive.children.Add(dash2);
	aggressive.children.Add(explode);
	
	//hail mary tree
	var hiMary = new Sequence();
	var pickRanPoint = new PickRNDPointThree();
	var leaveBomb = new LeaveBomb();
	var teleport = new Teleport();
	hiMary.children.Add(pickRanPoint);
	hiMary.children.Add(leaveBomb);
	hiMary.children.Add(teleport);
	
	//the entire circle tree
	var circleTree = new Selector();
	circleTree.children.Add(DWhenLow);
	circleTree.children.Add(aggressive);
	circleTree.children.Add(hiMary);
	
	circleTree.Run();
}

