namespace dnHTM
{
    public interface IClassifierNode
    {
        void Fit(double[,] data, int[] categories);

        int[] Predict(double[,] data);
    }
}