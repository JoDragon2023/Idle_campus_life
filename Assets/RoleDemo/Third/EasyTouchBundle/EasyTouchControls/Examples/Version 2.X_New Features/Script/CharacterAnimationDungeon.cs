﻿using UnityEngine;
using System.Collections;

public class CharacterAnimationDungeon : MonoBehaviour {

	private CharacterController cc;
	private Animation anim;
	
	// Use this for initialization
	void Start () {
		
		cc= GetComponentInChildren<CharacterController>();
		anim = GetComponentInChildren<Animation>();
	}
	
	
	// Wait end of frame to manage charactercontroller, because gravity is managed by virtual controller
	void LateUpdate(){
		if (cc.isGrounded && (ETCInput.GetAxis("Vertical")!=0 || ETCInput.GetAxis("Horizontal")!=0)){
			//anim.CrossFade("Walk_02");
		}
		
		if (cc.isGrounded && ETCInput.GetAxis("Vertical") == 0 && ETCInput.GetAxis("Horizontal")==0){
			//anim.CrossFade("Stand");
			//Debug.Log(" soldierIdleRelaxed ");
		}
		
		// if (!cc.isGrounded){
		// 	anim.CrossFade("soldierFalling");
		// 	
		// 	Debug.Log(" soldierFalling ");
		// }

	}
}
