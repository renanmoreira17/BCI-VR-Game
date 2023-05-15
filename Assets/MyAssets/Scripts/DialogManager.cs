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

    private GameObject buttonText;


    public GameObject opt1TextObj;
    public GameObject opt2TextObj;

    
    public CharReplacerHindi _TextDialogue = new CharReplacerHindi();
    public CharReplacerHindi _TextOpt1 = new CharReplacerHindi();
    public CharReplacerHindi _TextOpt2 = new CharReplacerHindi();

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
            // dialogText.ForceMeshUpdate();
            _TextDialogue.UpdateMe();
            yield return new WaitForSeconds(typingSpeed);
        }

        if (!IsDialogueOver())
        {
            option1Button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dialogueTree.currentNode.option1.optionText;
            print(dialogueTree.currentNode.option1.text);
            option2Button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dialogueTree.currentNode.option2.optionText;
            print(dialogueTree.currentNode.option2.text);
            option1Button.SetActive(true);
            option2Button.SetActive(true);
            _TextOpt1 = opt1TextObj.GetComponent<CharReplacerHindi>();
            _TextOpt2 = opt2TextObj.GetComponent<CharReplacerHindi>();
            _TextOpt1.UpdateMe();
            _TextOpt2.UpdateMe();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        option1Button.SetActive(false);
        option2Button.SetActive(false);
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
        // option1Button.
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
