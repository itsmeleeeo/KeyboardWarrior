using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RegisterMenu : MonoBehaviour
{
    public TMP_Text username,password,email,passwordConfirmation,phoneNumber;
    private string usernameText,passwdText,emailText;
    private int phone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterBtn(){
        if(password.text == passwordConfirmation.text){
            usernameText = username.text;
            passwdText = password.text;
            emailText = email.text;
            phone = int.Parse(phoneNumber.text);
        }
    }
}
