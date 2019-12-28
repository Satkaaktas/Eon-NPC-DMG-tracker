using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSquare : MonoBehaviour
{

	#region Variables

	[SerializeField]
	private GameObject spot;

	private Spot[,] spots;
	private int columns = 7;
	private int rows = 10;
	private int damage = 0;
	private float spriteHeight = 100;
	private float spriteWidth = 100;
	private float height;
	private float width;
	#endregion

	#region Properties

	public int damageColumns
	{
		get { return columns; }
		set { columns = value; ChangeColumns(); }
	}

	public int damageTaken
	{
		get { return damage; }
		set { damage = value; UpdateDamage(); }
	}
	#endregion

	#region Private Methods

	private void ChangeColumns()
	{
		ClearSpots();
		for (int i = 0; i < spots.GetLength(0); i++)
		{
			for (int j = columns; j < spots.GetLength(1); j++)
			{
				spots[i, j].spotfill = SpotFill.Fully;
			}
		}
		UpdateDamage();
	}

	private void UpdateDamage()
	{
		//int cap = damage <= columns * spots.GetLength(0) ? damage : columns * spots.GetLength(0);
		int cap = columns * spots.GetLength(0);

		for (int i = 0; i < cap; i++)
		{
			if (i < damage)
			{
				spots[i / (columns), i % (columns)].spotfill = SpotFill.Fully;
			}
			else
			{
				spots[i / (columns), i % (columns)].spotfill = SpotFill.Empty;
			}
		}
	}

	private void ClearSpots()
	{
		foreach (Spot spot in spots)
		{
			spot.spotfill = SpotFill.Empty;
		}
	}
	private void Awake()
	{
		spots = new Spot[rows, 10];

		spriteHeight *= spot.transform.localScale.y;
		spriteWidth *= spot.transform.localScale.x;
		height = spriteHeight * spots.GetLength(0);
		width = spriteWidth * spots.GetLength(1);

		for (int i = 0; i < spots.GetLength(0); i++)
		{
			for (int j = 0; j < spots.GetLength(1); j++)
			{
				spots[i, j] = Instantiate(spot).GetComponent<Spot>();
				spots[i, j].transform.parent = transform;
				spots[i, j].transform.SetAsFirstSibling();
				//Setting position
				spots[i, j].transform.localPosition = Vector3.up * height * 0.5f - (Vector3.up * spriteHeight * i);
				spots[i, j].transform.localPosition += (Vector3.right * spriteWidth * j) - Vector3.right * width * 0.5f;
			}
		}
	}

	#endregion


}
