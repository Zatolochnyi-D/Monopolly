using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DirectorsInteractionUI : InteractionUI
{
    private static List<Director> allDirectors;

    [SerializeField] private GameObject pickDirectorScreen;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Transform cardsRoot;
    [SerializeField] private Transform cardTemplate;

    [SerializeField] private Button leaveButton;

    [SerializeField] private List<Director> directors;

    private PlayerLogic player;
    private List<string> availableDirectors = new();

    void Awake()
    {
        allDirectors = directors;
        cardTemplate.gameObject.SetActive(false);

        cancelButton.onClick.AddListener(() =>
        {
            ToDirectorBoard();
        });

        leaveButton.onClick.AddListener(() =>
        {
            Close();
        });
    }

    public override void Interact()
    {
        player = TurnManager.Instance.CurrentPlayer;

        DeleteCards();
        pickDirectorScreen.SetActive(true);
        CollectAvailableDirectors();
        AddCards();

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
    }

    private void AddCards()
    {
        foreach (string name in availableDirectors)
        {
            try
            {
                Director director = allDirectors.First(x => x.name == name);
                Transform card = Instantiate(cardTemplate, cardsRoot);
                DirectorCardUI script = card.GetComponent<DirectorCardUI>();
                script.SetInfo(director.name, director.image, director.power);
                script.OnClick += GetDirector;
                card.gameObject.SetActive(true);
            }
            catch { }
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