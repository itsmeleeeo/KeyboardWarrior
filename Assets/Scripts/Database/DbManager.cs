using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DbManager
{
    public static string username;
    public static int score;

    public static bool loggedIn {get {return username != null;}}

    public static void Logout() {
        username = null;
    }
}
