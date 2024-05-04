using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Lists/Chance")]
public class ChanceEventsListSO : ScriptableObject
{
    public List<string> texts;
    public List<int> effects;
}
