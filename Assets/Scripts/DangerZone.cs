using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class DangerZone : MonoBehaviour
{
    public EncounterData encounterData;
    [SerializeField] private float minTravel;   // limits for the totalTravel value to reach
    [SerializeField] private float maxTravel;
    
    private float totalTravel;
    private Vector2 prevPoint;

    private float limit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        limit = Random.Range(minTravel, maxTravel);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        var currPoint = col.transform.position;
        var distance = Vector2.Distance(currPoint, prevPoint);
        totalTravel += distance;

        if (totalTravel > limit)
        {
            DontDestroyOnLoad(gameObject);  //Will be destroyed after getting used
            FindObjectOfType<FadeControl>().FadeStart(1);
        }
        
        
        prevPoint = currPoint;
    }
}
