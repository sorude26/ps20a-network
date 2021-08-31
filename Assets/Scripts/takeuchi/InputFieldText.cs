using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldText : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] InputField inputField;
    public void RoomNameSet()
    {
        text.text = inputField.text;
    }
}
