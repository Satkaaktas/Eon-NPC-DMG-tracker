using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC
{
	private int[] damage;
	private int[] partialDamage;
	private int bleeding, columns, exhaustionColumns;
	private string myName;

	#region Properties

	public int[] damageCrosses
	{
		get { return damage; }
	}
	public int[] damagePartial
	{
		get { return partialDamage; }
	}
	public int bleedingCrosses
	{
		get { return bleeding; }
	}
	public int numberOfColumns
	{
		get { return columns; }
	}
	public int exColumns
	{
		get { return exhaustionColumns; }
	}
	public string name
	{
		get { return myName; }
	}

	#endregion

	public NPC(int dmgColumns, int exhaustionColumns, string name)
	{
		this.columns = dmgColumns;
		this.exhaustionColumns = exhaustionColumns;
		this.myName = name;
		damage = new int[4];
		partialDamage = new int[4];
	}

	public void Damage(int damage, int selectedSquare)
	{
		if (selectedSquare != -1)
		{
			damage = this.damage[selectedSquare] + damage >= 0 ? damage : -this.damage[selectedSquare];
			this.damage[selectedSquare] += damage;
		}
		else
		{
			damage = bleeding + damage >= 0 ? damage : -bleeding;
			bleeding += damage;
		}
	}

	public void PartialDamage()
	{
		Debug.Log("TODO");
	}

}
