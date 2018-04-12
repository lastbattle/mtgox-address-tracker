using System;
using System.Collections.Generic;
using System.Text;

namespace MJB_whale_whisperer.Utils
{
    public class Numeric
    {
        /// <summary>
        /// Converts satoshis to the amount of bitcoins
        /// </summary>
        /// <param name="satoshis"></param>
        /// <returns></returns>
        public static decimal ConvertSatoshisToBitcoin(long satoshis)
        {
            return satoshis / (decimal)100000000;
        }
    }
}
