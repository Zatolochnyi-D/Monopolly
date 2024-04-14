using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.UI;

public class DirectorsInteractionUI : InteractionUI
{
    private static readonly int winScore = 36; 

    private static List<Director> allDirectors;

    [SerializeField] private GameObject pickDirectorScreen;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Transform cardsRoot;
    [SerializeField] private Transform cardTemplate;

    [SerializeField] private Button endGameButton;
    [SerializeField] private Button leaveButton;

    [SerializeField] private GameObject endGameScreen;
    [SerializeField] private TextMeshProUGUI endGameText;
    [SerializeField] private Button cancelEndGameButton;

    [SerializeField] private Transform availableCards;
    [SerializeField] private Transform availableCardTemplate;
    [SerializeField] private Transform usedCards;
    [SerializeField] private Transform usedCardTemlplate;

    [SerializeField] private GameObject finalScoreScreen;
    [SerializeField] private TextMeshProUGUI finalScore;
    [SerializeField] private Button finalScoreCloseButton;

    [SerializeField] private List<Director> directors;

    private PlayerLogic player;
    private List<string> availableDirectors = new();
    private List<int> rolledScores = new();
    private int amountOfCardsInEndGame = 0;

    void Awake()
    {
        allDirectors = directors;
        cardTemplate.gameObject.SetActive(false);
        availableCardTemplate.gameObject.SetActive(false);
        usedCardTemlplate.gameObject.SetActive(false);

        cancelButton.onClick.AddListener(() =>
        {
            ToDirectorBoard();
        });

        leaveButton.onClick.AddListener(() =>
        {
            Close();
        });

        endGameButton.onClick.AddListener(() => 
        {
            ToEndGameScreen();
        });

        cancelEndGameButton.onClick.AddListener(() =>
        {
            endGameScreen.SetActive(false);
        });

        finalScoreCloseButton.onClick.AddListener(() =>
        {
            Close();
        });
    }

    public override void Interact()
    {
        player = TurnManager.Instance.CurrentPlayer;

        endGameScreen.SetActive(false);
        cancelEndGameButton.gameObject.SetActive(true);
        finalScoreScreen.SetActive(false);

        DeleteCards();
        pickDirectorScreen.SetActive(true);
        CollectAvailableDirectors();
        AddCards();
        rolledScores.Clear();

        if (availableDirectors.Count == 0)
        {
            ToDirectorBoard();
        }

        Show();
    }

    private void DeleteCards()
    {
        foreach (Transform child in cardsRoot)
        {
            if (child == cardTemplate) continue;
            Destroy(child.gameObject);
        }
    }

    private void CollectAvailableDirectors()
    {
        availableDirectors.Clear();

        if (player.Shares.Airlines > 50) availableDirectors.Add("Airlines");

        if (player.Shares.CarManufacturer > 50) availableDirectors.Add("Car Manufacturer");

        if (player.Shares.TourismAgency > 50) availableDirectors.Add("Tourism Agency");

        if (player.Shares.TVCompany > 50) availableDirectors.Add("TV Company");

        if (player.Shares.BuildingAgency > 50) availableDirectors.Add("Building Agency");

        if (player.Shares.BookPublisher > 50) availableDirectors.Add("Book Publisher");

        availableDirectors.RemoveAll(x => allDirectors.FirstOrDefault(y => y.name == x) == default);
    }

    private void AddCards()
    {
        foreach (string name in availableDirectors)
        {
            Director director = allDirectors.First(x => x.name == name);
            Transform card = Instantiate(cardTemplate, cardsRoot);
            DirectorCardUI script = card.GetComponent<DirectorCardUI>();
            script.SetInfo(director.name, director.image, director.power);
            script.OnClick += GetDirector;
            card.gameObject.SetActive(true);
        }
    }

    private void GetDirector(string name)
    {
        Director director = allDirectors.First(x => x.name == name);

        player.Directors.Add(director);
        allDirectors.RemoveAll(x => x == director);

        Close();
    }

    private void ToDirectorBoard()
    {
        pickDirectorScreen.SetActive(false);
    }

    private void ToEndGameScreen()
    {
        endGameScreen.SetActive(true);
        endGameText.text = $"With directors and luck, you can try to win this game. You need {winScore} points.";

        amountOfCardsInEndGame = player.Directors.Count;

        foreach (Transform child in availableCards)
        {
            if (child == availableCardTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (Transform child in usedCards)
        {
            if (child == usedCardTemlplate) continue;
            Destroy(child.gameObject);
        }

        foreach (Director director in player.Directors)
        {
            Transform card = Instantiate(availableCardTemplate, availableCards);
            var script = card.GetComponent<DirectorCardUI>();
            script.SetInfo(director.name, director.image, director.power);
            script.OnClick += RollForDirector;
            card.gameObject.SetActive(true);
        }
    }

    private async void RollForDirector(string name)
    {
        cancelEndGameButton.gameObject.SetActive(false);

        foreach (Transform child in availableCards)
        {
            if (child.GetComponent<DirectorCardUI>().Text == name)
            {
                Destroy(child.gameObject);
                break;
            }
        }
        Director director = player.Directors.First(x => x.name == name);

        Transform cardAndText = Instantiate(usedCardTemlplate, usedCards);
        Transform card = cardAndText.GetChild(0);
        var script = card.GetComponent<DirectorCardUI>();
        script.SetInfo(director.name, director.image, director.power);
        var text = cardAndText.GetChild(1).GetComponent<TextMeshProUGUI>();
        cardAndText.gameObject.SetActive(true);

        int rolledNumber = await Roll(text, Enumerable.Range(1, 6).ToArray());

        int score = rolledNumber * director.power;
        text.text = $"{score}";
        rolledScores.Add(score);

        amountOfCardsInEndGame--;

        TryCalculateFinalScore();
    }

    private async Task<int> Roll(TextMeshProUGUI displayText, int[] range)
    {
        int rolledNumber = 0;
        displayText.gameObject.SetActive(true);
        for (int i = 0; i < 20; i++)
        {
            rolledNumber = range[UnityEngine.Random.Range(0, range.Length)];
            displayText.text = rolledNumber.ToString();
            await Task.Delay(100);
        }

        await Task.Delay(1000);

        return rolledNumber;
    }

    private void TryCalculateFinalScore()
    {
        if (amountOfCardsInEndGame == 0)
        {
            int sum = rolledScores.Sum();
            finalScore.text = $"Your final score: {sum}";
            finalScoreScreen.SetActive(true);

            if (sum >= winScore)
            {
                // declare player victory.

                Debug.Log("Victory!");
            }
            else
            {
                allDirectors.AddRange(player.Directors);
                player.Directors.Clear();
            }
        }
    }

    private void Close()
    {
        Hide();
        EndTurn();
    }
}


[Serializable]
public struct Director
{
    public string name;
    public Sprite image;
    public int power;

    public static bool operator ==(Director a, Director b)
    {
        if (a.name != b.name)
        {
            return false;
        }
        if (a.image != b.image)
        {
            return false;
        }
        if (a.power != b.power)
        {
            return false;
        }
        return true;
    }

    public static bool operator !=(Director a, Director b)
    {
        return !(a == b);
    }
}