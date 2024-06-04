using System;
using System.Linq;

namespace GUI
{
    public class Equation
    {
        /// <summary>
        /// Matrix of coefficients of the equation.
        /// </summary>
        public double[,] Coefficients { get; set; }
        /// <summary>
        /// Vector of constant terms.
        /// </summary>
        public double[] Constants { get; set; }
        /// <summary>
        /// Size of the matrix.
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// Solution of the equation.
        /// </summary>
        public double[] Result { get; set; }
        /// <summary>
        /// Iteration counter, used to track the number of calculations.
        /// </summary>
        public int IterationCounter { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Equation"/> class with specified coefficients, constants, and size.
        /// </summary>
        /// <param name="coefficients">Matrix of coefficients.</param>
        /// <param name="constants">Vector of constant terms.</param>
        /// <param name="size">Size of the matrix.</param>
        public Equation(double[,] coefficients, double[] constants, int size)
        {
            Coefficients = coefficients;
            Constants = constants;
            Size = size;
            Result = new double[size];
            IterationCounter = 0;
        }
        /// <summary>
        /// Calculates the minor of a matrix by removing the first row and the specified column.
        /// </summary>
        /// <param name="matrix">Input matrix.</param>
        /// <param name="index">Index of the column to be removed.</param>
        /// <returns>The minor matrix.</returns>
        private double[,] CalculateMinor(double[,] matrix, int index)
        {
            int n = matrix.GetLength(0);
            double[,] minor = new double[n - 1, n - 1];

            for (int i = 1; i < n; i++)
            {
                for (int j = 0, k = 0; j < n; j++)
                {
                    if (j == index)
                        continue;

                    minor[i - 1, k++] = matrix[i, j];
                }
            }

            return minor;
        }
        /// <summary>
        /// Transposes the given matrix.
        /// </summary>
        /// <param name="matrix">Matrix to transpose.</param>
        /// <param name="n">Size of the matrix.</param>
        /// <returns>Transposed matrix.</returns>
        public double[,] Transpose(double[,] matrix, int n)
        {
            double[,] transMatrix = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    IterationCounter++;
                    transMatrix[i, j] = matrix[j, i];
                }
            }
            return transMatrix;
        }
        /// <summary>
        /// Calculates the determinant of a matrix.
        /// </summary>
        /// <param name="matrix">Matrix to calculate the determinant of.</param>
        /// <returns>The determinant of the matrix.</returns>
        public double CalculateDeterminant(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            if (n == 1)
            {
                return matrix[0, 0];
            }
            double determinant = 0;
            int sign = 1;
            for (int i = 0; i < n; i++)
            {
                double[,] minorMatrix = CalculateMinor(matrix, i);
                double minorDeterminant = CalculateDeterminant(minorMatrix);
                determinant += sign * matrix[0, i] * minorDeterminant;
                sign = -sign;
            }
            return determinant;
        }
        /// <summary>
        /// Solves the system of equations using the square root method.
        /// </summary>
        public void CalculateSqrtMethod()
        {
            IterationCounter = 0;
            double[,] S = ComputeSMatrix();
            double[,] St = Transpose(S, Size);
            double[] y = ComputeY(S);

            Result[Size - 1] = y[Size - 1] / S[Size - 1, Size - 1];
            for (int i = Size - 2; i >= 0; i--)
            {
                double sum = 0;
                for (int j = i + 1; j < Size; j++)
                {
                    IterationCounter++;
                    sum += St[i, j] * Result[j];
                }
                Result[i] = (y[i] - sum) / S[i, i];
            }
        }
        /// <summary>
        /// Computes the S matrix for Sqrt method.
        /// </summary>
        /// <returns>The S matrix.</returns>
        private double[,] ComputeSMatrix()
        {
            double[,] S = new double[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (i == j)
                    {
                        double sum = 0;
                        for (int k = 0; k < i; k++)
                        {
                            IterationCounter++;
                            sum += S[i, k] * S[i, k];
                        }
                        S[i, i] = Math.Sqrt(Coefficients[i, i] - sum);
                    }
                    else if (i > j)
                    {
                        double sum = 0;
                        for (int k = 0; k < j; k++)
                        {
                            IterationCounter++;
                            sum += S[i, k] * S[j, k];
                        }
                        if (Math.Abs(S[j, j]) < double.Epsilon)
                        {
                            throw new InvalidOperationException("Ділення на число, близьке за модулем до 0.");
                        }
                        S[i, j] = (Coefficients[i, j] - sum) / S[j, j];
                    }
                    else
                    {
                        S[i, j] = 0;
                    }
                }
            }
            return S;
        }
        /// <summary>
        /// Computes the y vector.
        /// </summary>
        /// <param name="S">The S matrix based on which the Y vector is calculated.</param>
        /// <returns>The y vector.</returns>
        private double[] ComputeY(double[,] S)
        {
            double[] y = new double[Size];
            y[0] = Constants[0] / S[0, 0];
            for (int i = 1; i < Size; i++)
            {
                double sum = 0;
                for (int j = 0; j < i; j++)
                {
                    IterationCounter++;
                    sum += S[i, j] * y[j];
                }
                y[i] = (Constants[i] - sum) / S[i, i];
            }
            return y;
        }
        /// <summary>
        /// Solves the system of equations using the rotation method.
        /// </summary>
        public void CalculateRotationMethod()
        {
            IterationCounter = 0;
            double[,] A = new double[Size, Size];
            double[] B = new double[Size];
            Array.Copy(Coefficients, A, Coefficients.Length);
            Array.Copy(Constants, B, Constants.Length);

            for (int i = 0; i < Size - 1; i++)
            {
                for (int k = i + 1; k < Size; k++)
                {
                    double c = 0.0;
                    double s = 0.0;
                    double r = Math.Sqrt(A[i, i] * A[i, i] + A[k, i] * A[k, i]);
                    if (Math.Abs(r) < double.Epsilon)
                    {
                        c = 1.0;
                        s = 0.0;
                    } else
                    {
                        c = A[i, i] / r;
                        s = -A[k, i] / r;
                    }

                    RotateMatrix(A, i, k, c, s);
                    RotateVector(B, i, k, c, s);
                }
            }

            for (int i = Size - 1; i >= 0; i--)
            {
                if (Math.Abs(A[i, i]) < double.Epsilon)
                {
                    throw new InvalidOperationException("Ділення на число, близьке за модулем до 0.");
                }
                double sum = 0;
                for (int j = i + 1; j < Size; j++)
                {
                    IterationCounter++;
                    sum += A[i, j] * Result[j];
                }
                Result[i] = (B[i] - sum) / A[i, i];
            }
        }
        /// <summary>
        /// Rotates A Matrix.
        /// </summary>
        /// <param name="A">Matrix to rotate.</param>
        /// <param name="i">First row index.</param>
        /// <param name="k">Second row index.</param>
        /// <param name="c">Cos for rotation.</param>
        /// <param name="s">Sin for rotation.</param>
        private void RotateMatrix(double[,] A, int i, int k, double c, double s)
        {
            for (int j = 0; j < Size; j++)
            {
                IterationCounter++;
                double tempA1 = A[i, j];
                double tempA2 = A[k, j];
                A[i, j] = c * tempA1 - s * tempA2;
                A[k, j] = s * tempA1 + c * tempA2;
            }
        }
        /// <summary>
        /// Rotates B Vector.
        /// </summary>
        /// <param name="B">Vector to rotate.</param>
        /// <param name="i">First row index.</param>
        /// <param name="k">Second row index.</param>
        /// <param name="c">Cos for rotation.</param>
        /// <param name="s">Sin for rotation.</param>
        private void RotateVector(double[] B, int i, int k, double c, double s)
        {
            IterationCounter++;
            double tempB1 = B[i];
            double tempB2 = B[k];
            B[i] = c * tempB1 - s * tempB2;
            B[k] = s * tempB1 + c * tempB2;
        }
        /// <summary>
        /// Solves the system of equations using the LUP method.
        /// </summary>
        public void CalculateLUPMethod()
        {
            IterationCounter = 0;
            double[,] L = new double[Size, Size];
            double[,] U = new double[Size, Size];
            int[] P = new int[Size];

            for (int i = 0; i < Size; i++)
            {
                P[i] = i;
            }

            Array.Copy(Coefficients, U, Coefficients.Length);
            for (int i = 0; i < Size; i++)
            {
                L[i, i] = 1.0;
            }

            DecomposeLUP(L, U, P);
            double[] y = SolveLyEqualsB(L, P);

            for (int i = Size - 1; i >= 0; i--)
            {
                IterationCounter++;
                Result[i] = y[i];
                for (int j = i + 1; j < Size; j++)
                {
                    IterationCounter++;
                    Result[i] -= U[i, j] * Result[j];
                }
                Result[i] /= U[i, i];
            }
        }
        /// <summary>
        /// Performs LUP decomposition.
        /// </summary>
        /// <param name="L">Lower triangular matrix.</param>
        /// <param name="U">Upper triangular matrix.</param>
        /// <param name="P">Permutation matrix.</param>
        private void DecomposeLUP(double[,] L, double[,] U, int[] P)
        {
            for (int k = 0; k < Size - 1; k++)
            {
                int pivotRow = FindPivotRow(U, k);
                if (pivotRow != k)
                {
                    SwapRows(U, k, pivotRow, Size);

                    int tempIndex = P[k];
                    P[k] = P[pivotRow];
                    P[pivotRow] = tempIndex;

                    SwapRows(L, k, pivotRow, k);
                }
                for (int i = k + 1; i < Size; i++)
                {
                    if (Math.Abs(U[k, k]) < double.Epsilon)
                    {
                        throw new InvalidOperationException("Ділення на число, близьке за модулем до 0.");
                    }
                    L[i, k] = U[i, k] / U[k, k];
                    for (int j = k; j < Size; j++)
                    {
                        IterationCounter++;
                        U[i, j] -= L[i, k] * U[k, j];
                    }
                }
            }
        }
        /// <summary>
        /// Computes pivot row.
        /// </summary>
        /// <param name="U">Upper triangular matrix in which the pivot needs to be found.</param>
        /// <param name="k">Primary row index.</param>
        /// <returns>New pivot row.</returns>
        private int FindPivotRow(double[,] U, int k)
        {
            int pivotRow = k;
            double pivotValue = Math.Abs(U[k, k]);

            for (int i = k + 1; i < Size; i++)
            {
                IterationCounter++;
                if (Math.Abs(U[i, k]) > pivotValue)
                {
                    pivotRow = i;
                    pivotValue = Math.Abs(U[i, k]);
                }
            }

            return pivotRow;
        }
        /// <summary>
        /// Swaps rows
        /// </summary>
        /// <param name="matrix">Matrix for swapping rows.</param>
        /// <param name="row1">First row.</param>
        /// <param name="row2">Second row.</param>
        /// <param name="startColumn">Column to start.</param>
        private void SwapRows(double[,] matrix, int row1, int row2, int end)
        {
            for (int j = 0; j < end; j++)
            {
                IterationCounter++;
                double temp = matrix[row1, j];
                matrix[row1, j] = matrix[row2, j];
                matrix[row2, j] = temp;
            }
        }
        /// <summary>
        /// Computes y vector based on L matrix and constant vector considering permutations
        /// </summary>
        /// <param name="L">Lower triangular matrix.</param>
        /// <param name="P">Permutation vector.</param>
        /// <returns>y vector</returns>
        private double[] SolveLyEqualsB(double[,] L, int[] P)
        {
            double[] y = new double[Size];
            for (int i = 0; i < Size; i++)
            {
                IterationCounter++;
                y[i] = Constants[P[i]];
                for (int j = 0; j < i; j++)
                {
                    IterationCounter++;
                    y[i] -= L[i, j] * y[j];
                }
            }

            return y;
        }
        /// <summary>
        /// Find the maximum and minimum value in the matrix of coefficients and vector free terms for the 2x2 system.
        /// </summary>
        /// <returns>An array containing the maximum and minimum values.</returns>
        public double[] FindMaximumMinimum()
        {
            int arrSize = Size * 3;
            double[] arr = new double[arrSize];
            int k = 0;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    arr[k] = Coefficients[i, j];
                    k++;
                }
                arr[k] = Constants[i];
                k++;
            }
            double max = arr.Max();
            double min = arr.Min();
            return new double[] { max, min };
        }
    }
}
