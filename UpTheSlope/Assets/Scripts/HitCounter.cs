using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HitCounter : MonoBehaviour
{
	public TextMeshProUGUI hitsText;
	// public TextMesh text;
	// public GameObject textObject;
	// public Text hitsText;
	public string hitsBaseText = "hits: ";
	private int counter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		counter = 0;
		hitsText.text = hitsBaseText + "0";
    }

    // Update is called once per frame
    void Update()
    {

    }

	void OnCollisionEnter(Collision collision)
	{
		counter++;
		hitsText.text = hitsBaseText + counter.ToString();
	}
}
