using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Creator : MonoBehaviour
{
	[SerializeField] GameObject NPCCreationPanel;
	[SerializeField] Text nameInput;
	Image[,] buttons;
	int dmgCol = 10, exhCol = 10;

	int dmgColumns
	{
		set { ChangeDmgHighlight(dmgCol,value); dmgCol = value; }
	}

	int exhColumns
	{
		set { ChangeExhHighlight(exhCol, value); exhCol = value; }
	}

	// Start is called before the first frame update
	void Start()
	{
		nameInput.text = "";
		buttons = new Image[2, 10];
		Transform _obj = GameObject.Find("dmgColumns").transform;
		for (int i = 0; i < buttons.GetLength(1); i++)
		{
			buttons[0, i] = _obj.GetChild(i).GetComponent<Image>();
		}

		_obj = GameObject.Find("exhaustionColumns").transform;
		for (int i = 0; i < buttons.GetLength(1); i++)
		{
			buttons[1, i] = _obj.GetChild(i).GetComponent<Image>();
		}
		NPCCreationPanel.SetActive(false);
	}

	private void ChangeDmgHighlight(int from, int to)
	{
		buttons[0, from - 1].color = Color.white;
		
		buttons[0, to-1].color = Color.red;
	}

	private void ChangeExhHighlight(int from, int to)
	{
		buttons[1, from - 1].color = Color.white;

		buttons[1, to - 1].color = Color.red;
	}

	public void ShowNPCPanel()
	{
		NPCCreationPanel.SetActive(true);
	}

	public void setDmgColumns(int columns)
	{
		dmgColumns = columns;
	}

	public void setExhColumns(int columns)
	{
		exhColumns = columns;
	}

	public void CreateNPC()
	{
		if (nameInput.text != "")
		{
			GameManager.instance.CreateNPC(dmgCol, exhCol, nameInput.text);
		}
		else
		{
			GameManager.instance.CreateNPC(dmgCol, exhCol);
		}
		ResetValues();
		NPCCreationPanel.SetActive(false);
	}

	private void ResetValues()
	{
		buttons[0, dmgCol - 1].color = Color.white;
		buttons[1, exhCol - 1].color = Color.white;
		dmgCol = 10;
		exhCol = 10;
	}

}
