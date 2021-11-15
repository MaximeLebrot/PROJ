using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class PuzzleTranslator
{

    
    private string solution;
    protected List<string> translations = new List<string>();

   

    public string CalculateSolution(List<PuzzleObject> objects)
    {
        solution = "";
        translations.Clear();

        //bygg f�rst en array med alla symbolers �vers�ttningar. s� streck upp blir bara en 8 t.ex
        foreach(PuzzleObject obj in objects)
        {          
            translations.Add(obj.GetTranslation());
        }

        //g� igenom array och �vers�tt med switchcase t.ex. logiska operatorer blir n�got annat h�r t.ex. case R s� roteras den
        for (int i = 0; i < translations.Count; i++)
        {
            string newString = "";
            string oldString = translations[i];
            StringBuilder sb = new StringBuilder();

            switch (translations[i][0])
            {
                case 'Q':
                    newString = PuzzleHelper.SkipFirstChar(oldString);
                    translations[i] = PuzzleHelper.RotateSymbolsTwoStep(newString);
                    break;

                case 'W':
                    newString = PuzzleHelper.SkipFirstChar(oldString);
                    for(int j = 0; j < 2; j++)
                    {
                        newString = PuzzleHelper.RotateSymbolsTwoStep(newString);
                    }
                    translations[i] = newString;
                    break;

                case 'E':
                    newString = PuzzleHelper.SkipFirstChar(oldString);
                    for (int j = 0; j < 3; j++)
                    {
                        newString = PuzzleHelper.RotateSymbolsTwoStep(newString);
                    }
                    translations[i] = newString;
                    break;

                case 'R':
                    newString = PuzzleHelper.SkipFirstChar(oldString);
                    translations[i] = newString;
                    translations.Insert(i + 1, newString);
                    i++;
                    break;

                case 'X':
                    translations.RemoveAt(i);
                    break;

                case 'M':
                    newString = PuzzleHelper.SkipFirstChar(oldString);
                    translations[i] = PuzzleHelper.MirrorSymbols(newString);
                    break;
                case 'D':
                    newString = PuzzleHelper.SkipFirstChar(oldString);
                    translations[i] = PuzzleHelper.DoubleStrokes(newString);
                    break;
                    
                default:
                    break;
            }

        }

        /*
        for (int i = 0; i < translations.Count; i++)
        {
            switch (translations[i])
            {
                case "Q":
                    translations[i - 1] = PuzzleHelper.RotateSymbols(translations[i - 1]);
                    translations.Remove("Q");
                    i--;
                    break;
                case "W":

                    translations[i - 1] = PuzzleHelper.RotateSymbols(translations[i - 1]);
                    translations.Remove("W");
                    i--;
                    break;
                case "E":
                    translations[i - 1] = PuzzleHelper.RotateSymbols(translations[i - 1]);
                    translations.Remove("E");
                    i--;
                    break;
                case "R":
                    translations[i] = translations[i - 1];
                    break;

                default:
                    break;
            }
        }
        */

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

    internal List<string> GetTranslations()
    {
        return translations;
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


