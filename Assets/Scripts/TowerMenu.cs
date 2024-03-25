using UnityEngine;

using UnityEngine.UIElements;



public class TowerMenu : MonoBehaviour

{

    private Button archerButton;

    private Button swordButton;

    private Button wizardButton;

    private Button updateButton;

    private Button destroyButton;



    private VisualElement root;
    private ConstructionSite selectedSite;


    void Start()

    {

        root = GetComponent<UIDocument>().rootVisualElement;



        archerButton = root.Q<Button>("archer-button");

        swordButton = root.Q<Button>("sword-button");

        wizardButton = root.Q<Button>("wizard-button");

        updateButton = root.Q<Button>("button-upgrade");

        destroyButton = root.Q<Button>("button-destroy");



        if (archerButton != null)

        {

            archerButton.clicked += OnArcherButtonClicked;

        }



        if (swordButton != null)

        {

            swordButton.clicked += OnSwordButtonClicked;

        }



        if (wizardButton != null)

        {

            wizardButton.clicked += OnWizardButtonClicked;

        }



        if (updateButton != null)

        {

            updateButton.clicked += OnUpdateButtonClicked;

        }



        if (destroyButton != null)

        {

            destroyButton.clicked += OnDestroyButtonClicked;

        }



        root.visible = false;

    }



    private void OnArcherButtonClicked()

    {



    }



    private void OnSwordButtonClicked()

    {



    }



    private void OnWizardButtonClicked()

    {



    }



    private void OnUpdateButtonClicked()

    {



    }



    private void OnDestroyButtonClicked()

    {



    }
    public void EvaluateMenu()
    {
        if (selectedSite == null)
        {
            return;
        }

        int siteLevel = (int)selectedSite.Level;

        archerButton.SetEnabled(false);
        wizardButton.SetEnabled(false);
        swordButton.SetEnabled(false);
        updateButton.SetEnabled(false);
        destroyButton.SetEnabled(false);

        switch (siteLevel)
        {
            case 0:
                archerButton.SetEnabled(true);
                wizardButton.SetEnabled(true);
                swordButton.SetEnabled(true);
                break;
            case 1:
            case 2:
                updateButton.SetEnabled(true);
                destroyButton.SetEnabled(true);
                break;
            case 3:
                destroyButton.SetEnabled(true);
                break;
            default:
                Debug.LogError("Invalid site level: " + selectedSite.Level);
                break;
        }
    }
    public void SetSite(ConstructionSite site)
    {
        selectedSite = site;

        if (selectedSite == null)
        {
            root.visible = false;
            return;
        }

        root.visible = true;
        EvaluateMenu();
    }
    private void OnDestroy()

    {

        if (archerButton != null)

        {

            archerButton.clicked -= OnArcherButtonClicked;

        }



        if (swordButton != null)

        {

            swordButton.clicked -= OnSwordButtonClicked;

        }



        if (wizardButton != null)

        {

            wizardButton.clicked -= OnWizardButtonClicked;

        }



        if (updateButton != null)

        {

            updateButton.clicked -= OnUpdateButtonClicked;

        }



        if (destroyButton != null)

        {

            destroyButton.clicked -= OnArcherButtonClicked;

        }

    }
}