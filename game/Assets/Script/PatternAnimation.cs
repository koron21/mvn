using UnityEngine;
using System.Collections;

public class PatternAnimation : MonoBehaviour
{
    public float AnimationSpeed
    {
        get { return animationSpeed; }
    }
    private float animationSpeed;

    public AnimationCurve legAnimationCurve;
    public float legAnimationLength;

    private float legAnimationTimer;

    // reference
    private Transform leg;
    private Transform[] legParts;

    // Use this for initialization
    void Start()
    {
        // default initializer
        if (legAnimationLength <= 0.0f)
            legAnimationLength = 1.0f;

        // find leg, and animation parts
        leg = transform.FindChild("legs");
        if (leg)
        {
            legParts = new Transform[leg.childCount];
            for (int i = 0; i < leg.childCount; i ++)
            {
                legParts[i] = leg.GetChild(i);
            }
        }
   }
    
    // Update is called once per frame
    void Update()
    {
        advanceTimer(ref legAnimationTimer, legAnimationLength);
        pickLegPattern();
    }

    public void SetAnimationSpeed(float value)
    { 
        if (value <= -5.0f)
            value = -5.0f;
        if (value >= 5.0f)
            value = 5.0f;
        
        animationSpeed = value;
    }

    void advanceTimer(ref float timer, float period)
    {
        timer += Time.deltaTime * animationSpeed;
    }

    void pickLegPattern()
    {
        foreach(Transform t in legParts)
        {
            //t.gameObject.SetActive(false);
            t.renderer.enabled = false;
        }

        float ratio = legAnimationTimer / legAnimationLength;
        float rMod = legAnimationCurve.Evaluate(ratio);
        int index = (int)(legParts.Length * rMod) % legParts.Length;
        //legParts[index].gameObject.SetActive(true);
        legParts[index].renderer.enabled = true;
    }

    int pickPattern(float ratio, int patternNo)
    {
        float rMod = legAnimationCurve.Evaluate(ratio);
        return Mathf.RoundToInt(patternNo * rMod);
    }
}
