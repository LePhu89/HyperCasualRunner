using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private Transform runnerParrent;
    [SerializeField] private RunnerSelector runnerSelectorPrefab;
    // Start is called before the first frame update
    void Start()
    {
        ShopManager.onSkinSelected += SelectSkin;
    }
    private void OnDestroy()
    {
        ShopManager.onSkinSelected -= SelectSkin;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            SelectSkin(Random.Range(0, 8));
    }

    public void SelectSkin(int skinIndex)
    {
        for (int i = 0; i < runnerParrent.childCount; i++)
        {
            runnerParrent.GetChild(i).GetComponent<RunnerSelector>().SelectRunner(skinIndex);
        }
        runnerSelectorPrefab.SelectRunner(skinIndex);
    }
}
