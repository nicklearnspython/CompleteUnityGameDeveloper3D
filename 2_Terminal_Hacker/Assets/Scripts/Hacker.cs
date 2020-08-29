using System.Collections;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    // Game Configuration Data
    string[] level1Passwords = { "Luke", "Ben", "Jabba", "Boba", "Force" };
    string[] level2Passwords = { "Lando", "Vader", "Father", "Dagobah", "Empire" };
    string[] level3Passwords = { "Stardust", "Darksaber", "Kyber", "Coruscant", "Gerrera" };
    
    // Game State
    int level;
    string password;

    enum Screen { MainMenu, Password, Win };
    Screen currentScreen = Screen.MainMenu;

    // Intro messages
    const string message1 = "Hello Rebel. We need your help. There\nis vulnerability in the Imperial\nsecurity protocol.";
    const string message2 = "Hack into these Imperial security\nterminals. Retrieve vital data to help the destruction of the Empire.";
    const string message3 = "Choose which security vulnerability to\nhack:\n" +
        "1 - Tatooine Patrol Station (Easy)\n" +
        "2 - Bespin Occupation Force (Medium)\n" +
        "3 - Scarif Security Complex (Hard)";
    const string menuMessage = "You may type 'menu' at anytime.";

    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
    }
    
    void ShowMainMenu()
    {
        StartCoroutine(IntroMessage());
    }

    IEnumerator IntroMessage()
    {
        Terminal.ClearScreen();
        
        Terminal.WriteLine(message1);
        yield return new WaitForSeconds(1);
        Terminal.WriteLine(message2);
        yield return new WaitForSeconds(1);
        Terminal.WriteLine(message3);
        Terminal.WriteLine(menuMessage);
    }

    void OnUserInput(string input)
    {
        if (input.ToLower() == "menu") // we can always go direct to main menu
        {
            currentScreen = Screen.MainMenu;
            ShowMainMenu();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            RunPassword(input);
        }
    }

    private void RunMainMenu(string input)
    {
        Terminal.ClearScreen();

        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            AskForPassword();
        }
        else
        {
            Terminal.WriteLine(input);
            Terminal.WriteLine("Select a valid level number.");
            Terminal.WriteLine(menuMessage);
            Terminal.WriteLine(message3);
        }
    }

    void AskForPassword()
    {
        currentScreen = Screen.Password;
        SetRandomPassword();

        Terminal.ClearScreen();
        Terminal.WriteLine("Enter your password: ");
        Terminal.WriteLine("hint: " + password.ToLower().Anagram());
    }

    void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Tatooine Patrol Station");
                password = level1Passwords[Random.Range(0, level1Passwords.Length)];
                break;
            case 2:
                Terminal.WriteLine("Bespin Occupation Force");
                password = level2Passwords[Random.Range(0, level2Passwords.Length)];
                break;
            case 3:
                Terminal.WriteLine("Scarif Security Complex");
                password = level3Passwords[Random.Range(0, level3Passwords.Length)];
                break;
            default:
                Debug.LogError("Invalid level number.");
                password = "password";
                break;
        }
    }

    private void RunPassword(string input)
    {
        if (input.ToLower() == password.ToLower())
        {
            RunWin(input);
        }
        else
        {
            Terminal.WriteLine("Incorrect, try again. hint: " + password.ToLower().Anagram());
            Terminal.WriteLine(menuMessage);
        }
    }

    private void RunWin(string input)
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward(input);
        Terminal.WriteLine(menuMessage);
    }

    private void ShowLevelReward(string input)
    {
        Terminal.WriteLine(input + " is Correct! Now Enjoy art!");

        switch (level)
        {
            case 1:
                Terminal.WriteLine(@"    8888888888  888    88888
   88     88   88 88   88  88
    8888  88  88   88  88888    
       88 88 888888888 88   88
88888888  88 88     88 88    888888
88  88  88   888    88888    88888
88  88  88  88 88   88  88  88
88 8888 88 88   88  88888    8888
 888  888 888888888 88   88     88
  88  88  88     88 88    8888888");
                break;

            case 2:
                Terminal.WriteLine(@"     _:_
  _/-----\_
<===========>
  \___ ___/
     \ /
      V
      |
      |
      .");
                break;
            
            case 3:
                Terminal.WriteLine(@"
   .
 _/ \_ 
\  .  /
 |/ \|  ");
                break;
            default:
                Debug.LogError("Invaled level reached.");
                break;
        }
    }
}
