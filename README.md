README

Stickball GameController using Observer pattern.
this is to get all the game states, ball states and player (pitcher) states under wraps
comminicate between them.

Gameplay: Bottom of the 9th losing by one run, you get 3 outs to score 2 runs to win the game.

GameController/Ball/Player Observer pattern

https://www.raywenderlich.com/2799-intermediate-unity-3d-for-ios-part-2-3


 Scoring
 Game Controller
 variables
 //total bases vars
// used to track the total bases of the current ball (batter)
  _newTotalBases = 0;

// baserunner indicator vars for scoreboard
  _runnerOnFirst = false;

// used to track the total runs for this at bat
  _newRuns = 0;

// used to track the total runs for the player (user)
 _totalRuns  

  functions
  Ball interacts with something and sends event
  event fires funciton in Controller. function sends updatescore()
  each function sets _newTotalbases unless its a ball or strike

  UpdateScore();
  uses if/else to determine strikeout vs walk 
  _newTotalBases is updated here for walks only

  UpdateBaserunners(_newTotalBases);
  _newTotalBases is used to set booleans _runnerOnFirst etc..
  lots of conditionals here so i put _newRuns here to track total runs for each ball event

  UpdateScoreboard();
  Updates scoreboard using scoreboard Functions
  Runs are calculated in a getTotalRuns function _totalRuns +=_newRuns

  SetDeadBall();
  _currentTotalBases is set from _newTotalBases

