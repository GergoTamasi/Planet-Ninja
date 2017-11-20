using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{

	public MeshRenderer mr;


	public void Awake()
	{
		
		//FadeOut();
		StartCoroutine(Colorize());
	}
	
	public void FadeOut()
	{
		for (float r = 1; r >= 0; r--)
		{
			mr.material.color= new Color(r,mr.material.color.b,mr.material.color.g);
		}	
		
	}

	private IEnumerator Colorize()
	{
		for (float r = 1; r >= 0; r=r-0.1f)
		{
			mr.material.color= new Color(r,mr.material.color.b,mr.material.color.g);
			yield return new WaitForSeconds(0.5f);
		}
		
	}
	
	
}
