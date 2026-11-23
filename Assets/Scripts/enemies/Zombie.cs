using UnityEngine;

public class Zombie : enemyScript
{
    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public void ZombieScratch(attackStats attack)
    {
        
        int range = attack.range;
        generator.actionState = "Attack";
        currentPath = Pathfinding.FindPath(
            currentPole,
            targetTile,
            "Attack"
        );
        bool isInCleanLine = currentPole.HasClearLine(targetTile);
        if (currentPath.Count <= range && isInCleanLine)
        {
            abillityAproved = true;
            
            

        }

    }
}
