/* CUSTOM EXTENSION METHODS FOR EASY USE */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ExtensionMethods
{
	

	public static void SetScale(this GameObject obj,float scaleValue){
		obj.transform.localScale = new Vector3 (scaleValue, scaleValue, scaleValue);
	}

	public static void SetScale(this GameObject obj,float scaleValueX,float scaleValueY,float scaleValueZ){
		obj.transform.localScale = new Vector3 (scaleValueX, scaleValueY, scaleValueZ);
	}
	public static void SetScale(this GameObject obj,GameObject targetObj){
		obj.transform.localScale = targetObj.transform.localScale;
	}


	//MOVEMENT METHODS
	public static void moveTo(this GameObject obj,GameObject startPosObj,GameObject endPosObj,float time,GameObject ScriptRef,string onComplete){
		obj.SetActive (true);
		obj.transform.position = startPosObj.transform.position;
		iTween.MoveTo(obj,iTween.Hash("position",endPosObj.transform.position,"easeType",iTween.EaseType.linear,"time",time,"onComplete",onComplete,"onCompleteTarget",ScriptRef));
	}
	public static void moveTo(this GameObject obj,GameObject startPosObj,GameObject endPosObj,float time,GameObject ScriptRef,string onComplete,int paramInt){
		obj.SetActive (true);
		obj.transform.position = startPosObj.transform.position;
		iTween.MoveTo(obj,iTween.Hash("position",endPosObj.transform.position,"easeType",iTween.EaseType.linear,"time",time,"onComplete",onComplete,"onCompleteTarget",ScriptRef, "oncompleteparams", paramInt));
	}
	public static void moveTo(this GameObject obj,GameObject startPosObj,Vector3 Position,float time){
		obj.transform.position = startPosObj.transform.position;
		iTween.MoveTo(obj,iTween.Hash("position",Position,"easeType",iTween.EaseType.linear,"time",time));
	}
	public static void moveTo(this GameObject obj,GameObject startPosObj,float addX,float addY,float time){
		obj.transform.position = startPosObj.transform.position;
		float x = 0;
		float y = 0;
		float z = 0;
		x = obj.transform.position.x + addX;
		y = obj.transform.position.y + addY;
		//z = obj.transform.position.z + addZ;

		Vector3 position = new Vector3 (x, y, z);
		iTween.MoveTo(obj,iTween.Hash("position",position,"easeType",iTween.EaseType.linear,"time",time));
	}


	public static void moveTo(this GameObject obj,GameObject startPosObj,Vector3 Position,float time,GameObject ScriptRef,string onComplete){
		obj.transform.position = startPosObj.transform.position;
		iTween.MoveTo(obj,iTween.Hash("position",Position,"easeType",iTween.EaseType.linear,"time",time,"onComplete",onComplete,"onCompleteTarget",ScriptRef));
	}

	public static void MoveToSpring (this GameObject obj, GameObject StartPosition, GameObject EndPosition, float Duration)
	{
		obj.transform.position = StartPosition.transform.position;
		iTween.MoveTo (obj, iTween.Hash ("position", EndPosition.transform.position, "easeType", iTween.EaseType.spring, "time", Duration));
	}

	public static void MoveToSpring (this GameObject obj, Vector3 StartPosition, Vector3 EndPosition, float Duration)
	{
		obj.transform.position = StartPosition;
		iTween.MoveTo (obj, iTween.Hash ("position", EndPosition, "easeType", iTween.EaseType.spring, "time", Duration));
	}



	//MOVEMENT METHODS
	public static void MoveTo (this GameObject obj, GameObject StartPosition, GameObject EndPosition, float Duration)
	{
		obj.transform.position = StartPosition.transform.position;
		iTween.MoveTo (obj, iTween.Hash ("position", EndPosition.transform.position, "easeType", iTween.EaseType.linear, "time", Duration));
	}

	public static void MoveTo (this GameObject obj, Vector3 StartPosition, Vector3 EndPosition, float Duration)
	{
		obj.transform.position = StartPosition;
		iTween.MoveTo (obj, iTween.Hash ("position", EndPosition, "easeType", iTween.EaseType.linear, "time", Duration));
	}

	//ROTATION METHODS
	public static void RotateToZ (this GameObject obj, float EndAngle, float Speed)
	{
		iTween.RotateTo (obj, iTween.Hash ("easeType", iTween.EaseType.linear, "time", Speed, "z", EndAngle));
	}


	//SCALING METHODS
	public static void ScaleTo (this GameObject obj, GameObject StartPosition, GameObject EndPosition, float Duration)
	{
		obj.transform.localScale = StartPosition.transform.localScale;
		iTween.ScaleTo (obj, iTween.Hash ("scale", EndPosition.transform.localScale, "easeType", iTween.EaseType.linear, "time", Duration));
	}
	public static void scaleTo (this GameObject obj,float x,float y,float z,float time,GameObject ScriptRef,string onComplete,int paramInt){
		iTween.ScaleTo (obj, iTween.Hash ("scale", new Vector3 (x, y, z), "easeType", iTween.EaseType.linear, "time", time, "onComplete", onComplete, "onCompleteTarget", ScriptRef, "oncompleteparams", paramInt));
	}

	public static void scaleTo (this GameObject obj,float x,float y,float z,float time,GameObject ScriptRef,string onComplete,GameObject paramInt){
		iTween.ScaleTo (obj, iTween.Hash ("scale", new Vector3 (x, y, z), "easeType", iTween.EaseType.linear, "time", time, "onComplete", onComplete, "onCompleteTarget", ScriptRef, "oncompleteparams", paramInt));
	}


	//POPUP
	public static void Popup (this GameObject obj, float StartScale, float EndScale, float Duration)
	{
		obj.SetActive (true);
		obj.transform.localScale = new Vector3 (StartScale, StartScale, StartScale);
		iTween.ScaleTo (obj, iTween.Hash ("x", EndScale, "y", EndScale, "easeType", iTween.EaseType.spring, "time", Duration));
	}

	//POPUP Gameobject
	public static void Popup (this GameObject obj, float StartScale, float EndScale, float Duration,GameObject OnCompleteTarget,string OnComplete)
	{
		obj.SetActive (true);
		obj.transform.localScale = new Vector3 (StartScale, StartScale, StartScale);
		iTween.ScaleTo (obj, iTween.Hash ("x", EndScale, "y", EndScale, "easeType", iTween.EaseType.spring, "time", Duration,OnCompleteTarget,"OnComplete"));
	}


	//ALPHA METHODS
	public static void SetAlpha (this GameObject obj, float AlphaValue)
	{
		obj.GetComponent<CanvasGroup> ().alpha = AlphaValue;
	}

	public static void SetAlpha (this GameObject[] obj, float AlphaValue)
	{
		for (int i = 0; i < obj.Length; i++) {
			obj [i].GetComponent<CanvasGroup> ().alpha = AlphaValue;
		}
	}

	public static float GetAlpha (this GameObject obj)
	{
		return obj.GetComponent<CanvasGroup> ().alpha;
	}

	public static float GetAlpha (this GameObject[] obj)
	{
		float a = 0;
		for (int i = 0; i < obj.Length; i++) {
			a += obj [i].GetComponent<CanvasGroup> ().alpha;
		}
		return a;
	}


	//POSITION METHODS

	public static float GetDistance (this GameObject obj, GameObject Target)
	{
		float d = Vector2.Distance (obj.transform.position, Target.transform.position);
		return d;
	}

	public static void SetPosition (this GameObject obj, GameObject TargetPosObj)
	{
		obj.transform.position = TargetPosObj.transform.position;
	}

	public static void SetPosition (this GameObject obj, Vector3 TargetPosition)
	{
		obj.transform.position = TargetPosition;
	}

	public static Vector3 GetPosition (this GameObject obj)
	{
		return obj.transform.position;
	}

	public static Vector3 GetLocalPosition (this GameObject obj)
	{
		return obj.transform.localPosition;
	}

	//	public static void ResetPosition (this GameObject obj, Vector3 Pos, float time)
	//	{
	//		obj.moveTo (obj.GetPosition (), Pos, time);
	//	}

	public static Vector3 GetScale (this GameObject obj)
	{
		return obj.transform.localScale;
	}

	public static Vector3 GetScale (this GameObject[] obj)
	{
		Vector3 Scale = Vector3.zero;
		for (int i = 0; i < obj.Length; i++) {
			Scale += obj [i].transform.localScale;
		}
		return Scale;
	}


	//SET AND GET SPRITES
	public static void SetSprite (this GameObject obj, Sprite Sprite, bool Setnative = true)
	{
		Image Img = obj.GetComponent<Image> ();
		Img.sprite = Sprite;
		if (Setnative) {
			Img.SetNativeSize ();
		}
	}


	//ANIMATION METHODS
	public static void Play (this GameObject obj, string ClipName)
	{
		if (obj.GetComponent<Animator> () != null) {
			obj.GetComponent<Animator> ().Play (ClipName, 0, 0f);
		}
	}

	public static void FadeTo (this GameObject obj, float StartValue, float EndValue, float time)
	{
		obj.GetComponent<CanvasRenderer> ().SetAlpha (StartValue);
		obj.GetComponent<Image> ().CrossFadeAlpha (EndValue, time, false);
	}


		public static int Length(this GameObject obj){
		return obj.transform.childCount;
	}

	//SCALING METHODS
//	public static void ScaleTo (this GameObject obj, GameObject StartPosition, GameObject EndPosition, float Duration)
//	{
//		obj.transform.localScale = StartPosition.transform.localScale;
//		iTween.ScaleTo (obj, iTween.Hash ("scale", EndPosition.transform.localScale, "easeType", iTween.EaseType.linear, "time", Duration));
//	}
}
