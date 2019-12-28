using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButton : MonoBehaviour
{
	NPC myNPC;

	public NPC NPC
	{
		set { myNPC = value; }
	}

	public void SelectNPC()
	{
		GameManager.instance.setNPC = myNPC;
	}
}
