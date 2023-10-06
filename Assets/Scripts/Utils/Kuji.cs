using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WorldTicket.Data;

namespace WorldTicket.Utils
{
    public static class Kuji
    {
        public static List<int> GetRandoms(IReadOnlyList<int> list, int count)
        {
            if (count < 0 || count >= list.Count) return new(list);

            List<int> randomItems = new();
            HashSet<int> selectedIndices = new HashSet<int>();

            while (randomItems.Count < count)
            {
                int randomIndex = Random.Range(0, list.Count);

                // 중복을 피하고 선택된 항목을 추적
                if (selectedIndices.Add(randomIndex))
                {
                    randomItems.Add(list[randomIndex]);
                }
            }

            return randomItems;
        }

        public static List<T> GetRandoms<T>(IReadOnlyList<T> list, int count) where T : IData
        {
            if (count < 0 || count >= list.Count) return new List<T>(list);

            List<T> randomItems = new List<T>();
            HashSet<int> selectedIndices = new HashSet<int>();

            while (randomItems.Count < count)
            {
                int randomIndex = Random.Range(0, list.Count);

                // 중복을 피하고 선택된 항목을 추적
                if (selectedIndices.Add(randomIndex))
                {
                    randomItems.Add(list[randomIndex]);
                }
            }

            return randomItems;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
