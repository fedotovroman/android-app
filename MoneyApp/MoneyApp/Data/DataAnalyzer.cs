using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyApp.Data
{
    public class DataAnalyzer
    {
        private List<float> dataSet;
        public DataAnalyzer(ICollection<float> vs) => dataSet = (List<float>)vs;

        public List<float> getEMA()
        {
            float[] setEma = new float[dataSet.Count];
            setEma[0] = dataSet[0];
            for (int i = 1; i < dataSet.Count; i++)
                setEma[i] = dataSet[i] * 0.5f + 0.5f * (setEma[i - 1]);
            return new List<float>(setEma);
        }

        public List<float> getSMA()
        {
            float[] setSma = new float[dataSet.Count];
            setSma[0] = dataSet[0];
            for (int i = 1; i < dataSet.Count; i++)
                setSma[i] = dataSet[i] * 0.2f + 0.8f * (setSma[i - 1]);
            return new List<float>(setSma);
        }

        private float meanValue()
        {
            float mean = 0;
            for (int i = 0; i < dataSet.Count; i++)
                mean += dataSet[i];
            return mean / dataSet.Count;
        }

        public float STDDev()
        {
            float std = 0;
            for (int i = 0; i < dataSet.Count; i++)
                std += (dataSet[i] - getEMA()[i]) * (dataSet[i] - getEMA()[i]);
            std /= dataSet.Count;
            std = (float)Math.Sqrt(std);
            return std;
        }

        public List<float> BBLine1()
        {
            float[] dline = new float[dataSet.Count];
            for (int i = 0; i < dataSet.Count; i++)
            {
                dline[i] = getEMA()[i] + STDDev();
            }
            return new List<float>(dline);
        }

        public List<float> BBLine2()
        {
            float[] dline = new float[dataSet.Count];
            for (int i = 0; i < dataSet.Count; i++)
            {
                dline[i] = getEMA()[i] - STDDev();
            }
            return new List<float>(dline);
        }
    }
}
