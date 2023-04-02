using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(Animator))]
public class AnimHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PostProcessVolume volume;
    private Bloom bloom;
    private ChromaticAberration aberration;
    private LensDistortion distortion;
    private ColorGrading grading;

    void Start()
    {
        volume.profile.TryGetSettings(out bloom);
        volume.profile.TryGetSettings(out aberration);
        volume.profile.TryGetSettings(out distortion);
        volume.profile.TryGetSettings(out grading);
    }
    void Update()
    {
        if (DataHandler.Instance.frenzy)
        {
            if (bloom.intensity != 22)
            {
                bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, 22, Time.deltaTime * 2f);
            }
            if (aberration.intensity != 1)
            {
                aberration.intensity.value = Mathf.Lerp(aberration.intensity.value, 1, Time.deltaTime * 2f);
            }
            if (distortion.intensity != 29)
            {
                distortion.intensity.value = Mathf.Lerp(distortion.intensity.value, 29, Time.deltaTime * 2f);
            }
        }
        else
        {
            if (bloom.intensity != 0) bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, 0, Time.deltaTime * 12f);
            if (aberration.intensity != 0) aberration.intensity.value = Mathf.Lerp(aberration.intensity.value, 0, Time.deltaTime * 12f);
            if (distortion.intensity != 0)  distortion.intensity.value = Mathf.Lerp(distortion.intensity.value, 0, Time.deltaTime * 12f);
        }
    }
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
    public void Frenzy()
    {

    }
}
