using UnityEngine;

public class AnimationCtrl : MonoBehaviour
{
    // reference
    public PatternAnimation[] animationClips;

    public enum AnimationNo
    {
        Leg = 0,
        Blink,
        Eye0,
        Eye1,
        Eye2
    }

    // Use this for initialization
    void Start()
    {
        for (int i = (int)AnimationNo.Eye0; i <= (int)AnimationNo.Eye2; i ++)
        {
            int j = i;
            animationClips[j].OnStart += () =>
            {
                DisableOthers((AnimationNo)j);
            };

            animationClips[j].OnEnd += () =>
            {
                DisableOthers(AnimationNo.Blink);
            };
        }
    }
    
    void DisableOthers(AnimationNo self)
    {
        for (int i = (int)AnimationNo.Blink; i <= (int)AnimationNo.Eye2; i ++)
        {
            if (i != (int)self)
                animationClips[i].gameObject.SetActive(false);
        }

        animationClips[(int)self].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Play(AnimationNo no)
    {
        animationClips[(int)no].Play();
    }

    public void Stop(AnimationNo no)
    {
        animationClips[(int)no].Stop();
    }

    public void Pause(AnimationNo no)
    {
        animationClips[(int)no].Pause();
    }

    public void Restart(AnimationNo no)
    {
        animationClips[(int)no].Restart();
    }
}    
