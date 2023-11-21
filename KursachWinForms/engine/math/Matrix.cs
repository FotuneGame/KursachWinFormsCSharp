using System;

namespace Engine.Math
{
    // НЕ ИСПОЛЬЗУЕТСЯ (Хотя её надо было использовать для поворотов и перемещений объектов)
    public sealed class Matrix
    {
        public double[,] matrix;

        public Matrix(double[,] matrix)
        {
            this.matrix = matrix;
        }
        public Matrix(int x,int y)
        {
            matrix = new double[x, y];
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    matrix[i,j] = 0;
        }


        // еденичная матрица
        public Matrix E_matrix(int dimensions)
        {
            Matrix E = new Matrix(dimensions, dimensions);
            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    E.matrix[i, j] = (i == j ? 1 : 0);
                }
            }
            return E;
        }

        // перемножение матриц
        public static Matrix operator *(Matrix a, Matrix b) {
            
            int rows = a.matrix.GetLength(0);
            int cols = a.matrix.GetLength(1);
            Matrix res = new Matrix(cols, rows);
            if (cols == b.matrix.GetLength(0)) throw new ArgumentException("Ошибка перемножения матриц (число строк a != числу столбцов b)");
            for (int i=0; i<rows; i++) {
                for (int j=0; j<cols; j++) {
                    res.matrix[i,j] = 0;
                    for (int k=0; k<cols; k++) {
                        res.matrix[i,j] += a.matrix[i,k]*b.matrix[k,j];
                    }
                 }
            }
            return res;
        }

        // транспонировать матрицу
        public Matrix transpose()
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            Matrix trans = new Matrix(cols, rows);
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    trans.matrix[j,i] = matrix[i,j];
            return trans;
        }

        //инвертировать матрицу
        public Matrix inverse()
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            if (cols == rows) throw new ArgumentException("Ошибка перемножения матриц (число строк a != числу столбцов b)");
            // дополнение квадратной матрицы единичной матрицей тех же размеров a => [ai]
            Matrix add_matrix = new Matrix(rows, cols * 2);
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    add_matrix.matrix[i,j] = matrix[i,j];
            for (int i = 0; i < rows; i++)
                add_matrix.matrix[i, i+cols] = 1;
            // первая часть
            for (int i = 0; i < rows - 1; i++)
            {
                // нормализовать первую строку
                for (int j = cols*2 - 1; j >= 0; j--)
                    add_matrix.matrix[i,j] /= add_matrix.matrix[i,i];
                for (int k = i + 1; k < rows; k++)
                {
                    double coeff = add_matrix.matrix[k,i];
                    for (int j = 0; j < cols*2; j++)
                    {
                        add_matrix.matrix[k,j] -= add_matrix.matrix[i,j] * coeff;
                    }
                }
            }
            // нормализовать последнюю строку
            for (int j = cols*2 - 1; j >= rows - 1; j--)
                add_matrix.matrix[rows - 1,j] /= add_matrix.matrix[rows - 1,rows - 1];
            // второй проход
            for (int i = rows - 1; i > 0; i--)
            {
                for (int k = i - 1; k >= 0; k--)
                {
                    double coeff = add_matrix.matrix[k,i];
                    for (int j = 0; j < cols*2; j++)
                    {
                        add_matrix.matrix[k,j] -= add_matrix.matrix[i,j] * coeff;
                    }
                }
            }
            // сократите идентификационную матрицу обратно
            Matrix truncate = new Matrix(rows, cols );
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    truncate.matrix[i,j] = add_matrix.matrix[i,j + cols];
            return truncate;
        }

        public override string ToString()
        {
            string res="";
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            for(int i = 0; i < rows; i++)
            {
                for(int j=0;j<cols; j++) res += matrix[i,j].ToString()+" ";
                res += "\n";
            }

            return res;
        }
        public Vector3 m2v()
        {
            return new Vector3(matrix[0, 0] / matrix[3, 0], matrix[1, 0] / matrix[3, 0], matrix[2, 0] / matrix[3, 0]);
        }


    }
}
