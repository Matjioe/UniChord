using UnityEngine;
using System.Collections;

public class StrumKeyProperty : MonoBehaviour {

	public Strummer m_strummer;
	public int keyIdx = 0;

	public void PlayKey()
	{
		m_strummer.SetCurrentKey(keyIdx);
		m_strummer.OnKeyStrummed();
	}
}
