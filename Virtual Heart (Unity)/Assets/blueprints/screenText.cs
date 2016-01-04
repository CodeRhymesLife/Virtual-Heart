using UnityEngine;
using System.Collections;

public class screenText : MonoBehaviour {

    public string DefaultText = "Virtual Heart";
    public int MaxCharsPerLine;

    private TextMesh _textMesh;

	// Use this for initialization
	void Start () {
        _textMesh = GetComponent<TextMesh>();

        ShowText(DefaultText);

        // Show organ description when it's selected
        organPart.AnOrganPartHighlighted += o =>
        {
            ShowText(
                "FMA Id: " + o.Metadata.FMAId,
                "Description: " + o.Metadata.Description
            );
        };

        // Remove organ desicription when it's selected
        organPart.AnOrganPartUnHighlighted += o =>
        {
            ShowText(DefaultText);
        };
    }

    /// <summary>
    /// Show the text wordwrapped
    /// </summary>
    /// <param name="lines">Lines of tet to show</param>
    private void ShowText(params string[] lines)
    {
        string result = "";
        foreach (string line in lines)
        {
            int charCount = 0;
            string[] words = line.Split(' ');

            for (int wordIndex = 0; wordIndex < words.Length; wordIndex++)
            {
                string word = words[wordIndex].Trim();
                charCount += word.Length + 1;

                if (charCount > MaxCharsPerLine)
                {
                    word += "\n";
                    charCount = 0;
                }
                else
                    word += " ";

                result += word;
            }

            result += "\n\n";
        }

        _textMesh.text = result;
    }
}
