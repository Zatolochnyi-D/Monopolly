using System;
using UnityEngine;
using UnityEngine.UI;

public class PopUpUIC : MonoBehaviour
{
    [SerializeField] private Button confirm;
    [SerializeField] private Button cancel;

    public Action actionOnConfirm;

    void Awake()
    {
        confirm.onClick.AddListener(() =>
        {
            actionOnConfirm?.Invoke();
        });
        cancel.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });

        gameObject.SetActive(false);
    }
}
