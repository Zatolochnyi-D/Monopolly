// using System.Collections.Generic;
// using UnityEngine;

// public class GamePreparationManager : MonoBehaviour
// {
//     public static GamePreparationManager Instance { get; private set; }

//     [SerializeField] private GameObject playerPrefab;
//     [SerializeField] private PawnVisualsSO[] pawnVisuals;

//     private List<PlayerLogic.PlayerBuilder> builders; 

//     void Awake()
//     {
//         Instance = this;

//         builders = new();

//         string[] names = new[]
//         {
//             "Uranium",
//             "Ferum",
//             "Cobalt",
//             "Zirconium"
//         };

//         // create test players
//         for (int i = 0; i < 4; i++)
//         {
//             builders.Add(new());
//             builders[i].SetPosition(0);
//             builders[i].SetName(names[i]);
//             builders[i].SetNumber(5);
//             builders[i].SetVisuals(pawnVisuals[i]);
//         }
//     }

//     public GameObject[] GetPlayers()
//     {
//         GameObject[] players = new GameObject[builders.Count];

//         for (int i = 0; i < players.Length; i++)
//         {
//             // players[i] = builders[i].GetProduct();
//         }

//         return players;
//     }
// }
