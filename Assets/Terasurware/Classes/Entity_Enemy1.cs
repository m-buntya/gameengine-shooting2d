using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity_Enemy1 : ScriptableObject
{	
	public List<Sheet> sheets = new List<Sheet> ();

	[System.SerializableAttribute]
	public class Sheet
	{
		public string name = string.Empty;
		public List<Param> list = new List<Param>();
	}

	[System.SerializableAttribute]
	public class Param
	{
		
		public double No;
		public string Key;
		public string Nama_JP;
		public double HP;
		public double ATK;
		public double SpeedX;
		public double SpeedY;
		public double BulletSpeed;
		public double fireRate;
        public bool BOSS;
	}
}

