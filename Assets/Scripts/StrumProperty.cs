using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class StrumProperty : MonoBehaviour {

	public Strummer m_strummer;
	public int stepCount = 64;

	// Use this for initialization
	void Start () {
		Scrollbar scroll = GetComponent<Scrollbar>();
		scroll.numberOfSteps = stepCount;

		UnityAction<float> onValueChanged = new UnityAction<float>(OnValueChanged);
		scroll.onValueChanged.AddListener(onValueChanged);
	}

	void OnValueChanged(float value)
	{
		m_strummer.SetCurrentKey((int)(value * stepCount));
		m_strummer.OnKeyStrummed();
	}
}
