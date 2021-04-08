using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussSeidel
{
    class Program
    {
        static void Main(string[] args)
        {
            Gauss g = new Gauss();
            bool diagonal;
            sbyte repeat=1;
            float error_deseado;

            Console.WriteLine("***Bienvenido a la calculadora de raíces de Gauss Seidel***\n\n");
            do
            {
                do
                {
                    MatrizCoeficientes(g);
                    diagonal = g.Diagonal_Dominante();
                    try
                    {
                        if (!diagonal)
                            throw new Exception("el sistema no es diagonalmente dominante.");                     
                    }catch(Exception ex)
                    {
                        Console.WriteLine("Se ha producido una anomalía: " + ex.Message);
                    }
                } while (!diagonal);
                LadoDerecho(g);
                do
                {
                    Console.WriteLine("Escriba por favor el error que desea tener a lo mucho de las 3 raíces:");
                    error_deseado = float.Parse(Console.ReadLine());
                } while (error_deseado < 0 || error_deseado > 100);
                //Se envian como 0 los valores iniciales de x,y y z, el erro deseado y 1 por la primera iteración
                g.AlgoritmoGS(0, 0, 0, error_deseado, 1);
                Console.WriteLine("\n\n¿Desea realizar alguna otra búsqueda de raíces?");
                Console.WriteLine("\t1-Si\t2-No");
                do
                {
                    Console.WriteLine("\nSu respuesta:");
                    repeat = Convert.ToSByte(Console.ReadLine());
                } while (repeat != 1 && repeat != 2);
            } while (repeat != 2);
            Console.WriteLine("\n\nPresion cualquier tecla para salir de la aplicación.");
        }

        //Esta función sirve para llenar los coeficientes de la matriz y mandarselas la clase Gauss para posteriormente procesarlos.
        public static void MatrizCoeficientes(Gauss g){

            //string aux;
            float floaux;
            //bool alpha = false; 
            short confirmacion;
            do
            {
                Console.WriteLine("A continuación escriba los coeficientes de la matriz lineal:");
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        //NOTA DEL PROGRAMADOR:
                        //Trabjar en este segmento a futuro con alguna expresión regular para validar la entrada del usuario.
                        Console.WriteLine("Coeficiente ({0},{1}):", i + 1, j + 1);
                        floaux = float.Parse(Console.ReadLine());
                        g.coeficientes[i, j] = floaux;
                    }
                }
                Console.WriteLine("\n****Matriz resultante****");
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine();
                    for (int j = 0; j < 3; j++)
                    {
                        Console.Write("\t" + g.coeficientes[i, j]);
                        if (j == 0)
                        {
                            Console.Write("x");
                        }
                        else if (j == 1)
                        {
                            Console.Write("y");
                        }
                        else
                        {
                            Console.Write("z");
                        }
                    }
                }
                Console.WriteLine("\n\n¿Esta es la matriz con la que quiere trabajar?");
                Console.WriteLine("\t1-Si\t2-No");
                do
                {
                    Console.WriteLine("\nSu respuesta:");
                    confirmacion = Convert.ToSByte(Console.ReadLine());
                } while (confirmacion != 1 && confirmacion != 2);
            } while (confirmacion != 1);
        }

        //En esta función escribiremos los lados derechos de el sistema para guardarlos en nuestra clase.
        public static void LadoDerecho(Gauss g)
        {
            float LD;
            sbyte confirmacion;
            do
            {
                for (int i = 0; i < 3; i++)
                {
                    //NOTA DEL PROGRAMADOR:
                    //Recuerda trabajar con una expresión regular para este segmento.
                    Console.WriteLine("Escriba el lado derecho de la ecuación {0}:", i + 1);
                    LD = float.Parse(Console.ReadLine());
                    g.lados_derechos[i] = LD;
                }
                Console.WriteLine("\t"+g.lados_derechos[0]);
                Console.WriteLine("\t"+g.lados_derechos[1]);
                Console.WriteLine("\t"+g.lados_derechos[2]);
                Console.WriteLine("\n¿Desea trabajar con estos lados derechos para las ecuaciones?");
                Console.WriteLine("\t1-Si\t2-No");
                do
                {
                    Console.WriteLine("\nSu respuesta:");
                    confirmacion = Convert.ToSByte(Console.ReadLine());
                } while (confirmacion != 1 && confirmacion != 2);
            } while (confirmacion != 1);
        }

    }
}
