using System.Collections.Generic;
using Recipes;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class TestQTEController : MonoBehaviour
{
    private readonly List<BaseRecipe> _cookbook = new List<BaseRecipe>
    {
        new SimpleRecipe()
    };

    private readonly List<QTEButtons.QTEInput> _availableCommands = new List<QTEButtons.QTEInput>
    {
        QTEButtons.U_KEY, QTEButtons.J_KEY, QTEButtons.I_KEY, QTEButtons.K_KEY
    };

    private Text _descriptionField;
    public GameObject recipeDescriptionText;
    public GameObject actionListPanel;
    public GameObject textHolderPrefab;

    private void Awake()
    {
        // We have to avoid to do this in the Update function
        _descriptionField = recipeDescriptionText.GetComponent<Text>();
    }

    private void Start()
    {
        Debug.Log("Let's go !");
        var recipe = _cookbook[new Random().Next(_cookbook.Count)];

        var qteSystem = gameObject.AddComponent<QTESystem>();
        qteSystem.StartRecipe(recipe);

        _descriptionField.text = recipe.Description;

        UpdateActionListPanel();
    }

    private void UpdateActionListPanel()
    {

    }
}