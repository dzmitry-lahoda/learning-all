
Create pseudo class named BigNumber and SmallNumber. Write operation upon it. Parse code and replace it with real int/long/etc.


MatrixExtensions provides extension methods for numeric arrays. Arrays are treated as vectors and 2D matrices. Operations provided for fast prototyping and integration of scientific libraries. Does NOT have Matrix or Vector class.


 accepts `Node[][]` as data. Each node contains index. 
```csharp
 [Test]
        public void ToJaggedWithIndecesTest()
        {
            //given a matrix
            var matrix = new[,] { { 1, 2 },
                                  { 3, 4 },
                                  {1, 1.5}};

            //you can convert it to jagged containing elements of different type
            //outer and inner loop indexes are used in lambda expression
            var actual = matrix.ToJagged((x,r,c) => new Node(c+1,x));

            var expected = new[] { new[] { new Node(1,1), new Node(2,2) }, 
                                   new[] { new Node(1,3), new Node(2,4)},
                                   new[] { new Node(1,1), new Node(2,1.5) } };
            Assert.AreEqual(actual[0][0], expected[0][0]);
            Assert.AreEqual(actual[0][1], expected[0][1]);
            Assert.AreEqual(actual[1][0], expected[1][0]);
            Assert.AreEqual(actual[1][1], expected[1][1]);
            Assert.AreEqual(actual[2][0], expected[2][0]);
            Assert.AreEqual(actual[2][1], expected[2][1]);
        }
```

- ata Analysis routines accept `double[,] xy`. Each row is point. Each column - variable. Last column - categories. So if we have `double[,] x` - data, `int[] y` - categories.

```csharp
 [Test]
        public void AppendVectorTest()
        {
            //given a matrix 
            var data = new [,]{{1.2, 2.2},
                               {0.4, 0.6},
                               {1.7, 0.1}};
            //given a vector with length equal to number of matrix's rows
            var categories = new[]{1,
                                   0,
                                   1};
            //we can append vector to the right side of matrix
            var actual = data.Append(categories);

            var expected = new[,]{{1.2, 2.2,1},
                                  {0.4, 0.6, 0},
                                  {1.7, 0.1, 1}};

            Assert.IsTrue(expected.EqualsElements(actual));
        }
```