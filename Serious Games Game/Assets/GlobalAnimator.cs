using UnityEngine;

public class GlobalAnimator : MonoBehaviour
{
    public void StartDialogue(DialogueScript script)
    {
        GameManager.Instance.LoadNewDialogue(script);
    }

    public void ForceAdvance()
    {
        GameManager.Instance.HardAdvanceLine();
    }

    public void ReturnProgression()
    {
        GameManager.Instance.ReturnProgression();
    }
}
