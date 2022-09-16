using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TalkLayer : MonoBehaviour
{
	RawImage m_rawImage;

	public void UpdateTexture(Texture texture)
	{
		if (texture == null)
		{
			m_rawImage.enabled = false;
			m_rawImage.texture = null;
		}
		else
		{
			m_rawImage.texture = texture;
			m_rawImage.enabled = true;
			TextureresourceManager.Mark(texture.name);
		}
	}

	#region UNITY_DELEGATE

	void Awake()
	{
		m_rawImage = GetComponent<RawImage>();
		gameObject.tag = "Layer";

		m_rawImage.enabled = false;
	}

	#endregion

}