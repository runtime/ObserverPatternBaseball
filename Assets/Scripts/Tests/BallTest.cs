using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// STUB GOES IN GAMECONTROLLER LATER
namespace VRStickball{

	[RequireComponent(typeof(GameBallController))]

	public class BallTest : MonoBehaviour {
		public GameBallController ball;

		// Use this for initialization
		void Start () {
			
			ball.OnStrike += Handle_OnStrike;
			//Debug.Log("[BallTest] Start()" + ball.OnStrike);
		}
		
		protected void Handle_OnStrike() {
			Debug.Log("[BallTest] Handle_OnStrike() STRIKE !!!");
		}
	}
	
}

