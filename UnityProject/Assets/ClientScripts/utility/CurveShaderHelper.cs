using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveShaderHelper : MonoBehaviour
{
    public AnimationCurve mCurve;

    private void Start()
    {
        DebugCurve(mCurve);
    }
    
    void DebugCurve(AnimationCurve anim)
    {
        Debug.Log("Evaluate Unity: " + anim.Evaluate(0.1f) + ", " + anim.Evaluate(0.2f) + ", " + anim.Evaluate(0.3f) + ", " + anim.Evaluate(0.4f) + ", " + anim.Evaluate(0.5f) + ", " + anim.Evaluate(0.6f) + ", " + anim.Evaluate(0.76f) + ", " + anim.Evaluate(0.88f) + ", " + anim.Evaluate(0.98f));


        Debug.Log("Evaluate Cubic: " + Evaluate(0.1f) + ", " + Evaluate(0.2f) + ", " + Evaluate(0.3f) + ", " + Evaluate(0.4f) + ", " + Evaluate(0.5f) + ", " + Evaluate(0.6f) + ", " + Evaluate(0.76f) + ", " + Evaluate(0.88f) + ", " + Evaluate(0.98f));
    }

    public float Evaluate(float t)
    {
        return EvaluateCurve(mCurve, t);
    }

    float EvaluateCurve(AnimationCurve anim ,float t)
    {
        float p1x = anim.keys[0].time;
        float p1y = anim.keys[0].value;
        float tp1 = anim.keys[0].outTangent;
        float p2x = anim.keys[1].time;
        float p2y = anim.keys[1].value;
        float tp2 = anim.keys[1].inTangent;

        Debug.Log(p1x + ", " + p1y + ", " + tp1 + ", " + p2x + ", " + p2y + ", " + tp2);

        float a = (p1x * tp1 + p1x * tp2 - p2x * tp1 - p2x * tp2 - 2 * p1y + 2 * p2y) / (p1x * p1x * p1x - p2x * p2x * p2x + 3 * p1x * p2x * p2x - 3 * p1x * p1x * p2x);
        float b = ((-p1x * p1x * tp1 - 2 * p1x * p1x * tp2 + 2 * p2x * p2x * tp1 + p2x * p2x * tp2 - p1x * p2x * tp1 + p1x * p2x * tp2 + 3 * p1x * p1y - 3 * p1x * p2y + 3 * p1y * p2x - 3 * p2x * p2y) / (p1x * p1x * p1x - p2x * p2x * p2x + 3 * p1x * p2x * p2x - 3 * p1x * p1x * p2x));
        float c = ((p1x * p1x * p1x * tp2 - p2x * p2x * p2x * tp1 - p1x * p2x * p2x * tp1 - 2 * p1x * p2x * p2x * tp2 + 2 * p1x * p1x * p2x * tp1 + p1x * p1x * p2x * tp2 - 6 * p1x * p1y * p2x + 6 * p1x * p2x * p2y) / (p1x * p1x * p1x - p2x * p2x * p2x + 3 * p1x * p2x * p2x - 3 * p1x * p1x * p2x));
        float d = ((p1x * p2x * p2x * p2x * tp1 - p1x * p1x * p2x * p2x * tp1 + p1x * p1x * p2x * p2x * tp2 - p1x * p1x * p1x * p2x * tp2 - p1y * p2x * p2x * p2x + p1x * p1x * p1x * p2y + 3 * p1x * p1y * p2x * p2x - 3 * p1x * p1x * p2x * p2y) / (p1x * p1x * p1x - p2x * p2x * p2x + 3 * p1x * p2x * p2x - 3 * p1x * p1x * p2x));

        return a * t * t * t + b * t * t + c * t + d;
    }
}
