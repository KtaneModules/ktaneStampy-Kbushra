using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;

public class SimpleModuleScript : MonoBehaviour 
{
	public KMSelectable Cat;
	public KMNeedyModule module;
	public KMAudio audio;
	public KMBombInfo info;

	static int ModuleIdCounter;
	int ModuleId;

	void Awake()
	{
		ModuleId = ModuleIdCounter++;

		GetComponent<KMNeedyModule>().OnNeedyActivation += OnNeedyActivation;
		GetComponent<KMNeedyModule>().OnNeedyDeactivation += OnNeedyDeactivation;
		Cat.OnInteract += AddTime;
		GetComponent<KMNeedyModule>().OnTimerExpired += OnTimerExpired;
	}

	protected void OnNeedyActivation()
	{
		Log ("Kitty Kat wants a pet!");
	}

	protected void OnNeedyDeactivation()
	{
		Log ("Purrr (:<");
	}

	protected void OnTimerExpired()
	{
		module.HandleStrike ();
		module.HandlePass ();
		Log ("Disgrace to cats!");
	}

	protected bool AddTime()
	{
		float time = GetComponent<KMNeedyModule>().GetNeedyTimeRemaining();
		if (time > 10)
		{
			GetComponent<KMNeedyModule>().SetNeedyTimeRemaining(time + 2f);
		}
		if (time < 11) 
		{
			Invoke ("OnTimerExpired", 0);
		}

		return false;
	}

	void Update()
	{
		float time = GetComponent<KMNeedyModule>().GetNeedyTimeRemaining();

		if (time > 99) 
		{
			module.HandlePass ();
		}
	}

	void Log(string message)
	{
		Debug.LogFormat("[The Stampycat #{0}] {1}", ModuleId, message);
	}
}
