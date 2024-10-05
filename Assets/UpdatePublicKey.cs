using UnityEngine;
using TMPro;

public class UpdatePublicKey : MonoBehaviour
{
    // Tham chiếu tới TextMeshProUGUI
    public TextMeshProUGUI PublicKeyText;

    private void Start()
    {
        string publicKey = PlayerPrefs.GetString("PublicKey", "No PublicKey");
        PublicKeyText.text = publicKey;
    }
}
