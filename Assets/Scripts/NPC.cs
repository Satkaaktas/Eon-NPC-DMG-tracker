using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC
{
	private int[] damage;
	private int[] partialDamage;
	private int bleeding, columns, exhaustionColumns;

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

	#endregion

	public NPC(int dmgColumns, int exhaustionColumns)
	{
		this.columns = dmgColumns;
		this.exhaustionColumns = exhaustionColumns;
		damage = new int[4];
		partialDamage = new int[4];
	}

	public void Damage(int damage, int selectedSquare)
	{
		if (selectedSquare != -1)
			this.damage[selectedSquare] += damage;
		else
			bleeding += damage;
	}

	public void PartialDamage()
	{
		Debug.Log("TODO");
	}

}
