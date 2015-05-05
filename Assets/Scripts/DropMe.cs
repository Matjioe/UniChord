using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropMe : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Image containerImage;
	public Image receivingImage;
	public Text receivingText;
	public ChordProperty receivingChord;
	private Color normalColor;
	public Color highlightColor = Color.yellow;
	
	public void OnEnable ()
	{
		if (containerImage != null)
			normalColor = containerImage.color;
	}
	
	public void OnDrop(PointerEventData data)
	{
		containerImage.color = normalColor;
		
		if (receivingImage != null)
		{
			Sprite dropSprite = GetDropSprite (data);
			if (dropSprite != null)
				receivingImage.overrideSprite = dropSprite;
		}

		if (receivingText != null)
		{
			string dropText = GetDropText (data);
			if (dropText != null)
				receivingText.text = dropText;
		}

		if (receivingChord != null)
		{
			GetDropText(data, out receivingChord.m_rootNoteNumber, out receivingChord.m_musicalMode);
		}
	}

	public void OnPointerEnter(PointerEventData data)
	{
		if (containerImage == null)
			return;
		
		Sprite dropSprite = GetDropSprite (data);
		string dropText = GetDropText (data);
		if (dropSprite != null || dropText != null)
			containerImage.color = highlightColor;
	}

	public void OnPointerExit(PointerEventData data)
	{
		if (containerImage == null)
			return;
		
		containerImage.color = normalColor;
	}
	
	private Sprite GetDropSprite(PointerEventData data)
	{
		var originalObj = data.pointerDrag;
		if (originalObj == null)
			return null;

		var srcImage = originalObj.GetComponent<Image>();
		if (srcImage == null)
			return null;
		
		return srcImage.sprite;
	}

	private string GetDropText(PointerEventData data)
	{
		var originalObj = data.pointerDrag;
		if (originalObj == null)
			return null;
		
		var srcText = originalObj.GetComponent<Text>();
		if (srcText == null)
			return null;
		
		return srcText.text;
	}

	private void GetDropText(PointerEventData data, out int rootNoteNumber, out MusicalMode musicalMode)
	{
		rootNoteNumber = 0;
		musicalMode = MusicalMode.Major;

		var originalObj = data.pointerDrag;
		if (originalObj == null)
			return;
		
		var srcChordSelector = originalObj.GetComponent<ChordSelector>();
		if (srcChordSelector == null)
			return;
		
		rootNoteNumber = srcChordSelector.m_rootNoteNumber;
		musicalMode = srcChordSelector.m_musicalMode;
	}
}
