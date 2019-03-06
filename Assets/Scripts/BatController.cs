using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRStickball {

	public class BatController : MonoBehaviour {

		//player character physics properties
		private Transform _transform;
		private Rigidbody _rigidbody;
		private CapsuleCollider _collider;

		public float speed = 90.0f;
		public float direction = 1.0f;
		
		private float posX;
		private float posY;
		private float posZ;
		

		void Awake() {
			_transform = GetComponent<Transform>();
			_rigidbody = GetComponent<Rigidbody>();
			_collider = GetComponent<CapsuleCollider>();
		}

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame

		void FixedUpdate () {

			posX = _rigidbody.transform.position.y;
			posY = _rigidbody.transform.position.y;
			posZ = _rigidbody.transform.position.z;
			//float dir = direction * speed; 
			//Debug.Log("[BatController] FixedUpdate rotation.z  = " + _rigidbody.transform.rotation.z);
			_rigidbody.transform.Rotate (new Vector3(0f, 0f , -800f) * Time.fixedDeltaTime);
			
			//_rigidbody.transform.localRotation *= Quaternion.AngleAxis(speed * Time.deltaTime, new Vector3(0f, 0f, dir));
			// if (_rigidbody.transform.position.y < 0.84f || _rigidbody.transform.position.y > 1.0f) {
			// 		_rigidbody.transform.position = (new Vector3(1.23f, 0.95f, -0.43f));
			// 		_rigidbody.transform.Rotate (new Vector3(0f, 0f , -180f) * Time.fixedDeltaTime);

			// } else if (_rigidbody.transform.rotation.z >  100) {
			// 	direction = direction * -1.0f;
			// }

		}

		void Update () {
			
		}
	}


}
