using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChordManager : MonoBehaviour {

	public Strummer m_strummer;

	public int m_currentChordIdx = 0;
	public MusicalMode m_musicalMode = MusicalMode.Major;

	//string [] keyNames = { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };
	string [] keyNames = { "Db", "Ab", "Bb", "Eb", "F", "C", "G", "D", "A", "E#", "B", "F#" };
	int [] keyRoot = { 1, 8, 10, 3, 5, 0, 7, 2, 9, 4, 11, 6 };

	// Use this for initialization
	void Start ()
	{
		InitKeysEvents();
	}

	void InitKeysEvents()
	{
		int i = 0;

		foreach(Transform child in transform)
		{
			child.gameObject.name = keyNames[i];

			// Assign text
			UnityEngine.UI.Text keyText = child.gameObject.GetComponentInChildren<Text>();
			keyText.text = keyNames[i];

			// Properties
			ChordProperty chordProp = child.gameObject.GetComponent<ChordProperty>();
			chordProp.m_musicalMode = m_musicalMode;
			chordProp.m_rootNoteNumber = keyRoot[i];
			chordProp.m_strummer = m_strummer;
			
			// Get next key name
			i++;
			if (i >= keyNames.Length)
				i = 0;
		}
	}
}
