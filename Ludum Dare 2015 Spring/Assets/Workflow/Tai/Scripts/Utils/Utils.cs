using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Utils : MonoBehaviour {

    public int[] GetUniqueRandomInt(int count, int startRange, int endRange, List<int> forbiddenNumbers = null)
    {
        if (endRange <= startRange)
            return null;

        if (forbiddenNumbers != null)
        {
            if (count > endRange - startRange - forbiddenNumbers.Count)
            {
                return null;
            }
        }
        else
        {
            if (count > endRange - startRange)
                return null;
        }

        if (count < 1)
            return null;

        int[] listFinalValues = new int[count];
        List<int> listRandom = new List<int>();
        int lengthAvailableRandom = endRange - startRange;

        for (int i = 0; i < lengthAvailableRandom; i++)
            listRandom.Add(startRange + i);

        if (forbiddenNumbers != null)
        {
            for (int i = 0; i < forbiddenNumbers.Count; i++)
            {
                if (listRandom.Contains(forbiddenNumbers[i]))
                    listRandom.Remove(forbiddenNumbers[i]);
            }
        }

        for (int i = 0; i < count; i++)
        {
            listFinalValues[i] = listRandom[UnityEngine.Random.Range(0, listRandom.Count)];
            listRandom.Remove(listFinalValues[i]);
        }

        return listFinalValues;
    }

}
