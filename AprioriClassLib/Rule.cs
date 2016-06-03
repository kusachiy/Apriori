using System;
using System.Collections.Generic;
using System.Linq;

namespace AprioriClassLib
{
    public class Rule
    {
        public HashSet<int> X;
        public HashSet<int> Y;
        public double Support { get; set; }
        public double Confidence { get; set; }

        public Rule(HashSet<int> X,HashSet<int> Y,double Support,double Confidence)
        {
            this.X = X;
            this.Y = Y;
            this.Support = Support;
            this.Confidence = Confidence;
        }
        public Rule(HashSet<int> X, int Y,double Support,double Confidence)
        {
            this.X = X;
            this.Y = new HashSet<int>(new int[] { Y });
            this.Support = Support;
            this.Confidence = Confidence;
        }
        public Rule(int X, int Y,double Support,double Confidence)
        {
            this.X = new HashSet<int>(new int[] { X });
            this.Y = new HashSet<int>(new int[] { Y });
            this.Support = Support;
            this.Confidence = Confidence;
        }

        public HashSet<int> ToHashSet()
        {
            return new HashSet<int>(X.Union(Y));
        }

        public override string ToString()
        {
            string result = "{0} -> {1}; support = {2}; confidence = {3}";
            return string.Format(result,PrintHashSet(X),PrintHashSet(Y),Math.Round(Support,2),Math.Round(Confidence,2));
        }
        public bool Contains(int element)
        {
            return X.Contains(element) || Y.Contains(element);
        }

        string PrintHashSet(HashSet<int> hs)
        {
            string result = "";
            for (int i = 0; i < hs.Count-1; i++)
            {
                result += hs.ElementAt(i) + ",";
            }
            result += hs.ElementAt(hs.Count - 1);
            return result;
        }
    }
}
