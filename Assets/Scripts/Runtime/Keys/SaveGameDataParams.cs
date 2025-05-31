using System;

namespace Runtime.Keys
{
    [Serializable]
    public struct SaveGameDataParams
    {
        public int Level;
        public int Money;
        public int IncomeLevel;
        public int StackLevel;
    }
}