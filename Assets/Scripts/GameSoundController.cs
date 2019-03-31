using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRStickball {

	public class GameSoundController : MonoBehaviour {

		public AudioClip ambientLoop;
		private AudioSource source;

		void Awake () {
			source = GetComponent<AudioSource> ();
			source.loop = true;
			source.clip = ambientLoop;
		}
		
		// Use this for initialization
		void Start () {
			print ("[GameSoundController] Start]");
			source.Play ();
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		public void playOneShot() {
			print ("[GameSoundController] playOneShot]");
			//source.PlayOneShot (somesound, somevolume);
		}
	}	
	
}
