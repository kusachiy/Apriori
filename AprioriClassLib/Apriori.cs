using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioriClassLib
{
    public class Apriori
    {
        List<int> alphabet;
        List<HashSet<int>> database;
        Dictionary<int,List<Rule>> rules;
        int countOfRows;        

        public Apriori(List<HashSet<int>> database,int maxValue)
        {
            this.database = database;
            countOfRows = database.Count;
            alphabet = new List<int>();
            rules = new Dictionary<int, List<Rule>>();
            for (int i = 0; i < maxValue; i++)            
                alphabet.Add(i + 1);            
        }

        public double Support(HashSet<int> hs)
        {
            int counter = 0;
            foreach (HashSet<int> set in database)
            {
                var s = set.Except(hs);
                if (set.Count - s.Count() == hs.Count)
                    counter++;
            }
            return counter / (double)countOfRows;
        }
               
        public double Support(int number)
        {
            int counter = 0;
            foreach (HashSet<int> set in database)
            {                
                if (set.Contains(number))
                    counter++;
            }
            return counter / (double)countOfRows;
        }
        public void Build()
        {
            CheckAlphabet();
            CreateRules_2();
            bool hc = true;
            int i = 3;
            while (hc)
            {                
                hc = CreateRules(i);
                i++;
            }            
        }
        void CheckAlphabet()
        {
            alphabet = alphabet.FindAll(x => Support(x) >= MinSupport);  
        }
        void CreateRules_2()
        {
            List<Rule> currentRules = new List<Rule>();
            for (int i = 0; i < alphabet.Count; i++)
            {
                for (int j = i+1; j < alphabet.Count; j++)
                {         
                        HashSet<int> set = new HashSet<int>(new int[] { alphabet[i],alphabet[j]});
                        double support = Support(set);
                        if (support >= MinSupport)
                        {
                            double confidence;

                            confidence = support / Support(alphabet[i]);
                            if (Math.Round(confidence,2) >= MinConfidence)
                            {
                                currentRules.Add(new Rule(alphabet[i], alphabet[j],support,confidence));
                            }

                            confidence = support / Support(alphabet[j]);
                            if (Math.Round(confidence,2) >= MinConfidence)                        
                                currentRules.Add(new Rule(alphabet[j],alphabet[i],support,confidence));                        
                        }                    
                }
            }
            rules.Add(2, currentRules);
        }
        bool CreateRules(int rank)
        {
            bool HasChanged = false;
            List<Rule> previosRules = rules[rank - 1];
            List<Rule> currentRules = new List<Rule>();
            for (int i = 0; i < previosRules.Count; i++)
            {
                for (int j = 0; j < alphabet.Count; j++)
                {
                    if (previosRules[i].Contains(alphabet[j]))
                        continue;
                    HashSet<int> set = previosRules[i].ToHashSet();

                    if (!IsUnique(set,alphabet[j], currentRules))
                        continue;

                    set.Add(alphabet[j]);                    
                                  
                    double support = Support(set);
                    if (support >= MinSupport)
                    {
                        double confidence;
                        confidence = support / previosRules[i].Support;
                        if (Math.Round(confidence, 2) >= MinConfidence)
                        {
                            currentRules.Add(new Rule(previosRules[i].ToHashSet(), alphabet[j], support, confidence));
                            HasChanged = true;
                        }                       
                    }
                }
            }
            if(HasChanged)
                rules.Add(rank, currentRules);
            return HasChanged;
        }

        public Dictionary<int,List<Rule>> GetRules
        {
            get { return rules; }
            set { rules = value; }
        }
        bool IsUnique(HashSet<int> set,int Y, List<Rule> rules)
        {
            foreach (var item in rules)           
                if (set.Except(item.X).Count() == 0 && Y == item.Y.ElementAt(0))
                    return false;            
            return true;
        }

        public double MinSupport { get; set; } = 0.4;
        public double MinConfidence { get; set; } = 0.4;
    }
}
