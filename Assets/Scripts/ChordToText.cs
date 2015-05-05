using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
[RequireComponent(typeof(ChordSelector))]
public class ChordToText : MonoBehaviour {

	Text textComp;
	ChordSelector chordSelector;

	string [] keyNames = { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };
	
	void Start ()
	{
		chordSelector = GetComponent<ChordSelector>();
		textComp = GetComponent<Text>();
		UpdateTextFromChord ();
	}

	public void UpdateTextFromChord ()
	{
		textComp.text = keyNames[chordSelector.m_rootNoteNumber];
		if (chordSelector.m_musicalMode == MusicalMode.Minor)
			textComp.text += "m";
		else if (chordSelector.m_musicalMode == MusicalMode.Seventh)
			textComp.text += "7";
	}
}
