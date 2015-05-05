using UnityEngine;
using System.Collections;

public enum MusicalMode
{
	Major = 0,
	Minor = 1,
	Seventh = 2
};

public class Strummer : MonoBehaviour {

	// Parameters
	public SoundPoolPlayer m_soundManager;
	public MusicalMode m_scale_mode = MusicalMode.Major;
	public int m_transpose = 1;

	// Public internal (hide in inspector)
	public int m_chordIdx = 0;
	public int m_currentKeyStrummed = 0;

	// Debug
	public float m_rootPitch;
	public int m_octave;
	public int m_chordNoteIdx;
	public float m_semi_tone_ratio = 0.0f;
	public float m_11_tones_ratio;

	void Start ()
	{
		m_semi_tone_ratio = Mathf.Pow(2.0f, 1.0f/12.0f);
		m_11_tones_ratio = Mathf.Pow(m_semi_tone_ratio, 11.0f);
	}
	
	void Update () {
	
	}

	public void SetCurrentScale(int chordIdx, MusicalMode scale_mode)
	{
		m_chordIdx = chordIdx;
		m_scale_mode = scale_mode;
	}

	public void SetCurrentKey(int keyIdx)
	{
		m_currentKeyStrummed = keyIdx;
	}

	public void OnKeyStrummed()
	{
		float pitch = GetPitchFromCurrentScaleAndCurrentKey();
		m_soundManager.PlaySound(pitch);
	}
	
	float GetPitchFromCurrentScaleAndCurrentKey()
	{
		m_rootPitch = GetPitchFromChord(m_chordIdx);
		m_rootPitch = m_rootPitch / (float)Mathf.Pow(m_transpose, 2);
		m_octave = GetOctave(m_currentKeyStrummed);
		m_chordNoteIdx = GetChordNoteIdx(m_currentKeyStrummed);
		float pitch = ComputePitch(m_rootPitch, m_octave, m_chordNoteIdx);
		return pitch;
	}

	float GetPitchFromChord(int chordIdx)
	{
		return Mathf.Pow(m_semi_tone_ratio, (float)chordIdx);
	}

	float ComputePitch(float rootPitch, int octave, int chordNoteIdx)
	{
		if (m_scale_mode == MusicalMode.Major)
		{
			return rootPitch * Mathf.Pow(2.0f, (float)octave) * (1.0f + 0.25f * (float)chordNoteIdx);
		}
		else if (m_scale_mode == MusicalMode.Minor)
		{
			float chordNotePitch = 1.0f;
			if (chordNoteIdx == 1)
				chordNotePitch = 1.2f;
			else if (chordNoteIdx == 2)
				chordNotePitch = 1.5f;

			return rootPitch * Mathf.Pow(2.0f, (float)octave) * chordNotePitch;
		}
		else if (m_scale_mode == MusicalMode.Seventh)
		{
			float chordNotePitch = 1.0f;
			if (chordNoteIdx == 1)
				chordNotePitch = 1.25f;
			else if (chordNoteIdx == 2)
				chordNotePitch = 1.5f;
			else if (chordNoteIdx == 3)
				chordNotePitch = m_11_tones_ratio;
			
			return rootPitch * Mathf.Pow(2.0f, (float)octave) * chordNotePitch;
		}
		else
		{
			return 0.0f;
		}
	}

	int GetOctave(int keyIdx)
	{
		if (m_scale_mode == MusicalMode.Seventh)
			return keyIdx / 4;
		else
			return keyIdx / 3;
	}

	int GetChordNoteIdx(int keyIdx)
	{
		if (m_scale_mode == MusicalMode.Seventh)
			return keyIdx % 4;
		else
			return keyIdx % 3;
	}
}
