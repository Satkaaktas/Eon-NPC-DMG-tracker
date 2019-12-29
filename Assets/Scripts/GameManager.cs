using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField] DamageSquare[] dmgSquares;
	[SerializeField] BleedingSquare bleedingSquare;
	[SerializeField] Image[] highlights;
	[SerializeField] Text[] stats;
	[SerializeField] GameObject content, NPCButtonPrefab, AreYouSurePanel;
	private int selectedSquare;
	private NPC activeNPC;

	private int NPCs;

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
		NPCs = 0;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
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
		else if (Input.GetKeyDown(KeyCode.P))
		{
			if (activeNPC != null)
			{
				activeNPC.Damage(1, selectedSquare);
				UpdateDamage();
			}
			else
				Debug.Log("No active NPC");
		}
	}

	private void SetNPC()
	{
		for (int i = 0; i < dmgSquares.Length - 1; i++)
		{
			dmgSquares[i].damageColumns = activeNPC.numberOfColumns;
			dmgSquares[i].damageTaken = activeNPC.damageCrosses[i];
		}
		dmgSquares[3].damageColumns = activeNPC.exColumns;
		dmgSquares[3].damageTaken = activeNPC.damageCrosses[3];

		bleedingSquare.damageTaken = activeNPC.bleedingCrosses;

		UpdateStats();
	}

	public void UpdateDamage()
	{
		for (int i = 0; i < dmgSquares.Length; i++)
		{
			dmgSquares[i].damageTaken = activeNPC.damageCrosses[i];
		}

		bleedingSquare.damageTaken = activeNPC.bleedingCrosses;
		UpdateStats();
	}

	public void CreateNPC(int dmgColumns, int exhColumns)
	{
		CreateNPC(dmgColumns, exhColumns, "NPC #" + (NPCs));
	}

	public void CreateNPC(int dmgColumns, int exhColumns, string name)
	{
		setNPC = new NPC(dmgColumns, exhColumns, name);
		NPCs++;
		GameObject _button = Instantiate(NPCButtonPrefab);
		_button.transform.parent = content.transform;
		_button.GetComponent<PlayerButton>().NPC = activeNPC;
		_button.transform.GetChild(0).GetComponent<Text>().text = activeNPC.name;
	}



	#region UI

	public void SelectSquare(int square)
	{
		//Removing old highlight
		if (selectedSquare == -1)
		{
			Color _color = highlights[4].color;
			_color.a = 0;
			highlights[4].color = _color;
		}
		else
		{
			Color _color = highlights[selectedSquare].color;
			_color.a = 0;
			highlights[selectedSquare].color = _color;
		}

		//Setting new highlight
		if (square == -1)
		{
			Color _color = highlights[4].color;
			_color.a = 155;
			highlights[4].color = _color;

		}
		else
		{
			Color _color = highlights[square].color;
			_color.a = 155;
			highlights[square].color = _color;
		}

		selectedSquare = square;
	}

	public void AddDamage(int dmg)
	{
		if (activeNPC != null)
		{
			activeNPC.Damage(dmg, selectedSquare);
			UpdateDamage();
		}
		else
			Debug.Log("No active NPC");
	}

	private void UpdateStats()
	{
		int[] _rows = new int[4];

		for (int i = 0; i < _rows.Length; i++)
		{
			_rows[i] = dmgSquares[i].GetStat();
		}

		//Dödsslag
		stats[0].text = "Dödsslag: OB" + (_rows[0] + _rows[2]) + "T6";
		//Chockslag
		stats[1].text = "Chockslag: OB" + (_rows[0] + _rows[1] + _rows[2]) + "T6";
		//Svårighetsökning
		stats[2].text = "Svårighetsökning: OB" + (_rows[1] + _rows[3]) + "T6";
		//Förflyttningsmod
		string _prefix = _rows[1] + _rows[3] > 0 ? "-" : "\u00B1";
		stats[3].text = "Förflyttning: " + _prefix + (_rows[1] + _rows[3]);
	}

	public void AddBloodLoss()
	{
		int _bleeding = bleedingSquare.GetStat();

		if (activeNPC != null)
		{
			activeNPC.Damage(_bleeding, 2);
			UpdateDamage();
		}
		else
			Debug.Log("No active NPC");

	}

	public void DeleteNPC()
	{
		if (activeNPC != null)
		{
			PlayerButton _button = activeNPC.button;

			//This (probablly?) creates garbage
			activeNPC = null;

			foreach (DamageSquare square in dmgSquares)
			{
				square.ClearSpots();
			}
			bleedingSquare.ClearSpots();


			Destroy(_button.gameObject);

			ToggleAreYouSurePanel(false);
		}
	}
	public void ToggleAreYouSurePanel(bool active)
	{
		AreYouSurePanel.SetActive(active);
	}

	#endregion

}
