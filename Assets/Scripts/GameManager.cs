using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] DamageSquare[] dmgSquares;
	[SerializeField] BleedingSquare bleedingSquare;
	private int selectedSquare;
	private NPC activeNPC;

	private List<NPC> NPCs;

	static GameManager inst;

	#region Properties

	public static GameManager instance
	{
		get { return inst; }
	}

	public NPC setNPC
	{
		set { activeNPC = value; SetNPC(); }
	}

	#endregion

	private void Awake()
	{
		if (inst != null)
			Destroy(this);
		inst = this;
		NPCs = new List<NPC>(32);

		
	}
	private void Start()
	{
		//REMOVE
		setNPC = new NPC(6, 7);
		NPCs.Add(activeNPC);
		NPCs.Add(new NPC(7, 8));
	}
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			selectedSquare = 0;
			print("Trauma selected");
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			selectedSquare = 1;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			selectedSquare = 2;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			selectedSquare = 3;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha0))
		{
			selectedSquare = -1;
		}
		else if(Input.GetKeyDown(KeyCode.P))
		{
			activeNPC.Damage(1,selectedSquare);
			UpdateDamage();
		}
		else if(Input.GetKeyDown(KeyCode.S))
		{
			setNPC = activeNPC == NPCs[0] ? NPCs[1] : NPCs[0];
		}
	}

	private void SetNPC()
	{
		for (int i = 0; i < dmgSquares.Length-1; i++)
		{
			dmgSquares[i].damageColumns = activeNPC.numberOfColumns;
			dmgSquares[i].damageTaken = activeNPC.damageCrosses[i];
		}
		dmgSquares[3].damageColumns = activeNPC.exColumns;
		dmgSquares[3].damageTaken = activeNPC.damageCrosses[3];

		bleedingSquare.damageTaken = activeNPC.bleedingCrosses;
	}

	public void UpdateDamage()
	{
		for (int i = 0; i < dmgSquares.Length; i++)
		{
			dmgSquares[i].damageTaken = activeNPC.damageCrosses[i];
		}

		bleedingSquare.damageTaken = activeNPC.bleedingCrosses;
	}
}
