using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TenderInteraction : Interaction
{
    public static IObserver OnPlayerEnterTender = new TenderEvent();

    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI difficultyText;
    [SerializeField] private GameObject warning;
    [SerializeField] private TextMeshProUGUI rolledNumberText;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI endScreenText;
    [SerializeField] private Button endScreenButton;

    [SerializeField] private int difficulty;
    [SerializeField] private int income;
    private int cost;

    private NumberRoller roller;
    private PlayerLogic player;

    void Awake()
    {
        confirmButton.onClick.AddListener(() =>
        {
            Try();
        });

        cancelButton.onClick.AddListener(() =>
        {
            Close();
        });

        endScreenButton.onClick.AddListener(() =>
        {
            Close();
        });

        cost = difficulty * 10;
        roller = new(rolledNumberText, Enumerable.Range(2, 11).ToArray());
        playerCommand = new PlayerLogic.AlterBalanceCommand()
        {
            NextCommand = new PlayerLogic.AlterPassiveIncomeCommand()
            {
                Parameters = new PlayerLogic.SimpleIntegerParam() { integer = income }
            },
            Parameters = new PlayerLogic.SimpleIntegerParam() { integer = -cost }
        };
    }

    void OnDestroy()
    {
        OnPlayerEnterTender = new TenderEvent();
    }

    public override void Interact()
    {
        player = TurnManager.Instance.CurrentPlayer;
        playerCommand.TargetPlayer = player;
        playerCommand.NextCommand.TargetPlayer = player;

        costText.text = $"Cost: {cost}00$";
        difficultyText.text = $"Difficulty: {difficulty}";

        warning.SetActive(false);
        rolledNumberText.gameObject.SetActive(false);
        endScreen.SetActive(false);

        OnPlayerEnterTender.Notify();

        Show();
    }

    private async void Try()
    {
        if (player.Money < cost)
        {
            warning.SetActive(true);
            return;
        }

        int rolledNumber = await roller.Roll();

        endScreen.SetActive(true);

        if (rolledNumber > difficulty)
        {
            endScreenText.text = "You won and now have factory here.";
            playerCommand.Execute();
        }
        else
        {
            endScreenText.text = "You loose and can't have factory here";
        }
    }
}


public class TenderEvent : IObserver
{
    private List<ISubscriber> subscribers = new();

    public void AddSubscriber(ISubscriber subscriber)
    {
        subscribers.Add(subscriber);
    }

    public void RemoveSubscriber(ISubscriber subscriber)
    {
        subscribers.Remove(subscriber);
    }

    public void Notify()
    {
        foreach (ISubscriber s in subscribers)
        {
            s.React();
        }
    }
}