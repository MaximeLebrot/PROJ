using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class PuzzleTranslator
{

    
    private string solution;
    protected List<string> translations = new List<string>();

   

    public string CalculateSolution(List<PuzzleObject> objects)
    {
        solution = "";
        translations.Clear();

        //bygg först en array med alla symbolers översättningar. så streck upp blir bara en 8 t.ex
        foreach(PuzzleObject obj in objects)
        {
            translations.Add(obj.GetTranslation());
        }

        //gå igenom array och översätt med switchcase t.ex. logiska operatorer blir något annat här t.ex. case R så roteras den strängen på indexet innan.
        for (int i = 0; i < translations.Count; i++)
        {
            string newString = "";
            string oldString = translations[i];
            StringBuilder sb = new StringBuilder();

            switch (translations[i][0])
            {
                case 'Q':
                    newString = SkipFirstChar(oldString);
                    translations[i] = PuzzleHelper.RotateSymbols(newString);
                    break;

                case 'R':
                    newString = SkipFirstChar(oldString);
                    translations[i] = newString;
                    translations.Insert(i + 1, newString);
                    i++;
                    break;

                case 'X':
                    translations.RemoveAt(i);
                    break;

                case 'M':
                    newString = SkipFirstChar(oldString);
                    translations[i] = PuzzleHelper.MirrorSymbols(newString);
                    break;

                default:
                    break;
            }

        }
        for (int i = 0; i < translations.Count; i++)
        {
            switch (translations[i])
            {
                case "Q":
                    translations[i - 1/*WHAT IF THE FIRST STRING HAS A MOD???*/] = PuzzleHelper.RotateSymbols(translations[i - 1]);
                    translations.Remove("Q");
                    i--;
                    break;

                case "R":
                    translations[i] = translations[i - 1];
                    break;

                default:
                    break;
            }
        }

        foreach (string s in translations)
        {
            solution += s;
        }

        return solution;
        #region TRANSLATIONS
        /*
         * 8 = N
         * 9 = NE
         * 6 = E
         * 3 = SE
         * 2 = S
         * 1 = SW
         * 4 = W
         * 7 = NW
         * Q = Rotate
         * R = Repeat
         * { = Loop open, save the following until close
         * } = Loop close, repeat all the saved translations
         */
        #endregion
    }

    protected string SkipFirstChar(string s)
    {
        StringBuilder sb = new StringBuilder();
        for (int j = 1; j < s.Length; j++)
        {
            sb.Append(s[j]);
        }
        return sb.ToString();
    }

    /*
    private void FindLoop(int index)
    {
        for(int i = index; i < translations.Count; i++)
        {
            if (translations[i] != "}")
            {
                loopSymbols.Add(translations[i]);
            }
            else
                break;
        }
    }
    */



}

public static class PuzzleHelper
{
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
