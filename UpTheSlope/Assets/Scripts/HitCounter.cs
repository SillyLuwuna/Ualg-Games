using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HitCounter : MonoBehaviour
{
	public TextMeshProUGUI HitsText;
	public string HitsBaseText = "hits: ";
	private int _hitCounter;

    void Start()
    {
		_hitCounter = 0;
		HitsText.text = HitsBaseText + "0";
    }

	void OnCollisionEnter(Collision collision)
	{
		_hitCounter++;
		HitsText.text = HitsBaseText + _hitCounter.ToString();
	}
}
