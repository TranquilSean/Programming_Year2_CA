using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingNoise : StateMachineBehaviour
{
	// Create AudioSource array to hold all AudioSource Comnponents
	private AudioSource[] audio;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		audio = animator.GetComponentsInParent<AudioSource>();
		audio[0].Play();
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

		// Stop audio file[0] (walking) 
		audio[0].Stop();
	}
}
