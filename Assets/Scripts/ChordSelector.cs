using UnityEngine;
using System.Collections;

public class ChordSelector : MonoBehaviour {

	public int m_rootNoteNumber = 0;
	public MusicalMode m_musicalMode = MusicalMode.Major;

	void Start () {
	}

	public void IncreaseChordSelector()
	{
		m_rootNoteNumber++;
		if (m_rootNoteNumber > 11)
		{
			m_rootNoteNumber = 0;
			IncreaseMusicalMode();
		}
	}

	public void IncreaseMusicalMode()
	{
		m_musicalMode++;
		if ((int)m_musicalMode > 2)
			m_musicalMode = 0;
	}

	public void DecreaseChordSelector()
	{
		if (m_rootNoteNumber == 0)
		{
			m_rootNoteNumber = 11;
			DecreaseMusicalMode();
		}
		else
		{
			m_rootNoteNumber--;
		}
	}
	
	public void DecreaseMusicalMode()
	{
		if ((int)m_musicalMode == 0)
			m_musicalMode = (MusicalMode)2;
		else
			m_musicalMode--;

	}
}
