using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static float GetClipLength(this Animator animator, string clipName)
    {
        return GetClip(animator, clipName).length;
    }

    public static AnimationClip GetClip(this Animator animator, string clipName)
    {
        if (animator.runtimeAnimatorController is AnimatorOverrideController)
        {
            var overrides = new List<KeyValuePair<AnimationClip, AnimationClip>>();
            ((AnimatorOverrideController)animator.runtimeAnimatorController).GetOverrides(overrides);
            foreach (var kv in overrides)
            {
                if (kv.Key.name == clipName)
                    return kv.Value;
            }
        }
        else
        {
            var clips = animator.runtimeAnimatorController.animationClips;
            foreach (var clip in clips)
            {
                if (clip.name == clipName)
                    return clip;
            }
        }
        Debug.LogError($"clip {clipName} not found");
        return null;
    }
}
