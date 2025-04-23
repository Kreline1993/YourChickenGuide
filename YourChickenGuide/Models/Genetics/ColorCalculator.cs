namespace YourChickenGuide.Models.Genetics
{
    public class ColorCalculator
    {
        public string Name { get; set; }

        //Base color (E-locus)
        public string E1 { get; set; } // Extension allele 1
        public string E2 { get; set; } // Extension allele 2

        //Pattern Modifiers
        public string Co1 { get; set; } // Columbian pattern (Co-locus)
        public string Co2 { get; set; }
        public string Mo1 { get; set; } // Mottling pattern (Mo-locus)
        public string Mo2 { get; set; }
        public string Ml1 { get; set; } // Melanotic (Ml/locus)
        public string Ml2 { get; set; }
        public string Pg1 { get; set; } // Pattern gene (Pg/locus)
        public string Pg2 { get; set; }


        // List of base color dominance order
        public static List<string> BaseColorDominanceOrder = new List<string>
            {
                "E", "ER", "eWh", "e+", "eb"
            };

        // List of columbian pattern dominance order
        public static List<string> ColumbianPatternDominanceOrder = new List<string>
        {
            "Co", "co+"
        };

        // List of mottling pattern dominance order
        public static List<string> MottlingPatternDominanceOrder = new List<string>
        {
            "Mo+", "mo"
        };

        //List of Melanotic pattern dominance order
        public static List<string> MelanoticPatternDominanceOrder = new List<string>
        {
            "Ml", "ml"
        };

        // List of pattern gene dominance order
        public static List<string> PatternGeneDominanceOrder = new List<string>
        {
            "Pg", "pg+"
        };

        public ColorCalculator(string name, string e1, string e2, string co1, string co2, string mo1, string mo2, string ml1, string ml2, string pg1, string pg2)
        {
            Name = name;
            E1 = e1;
            E2 = e2;
            Co1 = co1;
            Co2 = co2;
            Mo1 = mo1;
            Mo2 = mo2;
            Ml1 = ml1;
            Ml2 = ml2;
            Pg1 = pg1;
            Pg2 = pg2;
        }

        public string GetBaseColor()
        {
            // If both alleles are the same, return the allele
            if (E1 == E2)
            {
                return E1;
            }
            // If one allele is dominant over the other, return the dominant allele
            if (BaseColorDominanceOrder.IndexOf(E1) < BaseColorDominanceOrder.IndexOf(E2))
            {
                return E1;
            }
            else
            {
                return E2;
            }
        }

        public string GetColumbianPattern()
        {
            // Ensure Co1 is always the dominant allele
            if (ColumbianPatternDominanceOrder.IndexOf(Co1) > ColumbianPatternDominanceOrder.IndexOf(Co2))
            {
                (Co1, Co2) = (Co2, Co1); // Swap to maintain correct order
            }

            // Return genotype format (homozygous or heterozygous)
            if (Co1 == Co2)
            {
                return $"{Co1}/{Co1} (Homozygous)";
            }
            else
            {
                return $"{Co1}/{Co2} (Heterozygous)";
            }

        }


        public string GetMottlingPattern()
        {
            // Ensure Mo1 is always the dominant allele
            if (MottlingPatternDominanceOrder.IndexOf(Mo1) > MottlingPatternDominanceOrder.IndexOf(Mo2))
            {
                (Mo1, Mo2) = (Mo2, Mo1); // Swap to maintain correct order
            }

            // Return genotype format (homozygous or heterozygous)
            if (Mo1 == Mo2)
            {
                return $"{Mo1}/{Mo2} (Homozygous)";
            }
            else
            {
                return $"{Mo1}/{Mo2}(Heterozygous)";
            }
        }

        public string GetMelanoticPattern()
        {
            // Ensure Ml1 is always the dominant allele
            if (MelanoticPatternDominanceOrder.IndexOf(Ml1) > MelanoticPatternDominanceOrder.IndexOf(Ml2))
            {
                (Ml1, Ml2) = (Ml2, Ml1); // Swap to maintain correct order
            }

            // Return genotype format (homozygous or heterozygous)
            if (Ml1 == Ml2)
            {
                return $"{Ml1}/{Ml1} (Homozygous)";
            }
            else
            {
                return $"{Ml1}/{Ml2} (Heterozygous)";
            }

        }

        public string GetPatternGenePattern()
        {
            // Ensure Pg1 is always the dominant allele
            if (PatternGeneDominanceOrder.IndexOf(Pg1) > PatternGeneDominanceOrder.IndexOf(Pg2))
            {
                (Pg1, Pg2) = (Pg2, Pg1); // Swap to maintain correct order
            }
            // Return genotype format (homozygous or heterozygous)
            if (Pg1 == Pg2)
            {
                return $"{Pg1}/{Pg1} (Homozygous)";
            }
            else
            {
                return $"{Pg1}/{Pg2} (Heterozygous)";
            }
        }
    }
}
