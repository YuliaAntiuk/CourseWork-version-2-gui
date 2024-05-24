﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GUI
{
    public class Equation
    {
        public double[,] Coefficients { get; set; }
        public double[] Constants { get; set; }
        public int Size { get; set; }
        public double[] Result { get; set; }
        public int IterationCounter { get; set; }
        public Equation(double[,] coefficients, double[] constants, int size)
        {
            Coefficients = coefficients;
            Constants = constants;
            Size = size;
            Result = new double[size];
            IterationCounter = 0;
        }
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
        public double[,] Transpose(double[,] matrix, int n)
        {
            double[,] transMatrix = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                IterationCounter++;
                for (int j = 0; j < n; j++)
                {
                    IterationCounter++;
                    transMatrix[i, j] = matrix[j, i];
                }
            }
            return transMatrix;
        }
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
        public void CalculateSqrtMethod()
        {
            IterationCounter = 0;
            double[,] S = new double[Size, Size];
            double[] y = new double[Size];

            for (int i = 0; i < Size; i++)
            {
                IterationCounter++;
                for (int j = 0; j < Size; j++)
                {
                    IterationCounter++;
                    if (i == j)
                    {
                        double sum = 0;
                        for (int k = 0; k < i; k++)
                        {
                            sum += S[i, k] * S[i, k];
                        }
                        S[i, i] = Math.Sqrt(Coefficients[i, i] - sum);
                    }
                    else if (i > j)
                    {
                        double sum = 0;
                        for (int k = 0; k < j; k++)
                        {
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

            double[,] St = Transpose(S, Size);

            y[0] = Constants[0] / S[0, 0];
            for (int i = 1; i < Size; i++)
            {
                IterationCounter++;
                double sum = 0;
                for (int j = 0; j < i; j++)
                {
                    IterationCounter++;
                    sum += S[i, j] * y[j];
                }
                y[i] = (Constants[i] - sum) / S[i, i];
            }

            Result[Size - 1] = y[Size - 1] / S[Size - 1, Size - 1];
            for (int i = Size - 2; i >= 0; i--)
            {
                IterationCounter++;
                double sum = 0;
                for (int j = i + 1; j < Size; j++)
                {
                    IterationCounter++;
                    sum += St[i, j] * Result[j];
                }
                Result[i] = (y[i] - sum) / S[i, i];
            }
        }
        public void CalculateRotationMethod()
        {
            IterationCounter = 0;
            double[,] A = new double[Size, Size];
            double[] B = new double[Size];
            Array.Copy(Coefficients, A, Coefficients.Length);
            Array.Copy(Constants, B, Constants.Length);
            for (int i = 0; i < Size - 1; i++)
            {
                IterationCounter++;
                for (int k = i + 1; k < Size; k++)
                {
                    IterationCounter++;
                    double r = Math.Sqrt(A[i, i] * A[i, i] + A[k, i] * A[k, i]);
                    if (Math.Abs(r) < double.Epsilon)
                    {
                        throw new InvalidOperationException("Ділення на число, близьке за модулем до 0.");
                    }
                    double c = A[i, i] / r;
                    double s = -A[k, i] / r;

                    for (int j = 0; j < Size; j++)
                    {
                        IterationCounter++;
                        double tempA1 = A[i, j];
                        double tempA2 = A[k, j];
                        A[i, j] = c * tempA1 - s * tempA2;
                        A[k, j] = s * tempA1 + c * tempA2;
                    }

                    double tempB1 = B[i];
                    double tempB2 = B[k];
                    B[i] = c * tempB1 - s * tempB2;
                    B[k] = s * tempB1 + c * tempB2;
                }
            }

            for (int i = Size - 1; i >= 0; i--)
            {
                IterationCounter++;
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
        public void CalculateLUPMethod()
        {
            IterationCounter = 0;
            double[,] L = new double[Size, Size];
            double[,] U = new double[Size, Size];
            int[] P = new int[Size];

            for (int i = 0; i < Size; i++)
            {
                IterationCounter++;
                P[i] = i;
            }

            Array.Copy(Coefficients, U, Coefficients.Length);
            for (int i = 0; i < Size; i++)
            {
                IterationCounter++;
                L[i, i] = 1.0;
            }

            for (int k = 0; k < Size - 1; k++)
            {
                IterationCounter++;
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

                if (pivotRow != k)
                {
                    double temp;
                    for (int j = 0; j < Size; j++)
                    {
                        IterationCounter++;
                        temp = U[k, j];
                        U[k, j] = U[pivotRow, j];
                        U[pivotRow, j] = temp;
                    }

                    int tempIndex = P[k];
                    P[k] = P[pivotRow];
                    P[pivotRow] = tempIndex;

                    for (int j = 0; j < k; j++)
                    {
                        IterationCounter++;
                        temp = L[k, j];
                        L[k, j] = L[pivotRow, j];
                        L[pivotRow, j] = temp;
                    }
                }

                for (int i = k + 1; i < Size; i++)
                {
                    IterationCounter++;
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
