using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro; 


public class GameManager : MonoBehaviour
{
    public Slider healthBar;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI flavorText;
    public Button poisonButton1;
    public Button poisonButton2;

    private float health = 100f; //hp
    private float timer = 30f; // 30 seconds to survive

    public TextMeshProUGUI flavorText1; // first text UI
    public TextMeshProUGUI flavorText2; // second text UI

    IEnumerator textAnim(TextMeshProUGUI textComponent, string text, float typingSpeed)
    {
        textComponent.text = ""; // clears text
        foreach (char letter in text.ToCharArray())
        {
            textComponent.text += letter; // gives typewriter effect
            yield return new WaitForSeconds(typingSpeed); // waits before adding letters
        }
    }

    void Start()
    {
        UpdateUI();
        GenerateChoices();
    }

    void Update()
    {
        // decreases timer
        timer -= Time.deltaTime;
        if (timer <= 0 || health <= 0)
        {
            EndGame();
        }
        UpdateUI();
    }

    void GenerateChoices()
    {
        // Random poison effects
        int poison1Damage = Random.Range(5, 20);
        int poison2Damage = Random.Range(10, 30);

        // set button texts
        poisonButton1.GetComponentInChildren<TextMeshProUGUI>().text = $"Poison 1: -{poison1Damage} Health";
        poisonButton2.GetComponentInChildren<TextMeshProUGUI>().text = $"Poison 2: -{poison2Damage} Health";

        // button actions
        poisonButton1.onClick.RemoveAllListeners();
        poisonButton1.onClick.AddListener(() => ChoosePoison(poison1Damage));

        poisonButton2.onClick.RemoveAllListeners();
        poisonButton2.onClick.AddListener(() => ChoosePoison(poison2Damage));

        // Generate flavor texts
        string[] texts = GenerateFlavorText();

        flavorText1.text = texts[0]; 
        flavorText2.text = texts[1]; 

    StartCoroutine(textAnim(flavorText1, texts[0], 0.05f)); 
    StartCoroutine(textAnim(flavorText2, texts[1], 0.05f)); 
    }

    void ChoosePoison(int damage)
    {
        // random chance for positive effects (health gain or time addition)
        float randomChance = Random.Range(0f, 1f); // float between 0 and 1

        if (randomChance < 0.25f) // 25% chance for hp
        {
            int healthGain = Mathf.FloorToInt(Random.Range(5f, 15f)); // health gain between 5 and 15
            health += healthGain;
            flavorText.text = $"You gained {healthGain} health!";
        }
        else if (randomChance < 0.5f) // 25% chance for time
        {
            int timeGain = Mathf.FloorToInt(Random.Range(5f, 10f)); // time gain between 5 and 10 seconds
            timer += timeGain;
            flavorText.text = $"You gained {timeGain} seconds!"; 
        }
        else
        {
            // Normal poison damage
            int poisonDamage = Mathf.FloorToInt(damage); // makes the texts show as whole numbers
            health -= poisonDamage;
            flavorText.text = $"You lost {poisonDamage} health!"; 
        }
        GenerateChoices(); // Generate new choices after drinking the poison
    }

    void UpdateUI()
    {
        // update health bar and timer
        healthBar.value = health / 100f;
        timerText.text = $"Time: {Mathf.Ceil(timer)}";
    }

void EndGame()
{
    flavorText.gameObject.SetActive(true); // Game over text shows up

    // change the text of the flavorText component
    flavorText.text = "Game Over!";

    // disables interaction with the poison buttons
    poisonButton1.interactable = false;
    poisonButton2.interactable = false;
}

    string[] flavorTexts = {
    "A green liquid that glows in the dark.",
    "A shady cupcake covered in sprinkles.",
    "A bubbling red potion contained in a vial.",
    "A viscous slime seeping from a bottle.",
    "A purple drink that smells of sunflowers.",
    "A steaming black drink that smells terrible."
};

    string[] GenerateFlavorText()
    {
        int index1 = Random.Range(0, flavorTexts.Length);
        int index2;

        // Makes sure the flavor texts are never the same
        do
        {
            index2 = Random.Range(0, flavorTexts.Length);
        } while (index2 == index1);

        return new string[] { flavorTexts[index1], flavorTexts[index2] };
    }
}

