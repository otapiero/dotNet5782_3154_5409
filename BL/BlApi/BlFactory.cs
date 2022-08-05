using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    /// <summary>
    /// The bl factory.
    /// </summary>
    public static class BlFactory
    {
        static IBL Instance = null;

        /// <summary>
        /// Gets the bl.
        /// </summary>
        /// <returns>An IBL.</returns>
        public static IBL GetBl()
        {
            if (Instance == null)
                Instance = new BL.BL();

            return Instance;
        }
    }
}
