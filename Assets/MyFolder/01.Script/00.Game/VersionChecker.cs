using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VersionChecker : MonoBehaviour
{
    private void OnEnable()
    {
        TryGetComponent(out TextMeshProUGUI text);
        text.text = Application.version;
    }
}
