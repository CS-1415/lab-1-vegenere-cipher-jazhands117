//Jay Johnson 8/27/25 Lab 1
//This is a simple Vigenere cipher, with many debugging tests. Good luck figureing out the organization.

using System.Diagnostics;

//reads a character, tests if it's lowercase (ret. false if not)
static bool isLowercaseLetter(char c)
{
    if (char.IsLower(c))
    {
        return true;
    }
    return false;
}

//reads a string, tests if each character is lowercase (ret. false if not)
static bool isValidInput(string input)
{
    foreach (char c in input)
    {
        if (!isLowercaseLetter(c))
        {
            return false;
        }
    }
    return true;
}

//forcing user to do valid input
static string ForcedValidInput()
{
    while (true)
    {
        string input = Console.ReadLine();
        if (isValidInput(input))
        {
            return input;
        }
        else
        {
            Console.WriteLine("Please input only lowercase letters.");
        }
    }
}

//takes a character and shifts it by another
static char ShiftedLetter(char c, char b)
{
    int shiftChar = c - 'a';
    int shiftKey = b - 'a';
    int shiftedInt = shiftChar + shiftKey;
    char shifted = (char)((shiftedInt % 26) + 'a');

    return shifted;
}

//takes two strings, encrypts the first by the second
//char keyChar = key[keyPart % key.Length]; takes small keys and applies it char by char to input
static string ShiftedMessage(string input, string key)
{
    string result = "";
    int keyPart = 0;
    foreach (char c in input)
    {
        char keyChar = key[keyPart % key.Length];
        char shifted = ShiftedLetter(c, keyChar);
        result += shifted;
        keyPart++;
    }
    return result;
}


//Running tests
TestisLowercaseLetter();
TestisValidInput();
TestShiftedLetter();
TestShiftedMessage();


Console.Clear();
//user input
Console.WriteLine("This program encrypts the message using the Vigenere method.\nPlease enter the message.");
string message = ForcedValidInput();

Console.WriteLine("Please enter an encryption key.");
string key = ForcedValidInput();

//Actual encryption step
string encryption = ShiftedMessage(message, key);
Console.WriteLine($"Your encrypted message is: {encryption}.");



//Tests
static void TestisLowercaseLetter()
{
    Debug.Assert(isLowercaseLetter('a'));
    Debug.Assert(!isLowercaseLetter('A'));
    Debug.Assert(!isLowercaseLetter('\\'));
}
static void TestisValidInput()
{
    Debug.Assert(isValidInput("string"), "String 1");
    Debug.Assert(!isValidInput(" "), "String space");
    Debug.Assert(!isValidInput("."), "String symbol");
}
static void TestShiftedLetter()
{
    Debug.Assert(ShiftedLetter('t', 'r') == 'k', "CharShift 1");
    Debug.Assert(ShiftedLetter('a', 'a') == 'a', "charshift null");
    Debug.Assert(ShiftedLetter('a', 'b') != 'a', "returns same");
}
static void TestShiftedMessage()
{
    Debug.Assert(ShiftedMessage("hello", "b") == "ifmmp", "Final, single key");
    Debug.Assert(ShiftedMessage("hello", "bc") == "igmnp", "final, double key");
    Debug.Assert(ShiftedMessage("hello", "d") != "hello", "final, null");
}