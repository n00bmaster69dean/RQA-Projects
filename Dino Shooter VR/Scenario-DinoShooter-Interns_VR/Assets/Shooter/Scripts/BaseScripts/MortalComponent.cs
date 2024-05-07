using System.Collections;
using System.Collections.Generic;
using LowPolyAnimalPack;
using U4K.BehaviourTemplate;
using U4K.Utils;
using UnityEngine;

namespace U4K.BaseScripts
{
    public class MortalComponent : MonoBehaviour
    {
        private const float REST_SPEED = 0.01f;
        private const float RUNNING_SPEED = 3f;
        private Vector3 lastPosition;
        private Animator mAnimator;
        private float speed;
        private AnimationState mState = AnimationState.idling;
        private Dictionary<AnimationState, bool> capability = new Dictionary<AnimationState, bool>();

        private void Start()
        {
            mAnimator = GetComponent<Animator>();
            lastPosition = transform.position;
            var mainCamera = CommonUtil.GetMainCamera();
            var audioManager = mainCamera.GetComponent<AudioManager>();
            if (audioManager == null)
                audioManager = mainCamera.AddComponent<AudioManager>();
            capability.Add(AnimationState.idling, false);
            capability.Add(AnimationState.isWalking, false);
            capability.Add(AnimationState.isRunning, false);
            capability.Add(AnimationState.isDead, false);
            if (mAnimator != null)
            {
                foreach (var parameter in mAnimator.parameters)
                {
                    if (parameter.name == "isWalking")
                        capability[AnimationState.isWalking] = true;
                    if (parameter.name == "isRunning")
                        capability[AnimationState.isRunning] = true;
                    if (parameter.name == "isDead")
                        capability[AnimationState.isDead] = true;
                }
            }
        }

        public void Die(float delay)
        {
            if (mState != AnimationState.isDead)
            {
                mState = AnimationState.isDead;
                var motions = GetComponents<MotionBehaviourTemplate>();
                foreach (var motion in motions)
                {
                    motion.ToggleActive();
                }

                if (mAnimator != null)
                {
                    SetToState(mState);
                }

                StartCoroutine(Disappear(delay));
            }
        }

        private IEnumerator Disappear(float delay)
        {

            yield return new WaitForSeconds(delay);
            gameObject.SetActive(false);
        }

        private void LateUpdate()
        {
            speed = (transform.position - lastPosition).magnitude / Time.deltaTime;
            if (speed > RUNNING_SPEED) mState = AnimationState.isRunning;
            else if (speed > REST_SPEED) mState = AnimationState.isWalking;
            else mState = AnimationState.idling;
            SetToState(mState);
            lastPosition = transform.position;
        }

        private void SetToState(AnimationState state)
        {
            if (mAnimator == null) return;
            switch (state)
            {
                case AnimationState.idling:
                    if (capability[AnimationState.isWalking]) mAnimator.SetBool("isWalking", false);
                    if (capability[AnimationState.isRunning]) mAnimator.SetBool("isRunning", false);
                    return;
                case AnimationState.isWalking:
                    if (capability[AnimationState.isWalking]) mAnimator.SetBool("isWalking", true);
                    if (capability[AnimationState.isRunning]) mAnimator.SetBool("isRunning", false);
                    return;
                case AnimationState.isRunning:
                    if (capability[AnimationState.isWalking]) mAnimator.SetBool("isWalking", false);
                    if (capability[AnimationState.isRunning]) mAnimator.SetBool("isRunning", true);
                    else if (capability[AnimationState.isWalking]) mAnimator.SetBool("isWalking", true);
                    break;
                case AnimationState.isDead:
                    if (capability[AnimationState.isWalking]) mAnimator.SetBool("isWalking", false);
                    if (capability[AnimationState.isRunning]) mAnimator.SetBool("isRunning", false);
                    if (capability[AnimationState.isDead]) mAnimator.SetBool("isDead", true);
                    break;
            }
        }

        public enum AnimationState
        {
            idling,
            isWalking,
            isRunning,
            isDead
        }
    }
}