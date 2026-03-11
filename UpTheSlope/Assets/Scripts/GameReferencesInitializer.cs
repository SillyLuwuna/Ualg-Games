using UnityEngine;

public class GameReferencesInitializer : MonoBehaviour
{
	public GameObject player;

    void Start()
    {
		GameReferences.Player = player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
