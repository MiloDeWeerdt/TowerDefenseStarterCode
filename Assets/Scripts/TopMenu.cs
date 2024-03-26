using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopMenu : MonoBehaviour
{
    public static TopMenu instance;

    public Label waveLabel;
    public Label creditsLabel;
    public Label healthLabel;
    public Button startWaveButton;
    private void Start()
    {
        
        var root = GetComponent<UIDocument>().rootVisualElement;
        waveLabel = root.Q<Label>("waveLabel");
        creditsLabel = root.Q<Label>("creditsLabel");
        healthLabel = root.Q<Label>("healthLabel");
        startWaveButton = root.Q<Button>("startWaveButton");
        startWaveButton.clicked += OnStartWaveButtonClicked;
        UpdateTopMenuLabels(GameManager.instance.GetCredits(), GameManager.instance.GetHealth(), GameManager.instance.GetCurrentWaveIndex());
    }
    // Functie om de labels bij te werken
    public void UpdateTopMenuLabels(int credits, int health, int wave)
    {
        Debug.Log("Updating top menu labels: Credits: " + credits + ", Health: " + health + ", Wave: " + wave);
        creditsLabel.text = "Credits: " + credits; 
        healthLabel.text = "Health: " + health;
        waveLabel.text = "Wave: " + wave;
    }
    public void SetCreditsLabel(string text)
    {
        creditsLabel.text = text;
    }
    public void SetHealthLabel(string text)
    {
        healthLabel.text = text;
    }
    public void SetWaveLabel(string text)
    {
        waveLabel.text = text;
    }
    private void OnStartWaveButtonClicked()
    {
        int currentWaveIndex = GameManager.instance.GetCurrentWaveIndex();
        int nextWaveIndex = currentWaveIndex + 1;
        GameManager.instance.StartWave(nextWaveIndex);
    }
}
