using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
	//This is an altered Camera2DFollow class from the Unity 2D Standard Assets
    public class Camera2DFollow : MonoBehaviour
    {
		#region Variables
		//Variables for city 2 level
		private float horizontalCamStop;
		private float verticalCamStopBottom = 0;

		//Look ahead variables
		public Transform bunny;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;
        private float zOffset;
        private Vector3 previousBunnyPos;
        private Vector3 currentVelocity;
        private Vector3 lookAheadPos;
		#endregion

		#region Start
        private void Start() {

			//Set camStop for second city level
			if (Application.loadedLevel == 9) {
				horizontalCamStop = -100f;
			} else {
				horizontalCamStop = -100;
			}

			//Get previous bunny position at level start
			previousBunnyPos = bunny.position;

			//Get the z offset (z distance from bunny to camera)
			zOffset = (transform.position - bunny.position).z;
        }
		#endregion

		#region Update
        private void Update() {
            
			if (bunny != null) {

				//Get the difference of current to previous x position
				float xMoveDelta = (bunny.position - previousBunnyPos).x;

				//Only update lookahead position, if accelerating or changed direction
				bool updateLookAheadTarget = Mathf.Abs (xMoveDelta) > lookAheadMoveThreshold;
				if (updateLookAheadTarget) {
					lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign (xMoveDelta);
				} else {
					lookAheadPos = Vector3.MoveTowards (lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
				}

				//Get look ahead position
				Vector3 aheadTargetPos = bunny.position + lookAheadPos + Vector3.forward * zOffset;

				//Dampen the camera adjustment
				Vector3 newPos = Vector3.SmoothDamp (transform.position, aheadTargetPos, ref currentVelocity, damping);

				//Camera height is bunny height
				newPos.y = bunny.position.y;

				//Clamp camera at ground level
				if (newPos.y <= verticalCamStopBottom) {
					newPos.y = verticalCamStopBottom;
				}

				//Apply position as long as camStop hasn't been reached
				if (transform.position.x >= horizontalCamStop) {
					transform.position = newPos;
				}

				//Get previous bunny position for next frame
				previousBunnyPos = bunny.position;
			}
        }
		#endregion
    }
}
