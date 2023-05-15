public class DialogueNode
{
    public DialogueNode(string optionText, string text, DialogueNode option1, DialogueNode option2)
    {
        this.optionText = optionText;
        this.text = text;
        this.option1 = option1;
        this.option2 = option2;
        this.isLeaf = false;
    }

    public DialogueNode(string text, DialogueNode option1, DialogueNode option2)
    {
        this.text = text;
        this.option1 = option1;
        this.option2 = option2;
        this.isLeaf = false;
    }

    public DialogueNode(string optionText, string text)
    {
        this.optionText = optionText;
        this.text = text;
        this.isLeaf = true;
    }


    public string optionText { get; }
    public string text { get; }
    public DialogueNode option1 { get; }
    public DialogueNode option2 { get; }
    public bool isLeaf { get; }
}


public class DialogueTree
{
    public DialogueTree(DialogueNode initialNode)
    {
        this.initialNode = initialNode;
        this.currentNode = this.initialNode;
    }

    public enum OptionChoice
    {
        First,
        Second
    }

    public void ChooseOption(OptionChoice choice)
    {
        if (currentNode.isLeaf) throw new System.InvalidOperationException("Current node is leaf");
        switch (choice)
        {
            case OptionChoice.First:
                currentNode = currentNode.option1;
                break;

            case OptionChoice.Second:
                currentNode = currentNode.option2;
                break;
        }
    }

    private DialogueNode initialNode;
    public DialogueNode currentNode { get; private set; }
}

internal static class DialogueNodes
{
    public static DialogueNode dialogue1 = new DialogueNode
    (
        "अनुच्छेद 1 — सभी मनुष्यों को गौरव और अधिकारों के मामले में जन्मजात स्वतन्त्रता",
        new DialogueNode(
            "विकल्प 1",
            "और समानता प्राप्त है। उन्हें बुद्धि और अन्तरात्मा की देन प्राप्",
            new DialogueNode("विकल्प 1", "परिदृश्य का अंत 1."),
            new DialogueNode("विकल्प 2", "परिदृश्य का अंत 2.")
        ),
        new DialogueNode(
            "विकल्प 2",
            "त है और परस्पर उन्हें भाईचारे के भाव से बर्ताव करना चाहिये।",
            new DialogueNode("विकल्प 1", "परिदृश्य का अंत 3."),
            new DialogueNode("विकल्प 2", "परिदृश्य का अंत 4.")
        )
    );
}

public static class DialogueTreeFactory
{
    public static DialogueTree CreateVillageDialogue()
    {
        return new DialogueTree(DialogueNodes.dialogue1);
    }
}

