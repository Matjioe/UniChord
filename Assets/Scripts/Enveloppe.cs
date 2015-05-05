using UnityEngine;
using System.Collections;

public class Enveloppe : MonoBehaviour {

	public enum EnveloppeType
	{
		Curve,
		Length,
		CurveAndLength
	}

	public float length = 3.0f;
	public AnimationCurve animationCurve;
	public EnveloppeType enveloppeType = EnveloppeType.Curve;
	public float sustain = 1.0f;

	public bool isPlaying = false;
	public float ellapsedTime = 0.0f;

	AudioSource m_audioSource;

	// Use this for initialization
	void Start () {
		m_audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isPlaying == true)
		{
			ellapsedTime += Time.deltaTime;

			if (enveloppeType == EnveloppeType.Curve)
			{
				m_audioSource.volume = animationCurve.Evaluate(ellapsedTime / sustain);

				if (ellapsedTime >= animationCurve.length || ellapsedTime >= length)
				{
					m_audioSource.Stop();
					isPlaying = false;
					ellapsedTime = 0.0f;
				}
			}
			else
			{
				if (ellapsedTime >= length)
				{
					m_audioSource.Stop();
					isPlaying = false;
					ellapsedTime = 0.0f;
				}
			}
		}
	}

	public void StartPlaying(float pitch)
	{
		m_audioSource.pitch = pitch;
		m_audioSource.Play();
		m_audioSource.loop = true;
		ellapsedTime = 0.0f;
		isPlaying = true;

		if (enveloppeType == EnveloppeType.Curve)
			m_audioSource.volume = animationCurve.Evaluate(0.0f);	
	}
}
