using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    public void ShowMessage(string message)
    {
        _text.text = "";
        _text.text = message;
    }
}
