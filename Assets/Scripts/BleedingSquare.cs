using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedingSquare : MonoBehaviour
{
	#region Variables

	[SerializeField]
	private GameObject spot;

	private Spot[][] spots;
	private int damage = 0;
	private int rows = 10;

	private float spriteHeight = 100;
	private float spriteWidth = 100;
	private float height;
	private float width;
	#endregion

	#region Properties

	public int damageTaken
	{
		get { return damage; }
		set { damage = value; UpdateDamage(); }
	}
	#endregion

	#region Private Methods
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
			damageTaken++;
	}
	private void UpdateDamage()
	{
		
		int cap = damage <= (spots.Length * spots[1].Length) - 1 ? damage : (spots.Length * spots[1].Length-1);


		for (int i = 0; i < cap +1; i++)
		{
			i = i == 9 ? 10 : i;
			spots[i / spots[1].Length][i % spots[1].Length].spotfill = SpotFill.Fully;
		}
	}

	private void ClearSpots()
	{
		for (int i = 0; i < spots.Length; i++)
		{
			for (int j = 0; j < spots[i].Length; j++)
			{
				spots[i][j].spotfill = SpotFill.Empty;
			}
		}
	}
	private void Awake()
	{
		spots = new Spot[rows][];
		spots[0] = new Spot[9];
		for (int i = 1; i < spots.Length; i++)
		{
			spots[i] = new Spot[10];
		}

		spriteHeight *= spot.transform.localScale.y;
		spriteWidth *= spot.transform.localScale.x;
		height = spriteHeight * spots.Length;
		width = spriteWidth * spots[1].Length;

		for (int i = 0; i < spots.Length; i++)
		{
			for (int j = 0; j < spots[i].Length; j++)
			{
				spots[i][j] = Instantiate(spot).GetComponent<Spot>();
				spots[i][j].transform.parent = transform;
				//Setting position
				spots[i][j].transform.localPosition = Vector3.up * height * 0.5f - (Vector3.up * spriteHeight * i);
				spots[i][j].transform.localPosition += (Vector3.right * spriteWidth * j) - Vector3.right * width * 0.5f;
			}
		}
	}

	#endregion
}
