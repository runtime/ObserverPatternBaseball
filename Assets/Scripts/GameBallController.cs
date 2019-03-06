using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace VRStickball {

	//[RequireCompoenent (typeof(SphereCollider))]
	//[RequireCompoenent (typeof(Rigidbody))]
	
	public class GameBallController : MonoBehaviour {


		//physics
		private Transform _transform;
		private Rigidbody _rigidbody;
		private SphereCollider _sphereCollider;

		private GameObject currentCollider;
		private Vector3 contactNormal;
		private float contactMagnitude;

		//physical location
		private Vector3 _ballPosition;

		private Vector3 _ballRotation;


		//Delegates
		//Strike
		public delegate void Strike();
		// should be null but i changed it because it wasn't firing
		public Strike OnStrike;
		//Ball
		public delegate void Ball();
		public Ball OnBall;
		//Single
		public delegate void Single();
		public Single OnSingle;
		//Double
		public delegate void Double();
		public Double OnDouble;
		//Triple
		public delegate void Triple();
		public Triple OnTriple;
		//HomeRun
		public delegate void HomeRun();
		public HomeRun OnHomeRun;
		//HomeRun
		public delegate void FoulBall();
		public FoulBall OnFoulBall;

		public delegate void Out();
		public Out OnOut;

		
		// Game Controller Ref
		private GameController _gameController;

		// local variables
		private int _totalBases;

		public enum BallStateEnum {
			Live,
			Batted,
			Dead
		}

		// initial ball state
		private BallStateEnum _state = BallStateEnum.Dead;

		void Awake() {
			_transform = GetComponent<Transform>();
			_rigidbody = GetComponent<Rigidbody>();
			_sphereCollider = GetComponent<SphereCollider>();
		}

		// Use this for initialization
		void Start () {
			// get singleton of game controller
			_gameController = GameController.SharedInstance;
			// set the balls position
			//_ballPosition = new Vector3(Random.Range(-3.5f, 3.5f), 1f, 0f);
			_ballPosition = new Vector3(-3.5f, .25f, 0f);
			_ballRotation = new Vector3(0f, 0f, 0f);

			_transform.position = _ballPosition;
			_transform.Rotate (_ballRotation);

		}
		
		// Update is called once per frame
		void Update () {

			
			if(_gameController.State == VRStickball.GameController.GameStateEnum.Paused) {
				_rigidbody.useGravity = false;
			//	_rigidbody.isKinematic = false;
			} else {
				//Debug.Log("[GameBallController] Update() else ");
				_rigidbody.useGravity = true;
				//_rigidbody.isKinematic = false;
			}

			


			
		}

		void FixedUpdate () {

			if (_state == BallStateEnum.Live) {

			float speed = 10f;
			float step = speed * Time.fixedDeltaTime;
		//	_rigidbody.AddForce (new Vector3(Random.Range(.3f, 1.8f), Random.Range(.6f, 2.0f), Random.Range(-.8f, .8f)) * step, ForceMode.Impulse);
			_rigidbody.AddForce (new Vector3(6f, .9f, 0f) * step, ForceMode.Impulse);

			_rigidbody.transform.Rotate (new Vector3(25f, 25f , -55f) * Time.fixedDeltaTime);

			} else if (_state == BallStateEnum.Dead || _state == BallStateEnum.Batted) {
				//Debug.Log("[GameBallController] FixedUpdate() Else if BallStateEnum.Dead or Batted ");

				//Vector3 direction = contactNormal * contactMagnitude;
				//_rigidbody.AddForce (direction, ForceMode.VelocityChange);	
				//_rigidbody.transform.Rotate (direction);

				Vector3 direction = Vector3.zero;
				_rigidbody.AddForce (direction, ForceMode.VelocityChange);	
				_rigidbody.AddTorque (direction);
				_rigidbody.transform.Rotate (direction);

				//_rigidbody.useGravity = false;
				//Vector3 direction = Vector3.zero;
				//_rigidbody.velocity = direction;
				//_rigidbody.transform.Rotate (direction) = ;

			}
		
		}

		public Transform BallTransform {
			get {
				return _transform;
			}
		}

		public Rigidbody BallRigidbody {
			get {
				return _rigidbody;
			}
		}

		public SphereCollider BallColider {
			get {
				return _sphereCollider;
			}
		}

		public BallStateEnum State {
			get {
				return _state;
			}
			set {
				_state = value;
			}
		}


		public void OnCollisionEnter (Collision collision) {
			//Debug.Log("[GameBallController]: OnCollisionEnter");
			_gameController.OnBallCollisionEnter (collision);
		}

		public void OnTriggerEnter (Collider collider) {

			Debug.Log("STATE:: " + _state);

			// SET Batted State if Collision with Bat
			if (collider.transform.name.Equals ("Bat") && (_state != BallStateEnum.Dead)) {
				State = BallStateEnum.Batted;
			}

			// != Batted Collisons
			if(collider.transform.name.Equals ("StrikeZone") && (_state != BallStateEnum.Batted)) {

				Debug.Log("[GameBallController]: OnTriggerEnter: " + collider.transform.name );
				if(OnStrike != null && _state.ToString() != "Dead") {

					OnStrike();	
				}
			} else if (collider.transform.name.Equals ("BallZone") && (_state != BallStateEnum.Batted)) {

				Debug.Log("[GameBallController]: OnTriggerEnter: " + collider.transform.name );

				if(OnBall != null && _state.ToString() != "Dead") {
				
					OnBall();
			
				}

			} else if (collider.transform.name.Equals ("Ground") && (_state != BallStateEnum.Batted))  {

				Debug.Log("[GameBallController]: OnTriggerEnter: " + collider.transform.name );

				if(OnBall != null && _state.ToString() != "Dead") {
					OnBall();
				}
			}

			// == Batted Collisions
			else if (collider.transform.name.Equals ("Ground") && (_state == BallStateEnum.Batted)) {
				if(OnOut != null && _state.ToString() != "Dead") {
					OnOut();
				}
			}

			else if (collider.transform.name.Equals ("BallZone") && (_state == BallStateEnum.Batted)) {
				if(OnStrike != null && _state.ToString() != "Dead") {
					OnStrike();
				}
			}

			else if (collider.transform.name.Equals ("StrikeZone") && (_state == BallStateEnum.Batted)) {
				if(OnStrike != null && _state.ToString() != "Dead") {
					OnStrike();
				}
			}

			else if (collider.transform.name.Equals ("FoulZoneL") && (_state == BallStateEnum.Batted) ) {
				if (OnFoulBall != null && _state.ToString() != "Dead") {
					OnFoulBall();
				}
			}

			else if (collider.transform.name.Equals ("FoulZoneR") && (_state == BallStateEnum.Batted) ) {
				if (OnFoulBall != null && _state.ToString() != "Dead") {
					OnFoulBall();
				}
			}

			else if (collider.transform.name.Equals ("SingleZone") && (_state == BallStateEnum.Batted) ) {
				if (OnSingle != null && _state.ToString() != "Dead") {
					OnSingle();
				}
			}

			else if (collider.transform.name.Equals ("DoubleZone") && (_state == BallStateEnum.Batted) ) {
				if (OnDouble != null && _state.ToString() != "Dead") {
					OnDouble();
				}
			}

			else if (collider.transform.name.Equals ("TripleZone") && (_state == BallStateEnum.Batted) ) {
				if (OnTriple != null && _state.ToString() != "Dead") {
					OnTriple();
				}
			}

			else if (collider.transform.name.Equals ("HomeRuneZone") && (_state== BallStateEnum.Batted) ) {
				if (OnHomeRun != null && _state.ToString() != "Dead") {
					OnHomeRun();
				}
			}


		}

		public void ResetBall() {
			Debug.Log("[GameBallController]: ResetBall()");

			
			_rigidbody.velocity = Vector3.zero;
			_rigidbody.angularVelocity = Vector3.zero;

			_rigidbody.isKinematic = true;

			//_rigidbody.position = areaStartNode.position;
			_rigidbody.rotation = Quaternion.identity;

			_rigidbody.isKinematic = false;

		}

		// void OnCollisionExit(Collision col) {
		// 	currentCollider = col.gameObject;
		// 	contactMagnitude = col.relativeVelocity.magnitude;
		// 	foreach (ContactPoint contact in col.contacts) {
		// 		contactNormal = contact.normal;
				
		// 	}
		// }


	}

}

