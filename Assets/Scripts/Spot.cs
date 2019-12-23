using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpotFill {Empty, Partially, Fully }

public class Spot : MonoBehaviour
{
	[SerializeField]
	private GameObject[] images;
	private SpotFill filled;

	public SpotFill spotfill
	{
		get { return filled; }
		set { filled = value; setFilled(filled); }
	}

	void setFilled(SpotFill fill)
	{
		switch(fill)
		{
			case SpotFill.Empty:
				for (int i = 1; i < 3; i++)
				{
					images[i].SetActive(false);
				}
				break;
			case SpotFill.Partially:
				images[1].SetActive(true);
				images[2].SetActive(false);
				break;

			case SpotFill.Fully:
				images[1].SetActive(false);
				images[2].SetActive(true);
				break;
		}
	}
}
