using System.Collections.Generic;
using Blocks;
using Blocks.Extensions;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using NUnit.Framework;

namespace MPF.Algorithms.Tests
{
    [TestFixture]
    public class MemoryTest
    {

        private const int NumberOfPatterns = 5;

        [Test]
        public void AddOnePatternTest()
        {
            
            var memory = new FirstOrderMarkovChain();
            var vector = new DenseVector(new double[] { 1, 1 });
            var pattern = new DenseMatrix(2, 2);
            pattern.SetRow(0, vector.ToArray());
            pattern.SetRow(1, vector.ToArray());
            memory.AddPattern(pattern);
            Assert.IsNotNull(memory.NormalizedTransitionMatrix);
            Assert.IsNotNull(memory.Patterns);
            Assert.IsTrue(memory.Patterns.Count == 1, memory.ToString());
            Assert.IsTrue(memory.NormalizedTransitionMatrix.IsQuadratic(), memory.ToString());
            Assert.IsTrue(memory.NormalizedTransitionMatrix.RowCount == 1, memory.ToString());
            Assert.IsTrue(
                DefaultComparing.Compare(memory.NormalizedTransitionMatrix[0, 0], 0) == 0,
                memory.ToString()
            );
        }


        [Test]
        public void AddSeveralEqualPatternsTest()
        {
            var memory = new FirstOrderMarkovChain();
            var vector = new DenseVector(new double[] { 1, 1 });
            var pattern = new DenseMatrix(2, 2);
            pattern.SetRow(0, vector.ToArray());
            pattern.SetRow(1, vector.ToArray());
            for (var i = 0; i < NumberOfPatterns; i++)
            {
                memory.AddPattern(pattern);
            }
            Assert.IsNotNull(memory.NormalizedTransitionMatrix);
            Assert.IsNotNull(memory.Patterns);
            Assert.IsTrue(memory.Patterns.Count == 1, memory.ToString());
            Assert.IsTrue(memory.NormalizedTransitionMatrix.IsQuadratic());
            Assert.IsTrue(memory.NormalizedTransitionMatrix.RowCount == 1);
            Assert.IsTrue(memory.NormalizedTransitionMatrix[0, 0] == 1);
        }

        [Test]
        public void AddSeveralDifferentPatternsInStairwayMannerTest()
        {
            var memory = new FirstOrderMarkovChain();
            for (var i = 0; i < NumberOfPatterns; i++)
            {
                var vector = new DenseVector(new double[] { i, i });
                var pattern = new DenseMatrix(2, 2);
                pattern.SetRow(0, vector.ToArray());
                pattern.SetRow(1, vector.ToArray());
                memory.AddPattern(pattern);
            }
            Assert.IsNotNull(memory.NormalizedTransitionMatrix);
            Assert.IsNotNull(memory.Patterns);
            Assert.IsTrue(memory.Patterns.Count == NumberOfPatterns, "Number of patterns less than should be." + memory.Patterns.Count);
            Assert.IsTrue(memory.NormalizedTransitionMatrix.IsQuadratic(), "NormalizedTransitionMatrix is not quadratic.");
            Assert.IsTrue(memory.NormalizedTransitionMatrix.RowCount == NumberOfPatterns, "NormalizedTransitionMatrix of transitions less than should be.");
            Assert.IsTrue(
                DefaultComparing.Compare(memory.NormalizedTransitionMatrix[0, 1], 1) == 0,
                memory.TransitionMatrix.ToString()
                );
        }

        [Test]
        public void AddSeveralDifferentPatternsTest()
        {
            var memory = new FirstOrderMarkovChain();
            var patterns = new List<Matrix>();
            for (var i = 0; i < NumberOfPatterns; i++)
            {
                var vector = new DenseVector(new double[] { i, i });
                var pattern = new DenseMatrix(2, 2);
                pattern.SetRow(0, vector.ToArray());
                pattern.SetRow(1, vector.ToArray());
                patterns.Add(pattern);
            }
            memory.AddPatterns(patterns[0], patterns[0], patterns[1], patterns[1], patterns[0]);
            Assert.IsNotNull(memory.NormalizedTransitionMatrix);
            Assert.IsNotNull(memory.Patterns);
            Assert.IsTrue(memory.Patterns.Count == 2, "Number of patterns less than should be." + memory.ToString());
            Assert.IsTrue(memory.NormalizedTransitionMatrix.IsQuadratic(), "NormalizedTransitionMatrix is not quadratic." + memory.ToString());
            Assert.IsTrue(memory.NormalizedTransitionMatrix.RowCount == 2, "NormalizedTransitionMatrix of transitions less than should be." + memory.ToString());
            Assert.IsTrue(
                DefaultComparing.Compare(memory.NormalizedTransitionMatrix[0, 0], 0.5) == 0,
                memory.TransitionMatrix.ToString()
                );
            Assert.IsTrue(
    DefaultComparing.Compare(memory.NormalizedTransitionMatrix[0, 1], 0.5) == 0,
    memory.TransitionMatrix.ToString()
    );
            Assert.IsTrue(
    DefaultComparing.Compare(memory.NormalizedTransitionMatrix[1, 0], 0.5) == 0,
    memory.TransitionMatrix.ToString()
    );
            Assert.IsTrue(
DefaultComparing.Compare(memory.NormalizedTransitionMatrix[1, 1], 0.5) == 0,
memory.TransitionMatrix.ToString()
);
        }

    }
}
