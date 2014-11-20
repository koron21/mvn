using UnityEngine;
using System.Collections;
using System;

public class PatternAnimation : MonoBehaviour
{
    public event Action OnStart;
    public event Action OnEnd;

    public float length;
    public bool loop;
    public float coolTimeMin;
    public float coolTimeMax;

    private float clipLength;
    private Transform[] clips;
    private float timer;
    private int index;
    private bool isPlaying;
    private float coolTime;

    void Start()
    {
        clips = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i ++)
        {
            clips[i] = transform.GetChild(i);
            if (clips[i].renderer == null)
                Debug.LogError(clips[i].name + "is null");
            else 
                clips[i].renderer.enabled = false;
        }
        clipLength = length / (float)clips.Length;
        index = 0;
        clips[0].renderer.enabled = true;
    }

    void Update()
    {
        if (isPlaying)
        {
            timer += Time.deltaTime;

            if (clips.Length == 0 || length <= 0)
                return;

            if (timer >= clipLength)
            {
                timer = 0.0f;
                clips[index].renderer.enabled = false;
                index ++;
                if (index == clips.Length)
                {
                    index = 0;
                    coolTime = UnityEngine.Random.Range(coolTimeMin, coolTimeMax);
                    timer = -coolTime;
                    if (!loop)
                    {
                        isPlaying = false;
                        if (OnEnd != null)
                            OnEnd();
                    }
                }
                clips[index].renderer.enabled = true;
            }
        }
    }

    public void Play()
    {
        if (OnStart != null)
            OnStart();
        isPlaying = true;
    }

    public void Pause()
    {
        isPlaying = false;
    }

    public void Stop()
    {
        isPlaying = false;
        index = 0;
    }

    public void Restart()
    {
        if (OnStart != null)
            OnStart();
        isPlaying = true;
        index = 0;
    }

    IEnumerator callEnd()
    {
        yield return new WaitForSeconds(coolTime);
        isPlaying = false;
        if (OnEnd != null)
            OnEnd();
    }
}



