using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRStickball {
	
		public class ScoreboardController : MonoBehaviour {

			public TextMesh gameStateTextMesh;
			public TextMesh ballStateTextMesh;
			public TextMesh ballsTextMesh;
			public TextMesh strikesTextMesh;
			public TextMesh outsTextMesh;
			
			public TextMesh runsTextMesh;

			public GameObject indicator_1b;
			public GameObject indicator_2b;
			public GameObject indicator_3b;
		
		
		void Awake() {
			//Debug.Log("[ScoreboardController] SetBallState()");
		}
		
		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}
		public void SetGameState(string gamestate) {
			gameStateTextMesh.text = gamestate;
			Debug.Log("[ScoreboardController] SetGameState()");
		}

		public void SetBallState(string ballstate) {
			ballStateTextMesh.text = ballstate;
			//Debug.Log("[ScoreboardController] SetBallState()");
		}

		public void SetRuns(string runs) {
			runsTextMesh.text = runs;
		}

		public void SetBalls(string balls) {
			ballsTextMesh.text = balls;
		}

		public void SetStrikes(string strikes) {
			strikesTextMesh.text = strikes;
		}
		public void SetOuts(string outs) {
			outsTextMesh.text = outs;
		}

		public void SetTB(bool[] bases) {
			Debug.Log("[ScoreboardController] SetTB() bases: " + bases[0] + " " + bases[1] + " " + bases[2] );
			if (bases[0]==true) {
			  indicator_1b.GetComponent<Renderer> ().material.color = Color.green;
			}
			
			if (bases[1]==true)  {
				indicator_2b.GetComponent<Renderer> ().material.color = Color.green;
			}
			
			 if (bases[2]==true) {
				indicator_3b.GetComponent<Renderer> ().material.color = Color.green;
			}
		}
	}
}
