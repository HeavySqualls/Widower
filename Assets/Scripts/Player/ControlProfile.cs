using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlProfile : MonoBehaviour
{
    // GAMEPAD

    public string Horizontal_Gamepad;
    public string Vertical_Gamepad;
    public string X_Gamepad;
    public string Y_Gamepad;
    public string Gamepad_Start;
    public string X_Button;
    public string O_Button;
    public string Sprint_Gamepad;

    // KEYBOARD

    public string Horizontal;
    public string Vertical;
    public string Mouse_X;
    public string Mouse_Y;
    public string Enter_Key;
    public string Sprint_Key;
    public string Eat_Key;
    public string QuitEat_Key;


    public void ControlProfile1()
    {
        // Gamepad
        Horizontal_Gamepad = "Horizontal_Gamepad";
        Vertical_Gamepad = "Vertical_Gamepad";
        X_Gamepad = "Gamepad_X";
        Y_Gamepad = "Gamepad_Y";
        Gamepad_Start = "STARTButton";
        X_Button = "XButton";
        O_Button = "BButton";
        Sprint_Gamepad = "Sprint_Gamepad";

        // Keyboard
        Horizontal = "Horizontal";
        Vertical = "Vertical";
        Mouse_X = "Mouse X";
        Mouse_Y = "Mouse Y";
        Enter_Key = "return";
        Sprint_Key = "left shift";
        Eat_Key = "e";
        QuitEat_Key = "q";
    }


    public void ControlProfile2()
    {
        // Gamepad
        Horizontal_Gamepad = "Horizontal_Gamepad2";
        Vertical_Gamepad = "Vertical_Gamepad2";
        X_Gamepad = "Gamepad_X2";
        Y_Gamepad = "Gamepad_Y2";
        Gamepad_Start = "STARTButton2";
        X_Button = "XButton2";
        O_Button = "BButton2";
        Sprint_Gamepad = "Sprint_Gamepad2";

        // Keyboard
        Horizontal = "Horizontal2";
        Vertical = "Vertical2";
        Mouse_X = "Mouse X2";
        Mouse_Y = "Mouse Y2";
        Enter_Key = "backspace";
        Sprint_Key = "right shift";
        Eat_Key = "end";
        QuitEat_Key = "delete";
    }
}
