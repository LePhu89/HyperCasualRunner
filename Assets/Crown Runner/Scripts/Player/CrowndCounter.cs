using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrowndCounter : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshPro crowdCounterText;
    [SerializeField] private Transform runnersParent;
    
    void Update()
    {
        crowdCounterText.text = runnersParent.childCount.ToString();

        if(runnersParent.childCount <= 0)
            Destroy(gameObject);
    }
}
