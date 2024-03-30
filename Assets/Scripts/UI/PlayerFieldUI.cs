using System.Linq;
using UnityEngine;

public class PlayerFieldUI : MonoBehaviour
{
    [SerializeField] private Transform pawnPreview;
    [SerializeField] private Sprite[] pawnVisuals; // all pawns + 1 random option at the end

    private int selectedPawn;
    private bool[] imagesAvailability;

    private PawnPreviewUI pawnPreviewScript;

    void Awake()
    {
        selectedPawn = pawnVisuals.Length - 1;

        imagesAvailability = new bool[pawnVisuals.Length].Select(x => true).ToArray();

        pawnPreviewScript = pawnPreview.GetComponent<PawnPreviewUI>();
    }

    void Start()
    {
        pawnPreviewScript.SetPreview(pawnVisuals[^1]);
        pawnPreviewScript.FillImageList(pawnVisuals);
        pawnPreviewScript.OnOptionSelected += SaveSelectedOption;
    }

    private void SaveSelectedOption(int option)
    {
        pawnPreviewScript.SetPreview(pawnVisuals[option]);

        if (option != pawnVisuals.Length - 1)
        {
            imagesAvailability[option] = false;
        }

        imagesAvailability[selectedPawn] = true;
        selectedPawn = option;

        pawnPreviewScript.UpdateAvailability(imagesAvailability);
    }
}
