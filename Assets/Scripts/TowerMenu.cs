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

        GameManager.Instance.Build(TowerType.Archer, SiteLevel.Level1);

    }



    private void OnSwordButtonClicked()

    {

        GameManager.Instance.Build(TowerType.Sword, SiteLevel.Level1);

    }



    private void OnWizardButtonClicked()

    {

        GameManager.Instance.Build(TowerType.Wizard, SiteLevel.Level1);

    }



    private void OnUpdateButtonClicked()

    {
        if (selectedSite != null)
        {
            SiteLevel nextLevel = selectedSite.Level + 1; 
            GameManager.Instance.Build(selectedSite.TowerType, nextLevel);
        }

    }



    private void OnDestroyButtonClicked()

    {

        if (selectedSite == null)
        {
            return;
        }
        selectedSite.SetTower(null, SiteLevel.Level0, TowerType.Onbebouwd);

    }
    public void EvaluateMenu()
    {
        if (selectedSite == null)
        {
            return;
        }

        int siteLevel = (int)selectedSite.Level;
        int cost = GameManager.Instance.GetCost(selectedSite.TowerType, selectedSite.Level);
        archerButton.SetEnabled(false);
        wizardButton.SetEnabled(false);
        swordButton.SetEnabled(false);
        updateButton.SetEnabled(false);
        destroyButton.SetEnabled(false);

        switch (siteLevel)
        {
            case 0:
                if (GameManager.Instance.GetCredits() >= cost)
                {
                    archerButton.SetEnabled(true);
                    wizardButton.SetEnabled(true);
                    swordButton.SetEnabled(true);
                }
                break;
            case 1:
            case 2:
                if (GameManager.Instance.GetCredits() >= cost)
                {
                    updateButton.SetEnabled(true);
                }
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