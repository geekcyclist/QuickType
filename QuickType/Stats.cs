using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickType
{
    /// <summary>
    /// Keeps stats for the game
    /// </summary>
    class Stats
    {
        public int Total { get; set; }
        public int Missed { get; set; }
        public int Correct { get; set; }
        public int Accuracy { get; set; }

        public void Update(bool CorrectEntry)
        {
            Total++;
            if (!CorrectEntry)
            {
                Missed++;
            }
            else
            {
                Correct++;
            }

            Accuracy = 100 * Correct / Total;
        }
    }
}
