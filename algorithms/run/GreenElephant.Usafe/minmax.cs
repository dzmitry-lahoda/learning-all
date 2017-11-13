using System;
using System.Collections.Generic;
using System.Text;

namespace System.Collections.Unsafe
{
    public struct minmax
    {
        int minValue, maxValue;

        public minmax(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public minmax max(int possibleMax) => this.maxValue < possibleMax ? new minmax(this.minValue, possibleMax) : this;
        public minmax min(int possibleMin) => this.minValue > possibleMin ? new minmax(possibleMin, this.maxValue) : this;
        public minmax minMax(int minMax) => this.min(minMax).max(minMax);
        public minmax minMax(int? minMax) => minMax.HasValue ? this.min(minMax.Value).max(minMax.Value) : this;
        public minmax minMax(int possibleMin, int possibleMax) => this.min(possibleMin).max(possibleMax);
        public minmax minMax(minmax minMax) => this.min(minMax.minValue).max(minMax.maxValue);
        public int abs() => Math.Abs(this.maxValue - this.minValue);
        public minmax greaterOf(minmax minMax) => this.abs() > minMax.abs() ? this : minMax;

        public override string ToString() => $"({this.minValue}, {this.maxValue})";
    }
}
