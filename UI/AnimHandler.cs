using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class AnimHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public void OpenCloseStore()
    {
        if (animator.GetBool("Store"))
        {
            animator.SetBool("Store", false);
        }
        else
        {
            animator.SetBool("Store", true);
        }
    }
    public void OPenClosePrestige()
    {
        if (animator.GetBool("Prestige"))
        {
            animator.SetBool("Prestige", false);
        }
        else
        {
            animator.SetBool("Prestige", true);
        }
    }
}
