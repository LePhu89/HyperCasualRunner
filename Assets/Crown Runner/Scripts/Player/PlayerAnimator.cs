using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Elenments")]
    [SerializeField] private Transform runnerParent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Run()
    {
        for (int i = 0; i < runnerParent.childCount; i++)
        {
            Transform runner = runnerParent.GetChild(i);
            Animator runnerAnimator = runner.GetComponent<Runner>().GetAnimator();

            runnerAnimator.Play("Run");
        }
    }
    public void Idle()
    {
        for (int i = 0; i < runnerParent.childCount; i++)
        {
            Transform runner = runnerParent.GetChild(i);
            Animator runnerAnimator = runner.GetComponent<Runner>().GetAnimator();

            runnerAnimator.Play("Idle");
        }
    }
}
