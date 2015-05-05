using UnityEngine;
using System.Collections;



public class SoundPoolPlayer : MonoBehaviour {

	public int m_polyphony = 128;
	public GameObject m_audioSourcePrefab;
	public float m_sustain = 1.0f;

	GameObject [] m_audioSourceGOPool;
	Enveloppe [] m_audioSourcePool;

	float m_lastSustain = 1.0f;

	void Start ()
	{
		m_audioSourceGOPool = new GameObject[m_polyphony];
		m_audioSourcePool = new Enveloppe[m_polyphony];
		for (int i = 0; i < m_polyphony; ++i)
		{
			m_audioSourceGOPool[i] = Instantiate(m_audioSourcePrefab);
			m_audioSourceGOPool[i].transform.parent = transform;
			m_audioSourcePool[i] = m_audioSourceGOPool[i].GetComponent<Enveloppe>();
		}

		UpdateSustain();
		m_lastSustain = m_sustain;
	}
	
	void Update ()
	{
		if (m_sustain != m_lastSustain)
		{
			UpdateSustain();
			m_lastSustain = m_sustain;
		}
	}

	void UpdateSustain()
	{
		for (int i = 0; i < m_audioSourcePool.Length; ++i)
		{
			m_audioSourcePool[i].sustain = m_sustain;
		}
	}

	public void PlaySound(float pitch)
	{
		int mostEllapsedIdx = 0;
		int availableIdx = GetAvailableAudioSource(out mostEllapsedIdx);
		if (availableIdx == -1)
		{
			//Debug.LogWarning("Max polyphony reached");
			availableIdx = mostEllapsedIdx;
		}

		m_audioSourcePool[availableIdx].StartPlaying(pitch);
	}

	int GetAvailableAudioSource(out int mostEllapsedIndex)
	{
		mostEllapsedIndex = 0;
		float mostEllapsedTime = 0.0f;

		for (int i = 0; i < m_audioSourcePool.Length; ++i)
		{
			if (m_audioSourcePool[i].isPlaying == false)
				return i;

			if (m_audioSourcePool[i].ellapsedTime > mostEllapsedTime)
				mostEllapsedIndex = i;
		}

		return -1;
	}
}
