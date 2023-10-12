using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTool : MonoBehaviour
{
    public List<Transform> ErrorTrans;
    [ContextMenu("Check Object Scale")]
    void CheckScale()
    {
        ErrorTrans = new List<Transform>();
        var allTrasn = FindObjectsOfType<Transform>(true);
        foreach (var trans in allTrasn)
        {
            if (trans.localScale.x == 0 || trans.localScale.y == 0 || trans.localScale.z == 0)
            {
                ErrorTrans.Add(trans);
            }
        }

        Debug.Log($"Check Trans Count = {allTrasn.Length} Error Scale Count : {ErrorTrans.Count}");
    }

    [ContextMenu("Check Rend Setting")]
    void CheckRendSetting()
    {
        var rends = new List<Renderer>();
        var allRends = FindObjectsOfType<Renderer>(true);
        foreach (var rend in allRends)
        {
            rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            rend.receiveShadows = false;
            rend.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
            rend.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
            rend.motionVectorGenerationMode = MotionVectorGenerationMode.Object;
            rend.allowOcclusionWhenDynamic = false;

        }
        var allskinRends = FindObjectsOfType<SkinnedMeshRenderer>(true);

        foreach (var item in allskinRends)
        {
            item.skinnedMotionVectors = false;
        }

        Debug.Log($"Check Trans Count = {rends.Count} Setting allRends Count : {allRends.Length}");
    }
}
