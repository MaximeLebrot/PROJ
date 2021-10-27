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

        //bygg f�rst en array med alla symbolers �vers�ttningar. s� streck upp blir bara en 8 t.ex
        foreach(PuzzleObject obj in objects)
        {          
            translations.Add(obj.GetTranslation());
        }

        //g� igenom array och �vers�tt med switchcase t.ex. logiska operatorer blir n�got annat h�r t.ex. case R s� roteras den str�ngen p� indexet innan.
        for (int i = 0; i < translations.Count; i++)
        {
            string newString = "";
            string oldString = translations[i];
            StringBuilder sb = new StringBuilder();

            switch (translations[i][0])
            {
                case 'Q':
                    newString = PuzzleHelper.SkipFirstChar(oldString);
                    translations[i] = PuzzleHelper.RotateSymbols(newString);
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


