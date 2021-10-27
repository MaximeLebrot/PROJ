using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public static class PuzzleHelper
{
    public static string SkipFirstChar(string s)
    {
        Debug.Log("Skip first char, string length is :" + s.Length);
        StringBuilder sb = new StringBuilder();
        for (int j = 1; j < s.Length; j++)
        {
            sb.Append(s[j]);
        }
        return sb.ToString();
    }

    public static string RemoveLastChar(string s)
    {
        StringBuilder sb = new StringBuilder();
        for (int j = 0; j < s.Length - 1; j++)
        {
            sb.Append(s[j]);
        }
        return sb.ToString();
    }
    public static char TranslateInput(Node aNode, Node bNode)
    {
        char c = '-';
        Vector3 a = aNode.transform.position;
        Vector3 b = bNode.transform.position;

        if (a.x == b.x && a.y > b.y)
            c = '8';

        if (a.x > b.x && a.y > b.y)
            c = '9';

        if (a.x > b.x && a.y == b.y)
            c = '6';

        if (a.x > b.x && a.y < b.y)
            c = '3';

        if (a.x == b.x && a.y < b.y)
            c = '2';

        if (a.x < b.x && a.y < b.y)
            c = '1';

        if (a.x < b.x && a.y == b.y)
            c = '4';

        if (a.x < b.x && a.y > b.y)
            c = '7';



        return c;
    }

    public static Vector3 TranslateNumToDirection(char c)
    {
        return Vector3.zero;
    }

    public static string RotateSymbols(string chars)
    {
        //switch case för att rotera varje char. foreach char in chars.. switch() t.ex. case 8 = 6
        string rotatedString = "";
        foreach (char c in chars)
        {
            switch (c)
            {
                case '8':
                    rotatedString += '6';
                    break;
                case '6':
                    rotatedString += '2';
                    break;
                case '2':
                    rotatedString += '4';
                    break;
                case '4':
                    rotatedString += '8';
                    break;
                case '9':
                    rotatedString += '3';
                    break;
                case '3':
                    rotatedString += '1';
                    break;
                case '1':
                    rotatedString += '7';
                    break;
                case '7':
                    rotatedString += '9';
                    break;
            }
        }


        return rotatedString;
    }

    public static string MirrorSymbols(string chars)
    {
        //switch case för att rotera varje char. foreach char in chars.. switch() t.ex. case 8 = 6
        string rotatedString = "";
        foreach (char c in chars)
        {
            switch (c)
            {
                case '8':
                    rotatedString += '2';
                    break;
                case '6':
                    rotatedString += '6';
                    break;
                case '2':
                    rotatedString += '8';
                    break;
                case '4':
                    rotatedString += '4';
                    break;
                case '9':
                    rotatedString += '3';
                    break;
                case '3':
                    rotatedString += '9';
                    break;
                case '1':
                    rotatedString += '7';
                    break;
                case '7':
                    rotatedString += '1';
                    break;
            }
        }


        return rotatedString;
    }

    /*
    public static void HandleInstructions(List<Symbol> symbols, PuzzleInstruction inst)
    {
        foreach (string s in inst.GetInstructions())
        {
            if (s[0] != 'L')
            {
                SYMBOLDIRECTION dir = TranslateDirection(s[1]);
                bool cap = TranslateCapital(s[2]);
                Symbol symbol = TranslateLetterSymbol(s[0], dir, cap);
                symbols.Add(symbol);
            }
            else
            {
                //Create the logical symbols in a similar manner
                Symbol symbol = TranslateLogicalSymbol(s[1]);
                symbols.Add(symbol);
            }

        }
    }
    */
    /*
    private static Symbol TranslateLetterSymbol(char c, SYMBOLDIRECTION dir, bool cap)
    {
        Symbol instance;
        switch (c)
        {
            case 'A':
                instance = new A();
                instance.Init(dir, cap);
                return instance;
            case 'B':
                instance = new B();
                instance.Init(dir, cap);
                return instance;
            case 'C':
                instance = new C();
                instance.Init(dir, cap);
                return instance;
            case 'D':
                instance = new D();
                instance.Init(dir, cap);
                return instance;
            case 'E':
                instance = new E();
                instance.Init(dir, cap);
                return instance;
            case 'F':
                instance = new F();
                instance.Init(dir, cap);
                return instance;


        }

        return null;
    }
    */
    /*
    private static Symbol TranslateLogicalSymbol(char c)
    {

        switch (c)
        {
            case 'Q':
                return new Rotate();
            case '{':
                return new LoopOpen();
            case '}':
                return new LoopClosed();
            case 'R':
                return new Repeat();
        }

        return null;
    }
    */
    /*
    private static SYMBOLDIRECTION TranslateDirection(char c)
    {
        switch (c)
        {
            case 'N':
                return SYMBOLDIRECTION.North;
            case 'E':
                return SYMBOLDIRECTION.East;
            case 'S':
                return SYMBOLDIRECTION.South;
            case 'W':
                return SYMBOLDIRECTION.West;
            default:
                Debug.LogError("Invalid direction character");
                return SYMBOLDIRECTION.North;
        }
    }
    */

    private static bool TranslateCapital(char c)
    {
        switch (c)
        {
            case 'T':
                return true;
            case 'F':
                return false;

            default:
                Debug.LogError("Invalid bool character");
                return false;
        }
    }


}
