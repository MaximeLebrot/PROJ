using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnorderedSymbolPlacer : SymbolPlacer
{
    [SerializeField] private List<Transform> symbolPositions = new List<Transform>();

    protected override void UnevenPlaceSymbols()
    {
        //Should probably only relate to the correct number of positions, uneven or even shouldnt matter.
        EvenPlaceSymbols();
    }
    protected override void EvenPlaceSymbols()
    {
        //May be a way to handle this depending on implemenetation, discarding extra symbols or extra positions for instance
        if(instantiatedSymbols.Count != symbolPositions.Count)
            Debug.LogError("Wrong number of symbols to symbolPositions");



    }
}
