using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "New Script", menuName = "Dialogue Script", order = 1)]
public class DialogueScript : ScriptableObject
{
    public DialogueLine[] dialogueLines;
}

[System.Serializable]
public class DialogueLine
{
    public string charName;
    public string lineText;
    public string nextAnimName;
}