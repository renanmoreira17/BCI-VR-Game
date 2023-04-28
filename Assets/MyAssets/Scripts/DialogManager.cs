using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogManager : MonoBehaviour
{

    [SerializeField]
    private float typingSpeed = 0.05f;

    [SerializeField]
    private TextMeshProUGUI dialogText;

    [SerializeField]
    private GameObject option1Button;
    [SerializeField]
    private GameObject option2Button;

    private int index;

    private DialogueTree dialogueTree;
    private string currentText;

    public void StartDialogue()
    {
        dialogueTree = DialogueTreeFactory.CreateVillageDialogue();
        currentText = dialogueTree.currentNode.text;
        StartCoroutine(TypeDialogue());
    }

    private IEnumerator TypeDialogue()
    {
        foreach (char letter in currentText.ToCharArray())
        {
            dialogText.text += letter;
            dialogText.ForceMeshUpdate();
            yield return new WaitForSeconds(typingSpeed);
        }

        if (!IsDialogueOver())
        {
            option1Button.SetActive(true);
            option2Button.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool IsDialogueOver()
    {
        return dialogueTree.currentNode.isLeaf;
    }

    private void PreOptionChosen()
    {
        dialogText.text = string.Empty;
        option1Button.SetActive(false);
        option2Button.SetActive(false);
    }

    private void PostOptionChosen()
    {
        currentText = dialogueTree.currentNode.text;
        StartCoroutine(TypeDialogue());
    }

    public void Option1Chosen()
    {
        if (IsDialogueOver()) return;
        PreOptionChosen();
        dialogueTree.ChooseOption(DialogueTree.OptionChoice.First);
        PostOptionChosen();
    }

    public void Option2Chosen()
    {
        if (IsDialogueOver()) return;
        PreOptionChosen();
        dialogueTree.ChooseOption(DialogueTree.OptionChoice.Second);
        PostOptionChosen();
    }
}
