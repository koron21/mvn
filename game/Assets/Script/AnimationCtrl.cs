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
        Eye2,

        MAX
    }

    // Use this for initialization
    void Start()
    {
        // eye animation events
        int start = (int)AnimationNo.Blink;
        int end = Mathf.Min((int)AnimationNo.Eye2);
        for (int i = start; i <= end; i ++)
        {
            if (animationClips.Length <= i)
                break;

            int j = i;
            animationClips[j].OnStart += () =>
            {
                StopAndDisableOthers((AnimationNo)j);
            };

            animationClips[j].OnEnd += () =>
            {
                StopAndDisableOthers(AnimationNo.Blink);
            };
        }

        StopAndDisableOthers(AnimationNo.Blink);
    }
    
    void StopAndDisableOthers(AnimationNo self)
    {
        for (int i = (int)AnimationNo.Blink; i <= (int)AnimationNo.Eye2; i ++)
        {
            if (animationClips.Length <= i)
                break;

            if (i != (int)self)
            {
                animationClips[i].Stop();
                animationClips[i].gameObject.SetActive(false);
            }
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

    public void PlayRandom(AnimationNo[] list)
    {
        foreach(AnimationNo no in list)
        {
            if (animationClips[(int)no].IsPlaying)
                return;
        }

        int rand = Random.Range(0, list.Length);
        Play((AnimationNo)list[rand]);
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
