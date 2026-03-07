using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HitCounter : MonoBehaviour
{
	// public TextMeshProUGUI hitsText;
	private int hitCounter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		hitCounter = 0;
		// hitsText.text = hitsBaseText + "0";
    }

    // Update is called once per frame
    void Update()
    {

    }

	private void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 100, 20), "hits: " + hitCounter);
	}

	void OnCollisionEnter(Collision collision)
	{
		hitCounter++;
		// hitsText.text = hitsBaseText + hit_counter.ToString();
	}
}
