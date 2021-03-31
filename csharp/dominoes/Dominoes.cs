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

            // If only one domino is entered, return bool for comparison of either side
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
            List<(int, int)> dominoChain = new List<(int, int)>();
            // Add the first domino to the list
            dominoChain.Add(dom);
            // Set the first domino in the list as current domino
            var currentDom = dominoChain.FirstOrDefault();

            for (int i = 1; i < dominoes.Count(); i++)
            {
                var domino = dominoes.ElementAt(i - 1);

                // Skip dominoes already checked
                if (domino == currentDom) continue;

                var nextDom = domino;
                // Compare current domino with next domino
                result = Valid(currentDom, nextDom);

                // If result is chainable, add it to the chain and check the next domino
                if (result)
                {
                    dominoChain.Add(nextDom);
                    currentDom = nextDom;
                }

                bool notCheckedAll = dominoChain.Count() != dominoes.Count();
                // If comparison is not valid and there's more dominoes to check
                if (!result && notCheckedAll)
                {
                    // Search earlier dominoes for validity
                    var backtrackResult = BacktrackChain(nextDom, dominoChain);
                    result = backtrackResult.Item1;

                    // If result is still false
                    if (!result)
                    {
                        // Try the next domino in given dominoes as the first domino in a new chain
                        ChainChecker(dominoes.ElementAt(i), dominoes); 
                    }

                    // Use new domino chain if possible
                    dominoChain = backtrackResult.Item2;
                    // Check the next domino
                    currentDom = nextDom;
                }

            }

            return result;
        }

        public static Tuple<bool, List<(int, int)>> BacktrackChain((int, int) domino, List<(int, int)> dominoChain)
        {
            for (int i = 1; i < dominoChain.Count; i++)
            {
                var domino1 = dominoChain[i - 1];
                bool validFrontDomino = Valid(domino1, domino);
                if (validFrontDomino) // Found one match
                {
                    var domino2 = dominoChain[i];
                    bool validBackDomino = Valid(domino2, domino);
                    if (validBackDomino) // Found adjacent match 
                    {
                        dominoChain.Insert(i, domino); // Insert domino into list at valid index
                        return Tuple.Create(true, dominoChain);
                    }
                }
            }

            return Tuple.Create(false, dominoChain);
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

