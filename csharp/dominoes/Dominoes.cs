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
            int currDomL = firstDom.Item1;
            int currDomR = firstDom.Item2;

            // If only one dominoe is entered, return bool for comparison of either side
            if (dominoes.Count() == 1) 
                return currDomL == currDomR;            

            bool result = false;
            var enumerator = dominoes.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var nextDom = enumerator.Current;
                int nextDomL = nextDom.Item1;
                int nextDomR = nextDom.Item2;
                // Compare current dominoe with next dominoe
                bool isValid = currDomL == nextDomL || currDomL == nextDomR || 
                                currDomR == nextDomL || currDomR == nextDomR;
                // If any donminoes can't chain, return false
                if (!isValid)
                    return false; // TODO: Check other dominoe sequence instead of only returning flase
                // Else, set result to true 
                result = isValid;
                // Set current dominoe to the next dominoe
                currDomL = nextDomL;
                currDomR = nextDomR;
            }
            return result;
        }
    }
}
