using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lv1 : ScriptableObject
{	
	public List<Param> param = new List<Param> ();

	[System.SerializableAttribute]
	public class Param
	{
		
		public string tuujou;
		public string kusukusu;
		public string kantan;
		public string komaru;
		public string ureshii;
		public string sagesumi;
	}
}