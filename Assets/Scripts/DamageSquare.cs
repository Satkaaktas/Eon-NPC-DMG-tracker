using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSquare : MonoBehaviour
{
	[SerializeField]
	private GameObject spot;
	
	private Spot[,] spots;
	private int columns = 10;
	private int damage;
	private float spriteHeight = 100;
	private float spriteWidth = 100;
	private float height;
	private float width;


	public int damageColumns
	{
		get { return columns; }
		set { columns = value; }
	}

	public int damageTaken
	{
		get { return damage; }
		set { damage = value; }
	}

	private void Start()
	{
		spots = new Spot[10, 10];
		
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
				//Setting position
				spots[i, j].transform.localPosition = Vector3.up * height * 0.5f -(Vector3.up *  spriteHeight * i);
				spots[i, j].transform.localPosition += Vector3.right * width * 0.5f - (Vector3.right * spriteWidth * j);
			}
		}
	}
}
