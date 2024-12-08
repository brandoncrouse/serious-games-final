using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public RectTransform textBox;
    public TextMeshProUGUI textBoxText;
    public bool textActive;
    float textStartY;
    public DialogueScript currentScript;
    public int curLine;
    int curChar;
    public string curText;
    public float textSpeed;

    public bool canProgress;

    [SerializeField] float lerpSpeed;

    public Animator sceneAnimator;

    PlayerController pc;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        textStartY = textBox.anchoredPosition.y;
        pc = FindFirstObjectByType<PlayerController>();
        if (currentScript != null)
        {
            LoadNewDialogue(currentScript);
        }
        else
        {
            textActive = false;
            textBox.anchoredPosition = new Vector2(0f, -750f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        textBox.anchoredPosition = Vector2.Lerp(textBox.anchoredPosition, textActive ? new Vector2(0f, textStartY) : new Vector2(0f, -750f), lerpSpeed * Time.deltaTime);
        if (textActive)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (curText == currentScript.dialogueLines[curLine].lineText)
                {
                    ClearText();
                    curLine++;
                    curChar = 0;
                    if (curLine < currentScript.dialogueLines.Length)
                    {
                        Invoke("NextChar", textSpeed);
                        if (currentScript.dialogueLines[curLine].nextAnimName != "")
                        {
                            sceneAnimator.Play(currentScript.dialogueLines[curLine].nextAnimName);
                        }
                    }
                    else
                    {
                        textActive = false;
                        pc.ReturnInputsInTime();
                        ResetText();
                    }
                }
                else
                {
                    CancelInvoke("NextChar");
                    curText = currentScript.dialogueLines[curLine].lineText;
                    UpdateText();
                }
            }
        }

    }

    public void LoadNewDialogue(DialogueScript dialogue)
    {
        ResetText();
        currentScript = dialogue;
        NextChar();
        textActive = true;
        pc.TakeInputs();
    }

    void ResetText()
    {
        curText = "";
        curLine = 0;
        curChar = 0;
        currentScript = null;
    }

    void NextChar()
    {
        curText += currentScript.dialogueLines[curLine].lineText[curChar];
        curChar++;
        UpdateText();
        if (curText != currentScript.dialogueLines[curLine].lineText)
        {
            Invoke("NextChar", textSpeed);
        }
    }

    void ClearText()
    {
        CancelInvoke("NextChar");
        curText = "";
        UpdateText();
    }

    void UpdateText()
    {
        textBoxText.text = curText;
        if (currentScript != null)
        {
            if (currentScript.dialogueLines[curLine].charName != "")
            {
                textBoxText.text = currentScript.dialogueLines[curLine].charName + ": " + curText;
            }
        }
    }

    public void HardAdvanceLine()
    {
        ClearText();
        curLine++;
        curChar = 0;
        if (curLine < currentScript.dialogueLines.Length)
        {
            Invoke("NextChar", textSpeed);
            if (currentScript.dialogueLines[curLine].nextAnimName != "")
            {
                sceneAnimator.Play(currentScript.dialogueLines[curLine].nextAnimName);
            }
        }
    }

    public void ReturnProgression() => canProgress = true;

    public void RevokeProgression() => canProgress = false;
}
