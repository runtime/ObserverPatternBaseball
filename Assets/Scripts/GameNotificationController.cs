using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VRStickball {

	public class GameNotificationController : MonoBehaviour {

		public GameObject notifcationOut;

		// Use this for initialization
		void Start () {

			notifcationOut.SetActive (false);
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		public void displayOut () {
			// set out to visible
			notifcationOut.SetActive (true);
		}

		public void clearNotifications() {
			Debug.Log("[gameNotificationController] clearNotifications ");
			notifcationOut.SetActive (false);
		}
	}

}

