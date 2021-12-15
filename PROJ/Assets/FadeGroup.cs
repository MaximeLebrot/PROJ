using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FadeGroup : MonoBehaviour {

    [Header("FADE IN occurs from index 0-n | FADE OUT occurs from index n-0")]
    [SerializeField] private List<FadeEntity> fadeOrder;
    
    public async Task Fade(FadeMode fadeMode) {
        
        Task[] tasks = new Task[fadeOrder.Count];

        int i = 0;
        
        foreach (FadeEntity entity in fadeOrder) {
            tasks[i] = entity.Fade(fadeMode);
            await Task.Delay((int)entity.TimeUntilNextFade * 1000);
            i++;
        }

        await Task.WhenAll(tasks);
    }
    
    
}
