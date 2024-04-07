using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Lists/Image")]
public class ImageEventsListSO : ScriptableObject
{
    public List<string> texts;
    public List<Vector2Int> numbers;
}
