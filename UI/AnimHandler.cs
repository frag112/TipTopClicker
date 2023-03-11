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
        }else{
            animator.SetBool("Store", true);
        }
    }
}
