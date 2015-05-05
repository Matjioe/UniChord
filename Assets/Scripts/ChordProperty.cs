using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ChordProperty : MonoBehaviour {
	
	public int m_rootNoteNumber = 0;
	public MusicalMode m_musicalMode = MusicalMode.Major;
	public Strummer m_strummer;

	void Start ()
	{
		UnityEngine.UI.Button button = GetComponent<UnityEngine.UI.Button>();
		//UnityAction onClickKey = new UnityAction(OnClicked);
		//button.onClick.AddListener(onClickKey);
	}
	
	void Update ()
	{
		
	}
	
//	void OnClicked()
//	{
//		m_strummer.SetCurrentScale(m_rootNoteNumber, m_musicalMode);
//	}

	void OnMouseEnter()
	{
		m_strummer.SetCurrentScale(m_rootNoteNumber, m_musicalMode);
	}
}
