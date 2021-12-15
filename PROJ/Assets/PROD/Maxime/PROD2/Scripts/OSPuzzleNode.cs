using UnityEngine;
using UnityEngine.UI;

public class OSPuzzleNode : MonoBehaviour
{
    public int number;
    [SerializeField] private bool isSelected;
    [SerializeField] private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SelectPuzzleNode()
    {
        image.color = Color.green;
        isSelected = true;
    }

    public void DeselectPuzzleNode()
    {
        image.color = Color.white;
        isSelected = false;
    }

    public bool GetSelected()
    {
        return isSelected;
    }
}
