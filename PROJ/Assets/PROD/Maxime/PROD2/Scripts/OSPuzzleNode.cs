using UnityEngine;
using UnityEngine.UI;

public class OSPuzzleNode : MonoBehaviour
{
    public int number;
    [SerializeField] private bool isSelected;
    [SerializeField] private RawImage image;
    [SerializeField] private Texture tex;
    [SerializeField] private Button button;

    private Color deselectedColor = new Color(1, 1, 1, 0.1f);

    private void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<RawImage>();
        Initialize();
    }

    public void SelectPuzzleNode()
    {
        image.color = Color.green;
        isSelected = true;
    }

    public void DeselectPuzzleNode()
    {
        image.color = deselectedColor;
        isSelected = false;
    }

    public bool GetSelected()
    {
        return isSelected;
    }

    public void Initialize()
    {
        button.interactable = true;
        image.texture = tex;
        if (number == 2)
        {
            image.color = Color.green;
            isSelected = true;
        }
        else
            image.color = deselectedColor;
    }
}
