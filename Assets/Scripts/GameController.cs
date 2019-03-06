using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRStickball {

    [RequireComponent(typeof(GameBallController))]

    public class GameController : MonoBehaviour
    {
        
        private static GameController _instance = null;

        public static GameController SharedInstance {
            get {
                if (_instance == null) {
                    _instance = GameObject.FindObjectOfType (typeof(GameController)) as GameController;
                }

                return _instance;
            }
        }
        
        // UI
        public GameObject startBtn;
        // Score Variables
        private float _inning;
        private float _outs;
        private float _balls;
        private float _strikes;
        
        //temporarily hold runs
        private float _totalRuns;

        private float _newRuns;

        private float _homeRuns;
        private float _visitorRuns;
        private float _homeHits;
        private float _visitorHits;
        private float _homeErrors;
        private float _visitorErrors;
        // bases
        private bool _runnerOnFirst;
        private bool _runnerOnSecond;
        private bool _runnerOnThird;
        // tb
        private float _newTotalBases;
        // _bases Array an array of bools to keep track of base runner indicators
        private bool[] _bases;

        // Gameplay Variables
        //Player
        public PlayerController pitcher;
        // dynamic ball (NOT USED)
        public GameObject gameBall;
        // static ball
        public GameBallController ball;
        // scoreboard
        public ScoreboardController scoreBoardController;
   

       

        // Enum for states
        public enum GameStateEnum {
            Undefined,
            Menu,
            Paused,
            Play,
            GameOver
        }

        // initial ball state
		private GameStateEnum _state = GameStateEnum.Paused;

        public GameStateEnum State {
            get {
                return _state;
            } set {
                _state = value;

                //MENU
                if (_state == GameStateEnum.Menu) {
                    Debug.Log("State Change - Menu");
                    // change player state
                    //player.State = Player.PlayerStateEnum.DoSomethinng;
                // StartNewGame()
                }
                //PAUSED
                else if (_state == GameStateEnum.Paused) {
                    Debug.Log("State change - Paused");
                    pitcher.State = VRStickball.PlayerController.PlayerStateEnum.Idle;

                }
                //PLAY
                else if (_state == GameStateEnum.Play) {
                    Debug.Log("State change - Play");
                    pitcher.State = VRStickball.PlayerController.PlayerStateEnum.Walk;
                }
                //GAME OVER
                else if (_state == GameStateEnum.GameOver) {
                    Debug.Log("State Change - GameOver");
                    pitcher.State = VRStickball.PlayerController.PlayerStateEnum.Idle;
                // StartNewGame
                }
            }
        }

        
        void Awake() {
            _instance = this;


        }


        // Use this for initialization
        void Start()
        {

            // observer add
            ball.OnStrike += Handle_OnStrike;
            ball.OnBall += Handle_OnBall;
            ball.OnSingle += Handle_OnSingle;
            ball.OnDouble += Handle_OnDouble;
            ball.OnTriple += Handle_OnTriple;
            ball.OnHomeRun += Handle_OnHomeRun;
            ball.OnFoulBall += Handle_OnFoulBall;
            ball.OnOut += Handle_OnOut;
            pitcher.OnPlayerAnimationFinished += HandleOnPlayerAnimationFinished;

            // Set Variables
            _inning = 9;
            _outs = 0;
            _balls = 0;
            _strikes = 0;
            _homeHits = 0;
            _visitorHits = 0;
            _homeErrors = 0;
            _visitorErrors = 0;
            _homeRuns = 0;
            _totalRuns = 0;
            _newRuns = 0;
            _visitorRuns = 1;

            // to help with tracking baserunners
            _runnerOnFirst = false;
            _runnerOnSecond = false;
            _runnerOnThird = false;
            
            // count TB to keep Score (current)
            _newTotalBases = 0;

             // Bases Array to track bases for indicators
            _bases = new bool[3];
            // setting here isn't really working so i set it again after each score
            _bases[0] = _runnerOnFirst;
            _bases[1] = _runnerOnSecond;
            _bases[2] = _runnerOnThird;

            // Set Game State
            State = GameStateEnum.Paused;


            //Find Controller Codes when object isn't a game Object
            scoreBoardController = GameObject.Find("Scoreboard").GetComponent<ScoreboardController>();
            pitcher = GameObject.Find("Player").GetComponent<PlayerController>();

           // for now update the scoreboard to show state even if game isn't running
            scoreBoardController.SetGameState("GameState: " + _state.ToString());
            scoreBoardController.SetBallState("BallState: " + ball.State.ToString());


           //StartGame();

        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void StartGame() {
           
           Debug.Log("[GameController] StartGame()");

           startBtn.SetActive (false);

           State = GameStateEnum.Play;

           StartCoroutine (PitchBall ());


          // state seperate from scoreboard for now
          scoreBoardController.SetBallState("BallState: " + ball.State.ToString());
          scoreBoardController.SetGameState("GameState: " + _state.ToString());

            
        }



        //protected methods from ball
        protected void Handle_OnStrike() {   
            Debug.Log("![GameController] Handle_OnStrike() ball.State = " + ball.State);        
            _strikes++;
            UpdateScore();
		}

        protected void Handle_OnBall() {
             Debug.Log("![GameController] Handle_OnBall() ball.State = " + ball.State);        
            _balls++;
            UpdateScore();
        }

        protected void Handle_OnSingle() {
             Debug.Log("![GameController] Handle_OnSingle() ball.State = " + ball.State);        
            //total bases logic
            _homeHits++;
            _newTotalBases = 1;
            _strikes = 0;
            _balls = 0;
            UpdateScore();
        }

        protected void Handle_OnDouble() {
             Debug.Log("![GameController] Handle_OnDouble() ball.State = " + ball.State);        
            _homeHits++;
            _newTotalBases = 2;
            _strikes = 0;
            _balls = 0;
            UpdateScore();
        }

        protected void Handle_OnTriple() {
             Debug.Log("![GameController] Handle_OnTriple() ball.State = " + ball.State);        
            //total bases logic
            _homeHits++;
            _newTotalBases = 3;
            _strikes = 0;
            _balls = 0;
            UpdateScore();
        }

        protected void Handle_OnHomeRun() {
             Debug.Log("![GameController] Handle_OnHomeRun() ball.State = " + ball.State);        
            //total bases logic
            _homeHits++;
            _newTotalBases = 4;
            _strikes = 0;
            _balls = 0;
            UpdateScore();
        }

        protected void Handle_OnFoulBall() {
             Debug.Log("![GameController] Handle_OnFoulBall() ball.State = " + ball.State);        
            //total bases logic
             _strikes++;
            UpdateScore();
        }

        protected void Handle_OnOut() {
             Debug.Log("![GameController] Handle_OnOut() ball.State = " + ball.State);        
            //total bases logic
             _outs++;
             _strikes = 0;
             _balls = 0;
            UpdateScore();
        }


        // listen to public methods from ball
        public void OnBallCollisionEnter (Collision collision) {
            Debug.Log ("[GameController] : OnBallCollisionEnter()");
        }

        // protected methods from player
        protected void HandleOnPlayerAnimationFinished(string animationName) {
            // TODO tell player what to do when an animation is complete

        }


        private void UpdateScore() {
            Debug.Log ("[GameController] : UpdateScore()");
            if (_balls < 4 && _strikes <3 && _outs < 3) {

                Debug.Log ("[GameController] : UpdateScore() if (_balls < 4 && _strikes <3 && _outs < 3)");


            } else if (_strikes == 3  && _outs < 3) {
                _outs++;
                
               // CurrentBatter = 0;
               // _batters.Add(CurrentBatter);

                _strikes = 0;
                _balls = 0;
               
            } else if (_balls == 4 && _outs < 3) {
               
               _newTotalBases = 1;

                Debug.Log ("[GameController] : UpdateScore() n_balls==4 && _outs < 3 _newTotalBases = " + _newTotalBases);
              // CurrentBatter = 1;
             //  _batters.Add(CurrentBatter);
               _balls = 0;
               _strikes = 0;
            } else if (_outs > 2) {
               // GameOver(); <-- set this in the While Loop of the IEnumerator PitchBall;
            }


            UpdateBaseRunners(_newTotalBases);
            //UpdateGameState()
            
        }

        

        private void UpdateBaseRunners(float _newTotalBases) {
             Debug.Log("[GameController UpdateBaseRunners() _newTotalBases = " + _newTotalBases);

            if (_runnerOnFirst == false && _runnerOnSecond == false && _runnerOnThird == false) {
                // bases empty
                if (_newTotalBases == 0) {

                } else if (_newTotalBases == 1) {
                    _runnerOnFirst = true;
                    
                } else if (_newTotalBases == 2) {
                    _runnerOnSecond = true;
                    
                } else if (_newTotalBases == 3) {
                    _runnerOnThird = true;   
                } 
            } else if (_runnerOnFirst == true && _runnerOnSecond == false && _runnerOnThird == false) {
                // runner on first
                 if (_newTotalBases == 0) {

                } else if (_newTotalBases == 1) {
                    _runnerOnSecond = true;
                    
                } else if (_newTotalBases == 2) {
                    _runnerOnFirst = false;
                    _runnerOnSecond = true;
                    _runnerOnThird = true;
                    
                } else if (_newTotalBases == 3) {
                     _runnerOnFirst = false;
                    _runnerOnThird = true;
                    _newRuns = 1;
                    
                } else if (_newTotalBases == 4) {
                    _runnerOnFirst = false;
                    _newRuns = 2;
                }

            } else if (_runnerOnFirst == false && _runnerOnSecond == true && _runnerOnThird == false) {
                // runnner on second
                 if (_newTotalBases == 0) {

                } else if (_newTotalBases == 1) {
                    _runnerOnFirst = true;
                    _runnerOnSecond = false;
                    _runnerOnThird = true;
                } else if (_newTotalBases == 2) {
                    _newRuns = 1;
                    
                } else if (_newTotalBases == 3) {
                    _runnerOnSecond = false;
                    _runnerOnThird = true;
                    _newRuns = 1;
                    
                } else if (_newTotalBases == 4) {
                    _runnerOnSecond = false;
                    _newRuns = 2;
                }

            } else if (_runnerOnFirst == false && _runnerOnSecond == false && _runnerOnThird == true) {
                // runner on third
                 if (_newTotalBases == 0) {

                } else if (_newTotalBases == 1) {
                    _runnerOnFirst = false;
                    _runnerOnThird = false;
                    _newRuns = 1;
                    
                } else if (_newTotalBases == 2) {
                    _runnerOnSecond = true;
                    _runnerOnThird = false;
                    _newRuns = 1;
                    
                } else if (_newTotalBases == 3) {
                    _newRuns = 1;
                    
                } else if (_newTotalBases == 4) {
                    _runnerOnThird = false;
                    _newRuns = 2;
                }

            } else if (_runnerOnFirst == true && _runnerOnSecond == true && _runnerOnThird == false) {
                // first and second
                 if (_newTotalBases == 0) {
                     //nothing changes
                } else if (_newTotalBases == 1) {

                    _runnerOnThird = true;
                    
                } else if (_newTotalBases == 2) {
                     _runnerOnFirst = false;
                     _runnerOnThird = true; 
                     _newRuns = 1;       
                    
                } else if (_newTotalBases == 3) {
                    _runnerOnFirst = false;
                    _runnerOnSecond = false;
                    _runnerOnThird = true;
                    _newRuns = 2;
                    
                } else if (_newTotalBases == 4) {
                     _runnerOnFirst = false;
                    _runnerOnSecond = false;
                    _newRuns = 3;
                }

            } else if (_runnerOnFirst == true && _runnerOnSecond == false && _runnerOnThird == true) {
                // first and third
                 if (_newTotalBases == 0) {

                } else if (_newTotalBases == 1) {
                    _runnerOnSecond = true;
                    _runnerOnThird = false;
                    _newRuns = 1;

                } else if (_newTotalBases == 2) {
                    _runnerOnFirst = false;
                    _runnerOnSecond = true;
                    _newRuns = 1;
                    
                } else if (_newTotalBases == 3) {
                    _runnerOnFirst = false;
                    _newRuns = 2;

                } else if (_newTotalBases == 4) {
                    _runnerOnFirst = false;
                    _runnerOnThird = false;
                    _newRuns = 3;
                    
                }

            } else if (_runnerOnFirst == false && _runnerOnSecond == true && _runnerOnThird == true) {
                // second and third
                 if (_newTotalBases == 0) {

                } else if (_newTotalBases == 1) {
                    _runnerOnFirst = true;
                    _runnerOnSecond = false;
                    _newRuns = 1;
                    
                } else if (_newTotalBases == 2) {
                    _runnerOnThird = false;
                    _newRuns = 2;
                    
                } else if (_newTotalBases == 3) {
                    _runnerOnSecond = false;
                    _newRuns = 2;
                    
                } else if (_newTotalBases == 4) {
                    _runnerOnSecond = false;
                    _runnerOnThird = false;
                    _newRuns = 3;
                    
                }

            } else if (_runnerOnFirst == true && _runnerOnSecond == true && _runnerOnThird == true) {
                // bases loaded
                 if (_newTotalBases == 0) {

                } else if (_newTotalBases == 1) {
                    //bases loaded
                    _newRuns = 1;
                    
                } else if (_newTotalBases == 2) {
                    _runnerOnFirst = false;
                    _newRuns = 2;
                    
                } else if (_newTotalBases == 3) {
                    _runnerOnFirst = false;
                    _runnerOnSecond = false;
                    _newRuns = 3;
                    
                } else if (_newTotalBases == 4) {
                    _runnerOnFirst = false;
                    _runnerOnSecond = false;
                    _runnerOnThird = false;
                    _newRuns = 4;
                }
            } 

            // set bases array again
            _bases[0] = _runnerOnFirst;
            _bases[1] = _runnerOnSecond;
            _bases[2] = _runnerOnThird;

            UpdateRunsScored();
            UpdateScoreBoard();
            SetDeadBall();

        }


        private void UpdateRunsScored() {
             _homeRuns = TotalRuns(_newRuns);
        }

        private void UpdateScoreBoard() {
          Debug.Log("[GameController] UpdateScoreBoard() ");

         

          scoreBoardController.SetBalls("balls: " + _balls.ToString());
          scoreBoardController.SetStrikes("strikes: " + _strikes.ToString());
          scoreBoardController.SetOuts("outs: " + _outs.ToString());
          scoreBoardController.SetRuns("runs: " + _homeRuns.ToString());
          scoreBoardController.SetTB(_bases);

          //Debug.Log("[GameController] UpdateScoreBoard() Balls: " + _balls + " Strikes: " + _strikes + " Outs: " + _outs + " runs: " + getTotalRuns(_newTotalBases));

        }


        
        public float TotalRuns(float newRuns) {

            _totalRuns += newRuns;
            return _totalRuns;

        }



        private void SetDeadBall() {
            Debug.Log("[GameController] SetDeadBall()");
            ball.State = GameBallController.BallStateEnum.Dead;
            //ball.transform = Vector3.zero;
            // ball status in UI while in development
            scoreBoardController.SetBallState("BallState: " + ball.State.ToString());

            // set vars
            _newTotalBases = 0;
            _newRuns = 0;

            pitcher.State = VRStickball.PlayerController.PlayerStateEnum.Idle;

            //ResetBall();
        }

        private void ResetBall() {
            Debug.Log("[GameController] ResetBall()");
            ball.ResetBall();

            // ball.transform.position = new Vector3(Random.Range(0f, 0f), 5f, 0f);
        }

        IEnumerator PitchBall() {
            Debug.Log("[GameController] PitchBall()");
            yield return new WaitForSeconds (2.0f);


            while (_outs < 3 && _totalRuns < 2) {
                Debug.Log("[GameController] PitchBall() -->");
                // Control Pitcher Animation
             // var ball = Instantiate (gameBall, transform.position, transform.rotation);

               // ball.transform.position = new Vector3(Random.Range(-3.5f, -3.5f), 5f, 0f);
                ball.transform.position = new Vector3(-3.5f, 1f, 0f);
                //ball.transform.position = new Vector3( -1f, 5f, 0f);
                ball.State = GameBallController.BallStateEnum.Live;
                scoreBoardController.SetBallState("BallState: " + ball.State.ToString());

                pitcher.State = VRStickball.PlayerController.PlayerStateEnum.Lumbering;

                yield return new WaitForSeconds (3.0f);
                ResetBall();
                 yield return new WaitForSeconds (3.0f);

            }


            yield return new WaitForSeconds (2.0f);
            GameOver();

        }
 

        private void GameOver() {
            Debug.Log("[GameController] GameOver()");
            ball.State = GameBallController.BallStateEnum.Dead;
            State = GameStateEnum.GameOver;
            scoreBoardController.SetGameState("GameState: " + _state.ToString());
            pitcher.State = VRStickball.PlayerController.PlayerStateEnum.Idle;



        }




    }


}

