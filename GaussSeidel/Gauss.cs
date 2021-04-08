using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussSeidel
{
    class Gauss
    {
        public float[,] coeficientes { get; set; } = new float[3, 3];
        //private float[,] copia { get; set; } = new float[3, 3];
        public float[] lados_derechos { get; set; } = new float[3];
        public float[] vector_solucion { get; set; } = new float[3];
        
        //Método simple para poder trabajar con valores absolutos en el método DD.
        private float VA(float numero) {
            if (numero < 0)
                return numero * -1;
            else
                return numero;
        }

        //En este método aseguramos que el sistema sea diagonalmente dominante para que no divergan nuestras soluciones.
        public bool Diagonal_Dominante()
        {
            bool DD = true;
            float[,] copia = (float[,])coeficientes.Clone();
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    copia[i, j] = VA(copia[i, j]);
            if ((copia[0, 0] < copia[0, 1] + copia[0, 2]) || (copia[1,1]<copia[1,0]+copia[1,2]) || (copia[2,2]<copia[2,0]+copia[2,1]))
                DD = false;

            return DD;
        }

        //Estos métodos calculan los valores de x,y,z para cada iteración dentro del algoritmo.
        private float CalcularX(float y, float z)
        {
            float x = (lados_derechos[0] - coeficientes[0, 1] * y - coeficientes[0, 2] * z) / coeficientes[0, 0];
            return x;
        }
        private float CalcularY(float x, float z)
        {
            float y = (lados_derechos[1] - (coeficientes[1, 0] * x) - (coeficientes[1, 2] * z)) / coeficientes[1, 1];
            return y;
        }
        private float CalcularZ(float x, float y)
        {
            float z = (lados_derechos[2] - (coeficientes[2, 0] * x) - (coeficientes[2, 1] * y)) / coeficientes[2, 2];
            return z;
        }

        private float ErrorPorcentualAproximado(float aproximacion_anterior,float aproximacion_actual )
        {
            float EPA = ((aproximacion_actual - aproximacion_anterior) / aproximacion_actual) * 100;
            EPA = VA(EPA);
            return EPA;
        }
        public void AlgoritmoGS(float x, float y, float z, float ED, int i)
        {
            float[] EPA=new float[3];
            float neox, neoy, neoz;

            Console.WriteLine("\n*ITERACIÓN #" + i);
            //Aquí va a entrar solo en la primera iteración, solo calcula los valores ya que no hay nada que comparar todavía.
            if (i == 1)
            {
                x = CalcularX(y, z);
                y = CalcularY(x, z);
                z = CalcularZ(x, y);
                Console.WriteLine("Valores obtenidos:");
                Console.WriteLine("x:{0}\tError porcentual:100%",x);
                Console.WriteLine("y:{0}\tError porcentual:100%", y);
                Console.WriteLine("z:{0}\tError porcentual:100%", z);
                AlgoritmoGS(x, y, z, ED, i + 1);
            }
            else
            {
                neox = CalcularX(y, z);
                neoy = CalcularY(neox, z);
                neoz = CalcularZ(neox, neoy);
                EPA[0] = ErrorPorcentualAproximado(x, neox);
                EPA[1] = ErrorPorcentualAproximado(y, neoy);
                EPA[2] = ErrorPorcentualAproximado(z, neoz);
                Console.WriteLine("x:{0}\tError porcentual:{1}%", neox,EPA[0]);
                Console.WriteLine("y:{0}\tError porcentual:{1}%", neoy,EPA[1]);
                Console.WriteLine("z:{0}\tError porcentual:{1}%", neoz,EPA[2]);
                //Si se cumple este if se acaba la recursividad y nos manda el mensaje de que encontró la solución del sistema.
                if(EPA[0]<ED && EPA[1]<ED && EPA[2] < ED)
                {
                    Console.WriteLine("\n\nSe ha encontrado un valor de acuerdo su búsqueda!!!!!");
                    Console.WriteLine("Vector solución:");
                    Console.WriteLine("Valor de x:{0}\tError porcentual:{1}%", neox, EPA[0]);
                    Console.WriteLine("Valor de y:{0}\tError porcentual:{1}%", neoy, EPA[1]);
                    Console.WriteLine("Valor z:{0}\tError porcentual:{1}%", neoz, EPA[2]);
                }
                else
                {
                    AlgoritmoGS(neox, neoy, neoz, ED, i + 1);
                }
            }       
        }
    }
}
