using System;
using System.Collections.Generic;
using System.Linq;

namespace DominoesSolution
{
    public class Dominoes
    {
        public static bool CanChain(IEnumerable<(int, int)> dominoes)
        {
            // If no dominoes entered, return true
            if (dominoes.Count() == 0)
                return true;

            var firstDom = dominoes.FirstOrDefault();

            // If only one dominoe is entered, return bool for comparison of either side
            if (dominoes.Count() == 1)
                return firstDom.Item1 == firstDom.Item2;

            // Check if dominoes can form a chain
            bool isChainable = ChainChecker(firstDom, dominoes);

            return isChainable;
        }

        public static bool ChainChecker((int, int) dom, IEnumerable<(int, int)> dominoes)
        {
            bool result = false;
            // Make a new list
            List<(int, int)> dominoeChain = new List<(int, int)>();
            // Add the first dominoe to the list
            dominoeChain.Add(dom);
            // Set the first dominoe in the list as current domine
            var currentDom = dominoeChain.FirstOrDefault();

            foreach (var dominoe in dominoes)
            {
                // Skip dominoes already checked
                if (dominoe == currentDom) continue;

                var nextDom = dominoe;
                // Compare current dominoe with next dominoe
                result = Valid(currentDom, nextDom);

                // If result is chainable, add it to the chain and check the next dominoe
                if (result)
                {
                    dominoeChain.Add(nextDom);
                    currentDom = nextDom;
                }

                bool checkedAll = dominoeChain.Count() != dominoes.Count();
                // If comparison is not valid and there's more dominoes to check
                if (!result && checkedAll)
                {
                    // Search earlier dominoes for validity
                    var backtrackResult = BacktrackChain(nextDom, dominoeChain);
                    result = backtrackResult.Item1;

                    // If result is still false, return false
                    if (!result) return false; // isChainable = false

                    // Use new dominoe chain if possible
                    dominoeChain = backtrackResult.Item2;
                    // Check the next dominoe
                    currentDom = nextDom;
                }

            }

            return result;
        }

        public static System.Tuple<bool, List<(int, int)>> BacktrackChain((int, int) dominoe, List<(int, int)> dominoeChain)
        {
            for (int i = 1; i < dominoeChain.Count; i++)
            {
                var dominoe1 = dominoeChain[i - 1];
                bool validFrontDominoe = Valid(dominoe1, dominoe);
                if (validFrontDominoe) // Found one match
                {
                    var dominoe2 = dominoeChain[i];
                    bool validBackDominoe = Valid(dominoe2, dominoe);
                    if (validBackDominoe) // Found adjacent match 
                    {
                        dominoeChain.Insert(i, dominoe); // Insert dominoe into list at valid index
                        return Tuple.Create(true, dominoeChain);
                    }
                }
            }

            return Tuple.Create(false, dominoeChain);
        }

        public static bool Valid((int, int) dom1, (int, int) dom2)
        {
            int dom1L = dom1.Item1;
            int dom1R = dom1.Item2;
            int dom2L = dom2.Item1;
            int dom2R = dom2.Item2;
            // Check if the two given dominoes are a valid chain
            bool validity = dom1L == dom2L || dom1L == dom2R ||
                            dom1R == dom2L || dom1R == dom2R;

            return validity;
        }
    }
}

