using UnityEngine;

public class EffectCtrl : MonoBehaviour
{
    public ParticleSystem[] effects;

    public enum Effect
    {
        Dust = 0
    }

    void Start()
    {     
    }

    void Update()
    {
    }

    public void Play(Effect No)
    {
        //effects[(int)No].renderer.enabled = true;
        effects[(int)No].Play();
    }

    public void Pause(Effect No)
    {
        //effects[(int)No].renderer.enabled = false;
        effects[(int)No].Pause();
    }

    public void Stop(Effect No)
    {
        effects[(int)No].Stop();
    }
}

