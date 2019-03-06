using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRStickball {

	[RequireComponent (typeof (Animation))]

	public class PlayerController : MonoBehaviour {

		public delegate void PlayerAnimationFinished(string animation);
		public PlayerAnimationFinished OnPlayerAnimationFinished = null;

		private Vector3 _shotPosition = Vector3.zero;

		public Vector3 ShotPosition {
			get {
				return _shotPosition;
			}

			set {
				_shotPosition = value;
			}
		}

		public enum PlayerStateEnum {
			Idle,
			Walk,
			Lumbering
		}

		// initial player state
		private PlayerStateEnum _state = PlayerStateEnum.Idle;

		// Current Animation
		private AnimationClip _currentAnimation = null;

		// Animations
		public AnimationClip animIdle; 
		public AnimationClip animSwing; 
		public AnimationClip animWalk; 

		// elapsed time of animation
		private float _elapsedStateTime = 0.0f;
		//player character physics properties
		private Transform _transform;
		private Animation _animation;
		private CapsuleCollider _collider;


		// dont need [holding ball]
		private bool _holdingBall = false;


		void Awake () {
			// set physics props
			_transform = GetComponent<Transform>();
			_collider = GetComponent<CapsuleCollider>();
			_animation = GetComponent<Animation>();

			Debug.Log("[PlayerController] Awake() _animation: " + _animation);

			
		}
		
		
		// Use this for initialization
		void Start () {
			
			InitAnimations();
			//_currentAnimation = animIdle;
			
		}
		
		// Update is called once per frame
		void Update () {

		}

		public bool IsHoldingBall {
			get {
				return _holdingBall;
			}
		}

		public PlayerStateEnum State {

			get {
				return _state;
			}

			set {
				CancelInvoke("OnAnimationFinished");

				_state = value;
				_elapsedStateTime = 0.0f;

				switch( _state ){
					case PlayerStateEnum.Idle:
					SetCurrentAnimation( animIdle ); 				
					break;

					case PlayerStateEnum.Walk:
					SetCurrentAnimation( animWalk ); 				
					break;

					case PlayerStateEnum.Lumbering:
					SetCurrentAnimation( animSwing ); 				
					break;

				}

			}
		}

		public float ElapsedStateTime {
			get {
				return _elapsedStateTime;
			}
		}

		private void InitAnimations() {
			//_animation.Stop();
			_animation [animIdle.name].wrapMode = WrapMode.Loop;
			_animation [animWalk.name].wrapMode = WrapMode.Loop;
			_animation [animSwing.name].wrapMode = WrapMode.Once;

		}

		public bool IsAnimating {
			get {
				return _animation.isPlaying;
			}
		}

		public AnimationClip CurrentAnimation {
			get {return _currentAnimation; }
			set {SetCurrentAnimation(value); }
		}

		public void SetCurrentAnimation(AnimationClip animationClip) {

			_currentAnimation = animationClip;	
			Debug.Log("[PlayerController] SetCurrentAnimation _animation: " + _currentAnimation.name );
			if(_animation != null) {
				_animation [_currentAnimation.name].time = 0.0f; 
				_animation.CrossFade(_currentAnimation.name, 0.1f);
				Debug.Log("[PlayerController] SetCurrentAnimation _currentAnimaton.name: " + _currentAnimation.name );

			}
			
			if (_currentAnimation.wrapMode != WrapMode.Loop) {
        		Invoke ("OnAnimationFinished", _animation [_currentAnimation.name].length / _animation [_currentAnimation.name].speed);
    		}

		}

		

	}
	
}
