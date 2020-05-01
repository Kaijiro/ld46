using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class InputFeedbackDisplayer : MonoBehaviour
{
    public Text CInputText;
    public Text SInputText;
    public Text MInputText;
    public Text GInputText;
    public Text OInputText;
    public Text AInputText;
    public Text PInputText;

    public Color rightInputColor;
    public Color wrongInputColor;

    private Dictionary<string, Text> _mapping;

    // Start is called before the first frame update
    void Start()
    {
        _mapping = new Dictionary<string, Text>{
            {"QTE_Input_1", CInputText},
            {"QTE_Input_2", SInputText},
            {"QTE_Input_3", MInputText},
            {"QTE_Input_4", GInputText},
            {"QTE_Input_5", OInputText},
            {"QTE_Input_6", AInputText},
            {"QTE_Input_7", PInputText},
        };

        GameEvents.Instance.OnGoodInputPressed += OnGoodInputPressed;
        GameEvents.Instance.OnWrongInputPressed += OnWrongInputPressed;
    }

    private Text GetTextHelpForInputPressed()
    {
        var helpText = _mapping.Where(pair => Input.GetButtonDown(pair.Key))
            .Select(pair => pair.Value)
            .FirstOrDefault();
        
        return helpText;
    }

    private void OnWrongInputPressed()
    {
        DisplayInputFeedback(wrongInputColor);
    }

    private void OnGoodInputPressed()
    {
        DisplayInputFeedback(rightInputColor);
    }

    private void DisplayInputFeedback(Color inputFeedbackColor)
    {
        var helpText = GetTextHelpForInputPressed();

        if (helpText != null)
        {
            var outline = helpText.gameObject.GetComponent<Outline>();
            outline.effectColor = inputFeedbackColor;
            StartCoroutine(AnimateOutline(outline));
        }
    }

    IEnumerator AnimateOutline(Outline outline)
    {
        const int maxLoopIteration = 15;
        float initialColorAlpha = outline.effectColor.a;

        for (int i = 0; i <= maxLoopIteration; i++)
        {
            var effectDistanceVector = outline.effectDistance;
            var outlineColor = outline.effectColor;

            float newAlpha = initialColorAlpha - (initialColorAlpha * i / maxLoopIteration);

            outlineColor.a = newAlpha;
            effectDistanceVector.y = i;
            outline.effectColor = outlineColor;
            outline.effectDistance = effectDistanceVector;
            
            yield return new WaitForSeconds(0.02f);
        }
    }
}
