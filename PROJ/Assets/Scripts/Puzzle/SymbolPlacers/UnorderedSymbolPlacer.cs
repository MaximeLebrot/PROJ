using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnorderedSymbolPlacer : SymbolPlacer
{
    [SerializeField] private List<Transform> symbolPositions = new List<Transform>();
    public List<PuzzleObject> instantiatedSymbolsClone;

    //Angle offset depending on number of symbols? 
    private float degreeOffset;
    //Where should the symbol spawn if its only 1?
    private float startingOffset = 90f;
    //Some arbitrary size for offset from middle of panel
    private float panelSize = 2f;

    protected override void UnevenPlaceSymbols()
    {
        //Should probably only relate to the correct number of positions, uneven or even shouldnt matter.
        EvenPlaceSymbols();
    }
    protected override void EvenPlaceSymbols()
    {
        //May be a way to handle this depending on implemenetation, discarding extra symbols or extra positions for instance
        if(instantiatedSymbols.Count != symbolPositions.Count)
            Debug.Log("Wrong number of symbols to symbolPositions");

        instantiatedSymbolsClone = new List<PuzzleObject>(instantiatedSymbols);
        //might not be what we want to do here, actually
        degreeOffset = Mathf.Deg2Rad * (360 / instantiatedSymbolsClone.Count);        
        int counter = 1;     
        
        for (int i = instantiatedSymbolsClone.Count; i > 0; i--)
        {
            float radians = startingOffset + counter * degreeOffset;
            PlaceSymbols(symbolPos.transform.position + new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0) * panelSize);
            Debug.Log("i is " + i + ", counter is : "+counter);
            counter++;
        }        
    }

    private void PlaceSymbols(Vector3 newPos)
    {
        Debug.Log("placing symbol at position:" + newPos);
        //Move and REmove instance from list

        PuzzleObject instance = instantiatedSymbolsClone[Random.Range(0, instantiatedSymbolsClone.Count)];
        Debug.Log("instance is : " + instance.name);
        instance.transform.position = newPos;
        instantiatedSymbolsClone.Remove(instance);
    }

}
