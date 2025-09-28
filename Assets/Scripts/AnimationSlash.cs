using UnityEngine;

public class AnimationSlash : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] string triggerName = "Q Attack"; // EXACT name from your Animator parameter
    int trigHash;

    void Awake()
    {
        if (!anim) anim = GetComponent<Animator>();
        trigHash = Animator.StringToHash(triggerName);
        Debug.Log($"[Test] Animator on: {anim?.gameObject.name}, controller: {anim?.runtimeAnimatorController}", anim);
        if (anim && anim.layerCount > 0) anim.SetLayerWeight(0, 1f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && anim)
        {
            anim.ResetTrigger(trigHash); // optional
            anim.SetTrigger(trigHash);
            Debug.Log("[Test] Fired trigger: " + triggerName);
        }
    }
}