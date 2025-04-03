using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class TagEffect : MonoBehaviour
{  
    private ParticleSystem customParticleSystem;
    public Transform p1;
    public Transform p2;
    public bool executeTagEffect = false;
    void Start()
    {
        customParticleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if(p1 && p2) transform.position = (p1.position + p2.position) / 2;
        if(executeTagEffect) {
            customParticleSystem.Play();
            executeTagEffect = false;
        }
    }
}
