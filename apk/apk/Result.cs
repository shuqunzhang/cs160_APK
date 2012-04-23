using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace apk
{
    public class Result
    {
        public String name;
        public String time;
        public double duration = 0;
        public double posture = 0;
        public double motionRange = 0;
        public double volume = 0;
        public double totalGestures = 0;
        public int totalWords = 0;
        public Dictionary<String, double> gestures = new Dictionary<String, double>();
        public Dictionary<String, int> words = new Dictionary<String, int>();
    }
}
