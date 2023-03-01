using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class MainButton : MonoBehaviour
{
    public List<AudioClip> _clickSounds;
    public AudioSource _source;
    void Start(){
        //_source.gameObject.GetComponent<AudioSource>();
    }
public void PlaySound(){
    var sound = Random.Range(0, _clickSounds.Count);
_source.PlayOneShot(_clickSounds[sound]);
}
}
