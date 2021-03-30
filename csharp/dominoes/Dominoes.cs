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

        public static List<(int, int)> dominoeChain = new List<(int, int)>();

        public static bool ChainChecker((int, int) dom, IEnumerable<(int, int)> dominoes)
        {
            bool result = false;
            dominoeChain.Add(dom);
            var currentDom = dominoeChain.FirstOrDefault();


            // Compare current dominoe with next dominoe
            var enumerator = dominoes.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var nextDom = enumerator.Current;

                // Compare current dominoe with next dominoe
                result = Valid(currentDom, nextDom);

                // If result is chainable, add it to the chain and check the next dominoe
                if (result)
                {
                    dominoeChain.Add(nextDom);
                    currentDom = nextDom;
                }

                // If comparison is not valid and there's more dominoes to check
                if (!result && dominoeChain.Count() != dominoes.Count())
                {
                    // Search earlier dominoes for validity
                    result = BacktrackChain(currentDom);

                    // If result is still false, return false
                    if (!result) return false;
                }

            }

            return result;
        }

        public static bool BacktrackChain((int, int) dominoe)
        {
            bool success = false;

            foreach (var d in dominoeChain)
            {
                success = Valid(d, dominoe);

                if (success)
                {
                    dominoeChain.Add(dominoe);
                    return true;
                }
            }

            return success;
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

